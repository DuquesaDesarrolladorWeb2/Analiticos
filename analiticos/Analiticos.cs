using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static analiticos.DatabaseHelper;
using System.Configuration;

namespace analiticos
{
    public partial class Analiticos : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Analiticos"].ConnectionString;
        string connectionDuquesa = ConfigurationManager.ConnectionStrings["Duquesa"].ConnectionString;
        string connectionPermisos = ConfigurationManager.ConnectionStrings["Permisos"].ConnectionString;
        string usuario = SessionManager.Usuario;

        public Analiticos()
        {
            InitializeComponent();
            VerificarPermisos(usuario);
            listBoxFunciones.SelectedIndexChanged += listBoxFunciones_SelectedIndexChanged;
            this.Controls.Add(this.listBoxFunciones);
            this.Controls.Add(this.panel1);
            this.WindowState = FormWindowState.Maximized;
            CargarFunciones();
        }
        private void VerificarPermisos(string usuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionPermisos))
                {
                    conn.Open();
                    string query = "SELECT GENERA, EXPORTA FROM PermAnaliticos WHERE USUARIO = @Usuario";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                BtnGenerar.Enabled = Convert.ToBoolean(reader["GENERA"]);
                                BtnExportar.Enabled = Convert.ToBoolean(reader["EXPORTA"]);
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron permisos para este usuario.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void CargarFunciones()
        {
            try
            {
                List<int> funcionesPermitidas = new List<int>();

                // 1️⃣ PRIMERO: OBTENER FUNCIONES PERMITIDAS DESDE Permisos.dbo.PermAnaliticos
                using (SqlConnection conPermisos = new SqlConnection(connectionPermisos))
                {
                    conPermisos.Open();

                    string queryPermisos = "SELECT FUNCIONES FROM PermAnaliticos WHERE USUARIO = @Usuario";

                    using (SqlCommand cmd = new SqlCommand(queryPermisos, conPermisos))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            funcionesPermitidas = result.ToString()
                                .Split(',')
                                .Select(f => int.TryParse(f, out int id) ? id : (int?)null)
                                .Where(id => id.HasValue)
                                .Select(id => id.Value)
                                .ToList();
                        }
                    }
                }

                if (funcionesPermitidas.Count == 0)
                {
                    MessageBox.Show("No tienes funciones disponibles.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2️⃣ SEGUNDO: OBTENER FUNCIONES DISPONIBLES DESDE Analiticos.dbo.Funciones
                using (SqlConnection conAnaliticos = new SqlConnection(connectionString))
                {
                    conAnaliticos.Open();

                    string queryFunciones = $@"
                SELECT IdFuncion, NombreVisual 
                FROM Funciones 
                WHERE IdFuncion IN ({string.Join(",", funcionesPermitidas)})";

                    using (SqlCommand cmd = new SqlCommand(queryFunciones, conAnaliticos))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var funciones = new List<KeyValuePair<int, string>>();

                        while (reader.Read())
                        {
                            funciones.Add(new KeyValuePair<int, string>(
                                Convert.ToInt32(reader["IdFuncion"]),
                                reader["NombreVisual"].ToString()
                            ));
                        }

                        if (funciones.Count == 0)
                        {
                            MessageBox.Show("No hay funciones disponibles.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        listBoxFunciones.DataSource = funciones;
                        listBoxFunciones.DisplayMember = "Value";
                        listBoxFunciones.ValueMember = "Key";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al cargar funciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listBoxFunciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFunciones.SelectedItem != null)
            {
                // Limpiar dgvResultados antes de cargar una nueva función
                dgvResultados.DataSource = null;
                dgvResultados.Rows.Clear();
                dgvResultados.Columns.Clear();
                BtnGenerar.Enabled = true;
                int idFuncion = ((KeyValuePair<int, string>)listBoxFunciones.SelectedItem).Key;
                CargarFormularioDinamico(idFuncion);
            }
        }

        private void CargarFormularioDinamico(int idFuncion)
        {
            panel1.Controls.Clear(); // Limpia antes de agregar nuevos elementos

            FlowLayoutPanel container = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true, // Permite que pasen a la siguiente línea si no caben
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT NombreParametro, TipoDato FROM Parametros WHERE IdFuncion = @IdFuncion";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdFuncion", idFuncion);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["NombreParametro"].ToString();
                            string tipo = reader["TipoDato"].ToString().ToLower();

                            FlowLayoutPanel campoContainer = new FlowLayoutPanel
                            {
                                AutoSize = true,
                                FlowDirection = FlowDirection.TopDown,
                                WrapContents = true,
                                Margin = new Padding(0, 10, 0, 10)
                            };

                            Label lbl = new Label
                            {
                                Text = nombre,
                                AutoSize = true,
                                Width = 150,
                                Padding = new Padding(5),
                                Font = new Font("Mangueria", 10, FontStyle.Bold),
                            };

                            Control input = null;
                            switch (tipo)
                            {
                                case "date":
                                    input = new DateTimePicker()
                                    {
                                        Width = 220,
                                        Value = DateTime.Now.AddDays(1)
                                    };
                                    break;
                                case "datetime":
                                    input = new DateTimePicker()
                                    {
                                        Width = 220,
                                        Format = DateTimePickerFormat.Custom,
                                        CustomFormat = "dddd, dd 'de' MMMM 'de' yyyy HH:mm:ss",
                                        ShowUpDown = false, 
                                        Value = DateTime.Now.AddDays(1)
                                    };
                                    break;
                                case "string":
                                    input = new TextBox { Width = 150 };
                                    break;
                                case "int":
                                    input = new NumericUpDown { Width = 150 };
                                    break;
                                case "bool":
                                    input = new CheckBox();
                                    break;
                                default:
                                    input = new TextBox { Width = 150 };
                                    break;
                                case "dropdown":
                                    input = new ComboBox
                                    {
                                        Width = 200,
                                        DropDownStyle = ComboBoxStyle.DropDownList
                                    };

                                    List<KeyValuePair<int, string>> items = new List<KeyValuePair<int, string>>();

                                    using (SqlConnection conn = new SqlConnection(connectionString))
                                    {
                                        conn.Open();
                                        string queryS = "SELECT NOMBRE, VALOR FROM DropdownValues WHERE IDFUNCION = @TipoId";

                                        using (SqlCommand cmdS = new SqlCommand(queryS, conn))
                                        {
                                            cmdS.Parameters.AddWithValue("@TipoId", idFuncion);
                                            using (SqlDataReader readers = cmdS.ExecuteReader())
                                            {
                                                while (readers.Read())
                                                {
                                                    string nombres = readers["NOMBRE"] != DBNull.Value ? readers["NOMBRE"].ToString() : "";
                                                    string valor = readers["VALOR"] != DBNull.Value && !string.IsNullOrEmpty(readers["VALOR"].ToString())
                                                                    ? readers["VALOR"].ToString()
                                                                    : "";

                                                    ((ComboBox)input).Items.Add(new KeyValuePair<string, string>(valor, nombres));
                                                }
                                            }
                                        }
                                    }

                                    ((ComboBox)input).DisplayMember = "Value"; 
                                    ((ComboBox)input).ValueMember = "Key";

                                    break;

                            }

                            if (input != null)
                            {
                                input.Name = nombre;
                                input.Margin = new Padding(5);
                                campoContainer.Controls.Add(lbl);
                                campoContainer.Controls.Add(input);
                            }

                            panel1.Controls.Add(campoContainer);
                        }
                    }
                }
            }
        }


        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            BtnGenerar.Enabled = false;
            if (listBoxFunciones.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una función.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnGenerar.Enabled = true;
                return;
            }

            int idFuncion = ((KeyValuePair<int, string>)listBoxFunciones.SelectedItem).Key;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            foreach (Control control in panel1.Controls)
            {
                if (control is FlowLayoutPanel container)
                {
                    string nombreParametro = container.Controls[0].Text; // Primer control es el Label

                    Control inputControl = container.Controls[1]; // Segundo control es el Input
                    object valor = null;

                    if (inputControl is TextBox textBox)
                    {
                        valor = textBox.Text;
                    }
                    else if (inputControl is NumericUpDown numericUpDown)
                    {
                        valor = numericUpDown.Value;
                    }
                    else if (inputControl is DateTimePicker dateTimePicker)
                    {
                        if (dateTimePicker.Format == DateTimePickerFormat.Short)
                        {
                            valor = dateTimePicker.Value.Date;
                        }
                        else
                        {
                            valor = dateTimePicker.Value;
                        }
                    }
                    else if (inputControl is ComboBox comboBox)
                    {
                        if (comboBox.SelectedItem is KeyValuePair<string, string> selectedItem)
                        {
                            valor = selectedItem.Key; // Obtiene el valor correcto
                        }
                        else
                        {
                            valor = comboBox.SelectedValue; // Alternativa si se usa BindingSource
                        }
                    }

                    parametros.Add(nombreParametro, valor);
                }
            }

            EjecutarFuncionSQL(idFuncion, parametros);
        }
        private void EjecutarFuncionSQL(int idFuncion, Dictionary<string, object> parametros)
        {
            string nombreFuncion = "";
            string baseDeDatos = "";
            int tipoFuncion = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string obtenerFuncionQuery = "SELECT NombreSQL, BaseDeDatos, Tipo FROM Funciones WHERE IdFuncion = @IdFuncion";

                using (SqlCommand cmd = new SqlCommand(obtenerFuncionQuery, con))
                {
                    cmd.Parameters.AddWithValue("@IdFuncion", idFuncion);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("No se encontró la función SQL en la tabla Funciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            BtnGenerar.Enabled = true;
                            return;
                        }

                        nombreFuncion = reader["NombreSQL"].ToString();
                        baseDeDatos = reader["BaseDeDatos"].ToString();
                        tipoFuncion = Convert.ToInt32(reader["Tipo"]);
                    }
                }
            }
            var builder = new SqlConnectionStringBuilder(connectionDuquesa);
            builder.InitialCatalog = baseDeDatos;
            connectionDuquesa = builder.ToString();


            using (SqlConnection con = new SqlConnection(connectionDuquesa))
            {
                con.Open();

                // **Determinar si es una función (0) o una vista (1)**
                string validarFuncionQuery = tipoFuncion == 0
                    ? "SELECT COUNT(*) FROM sys.objects WHERE name = @NombreFuncion AND type IN ('IF', 'FN', 'TF')"
                    : "SELECT COUNT(*) FROM sys.views WHERE name = @NombreFuncion";

                using (SqlCommand cmd = new SqlCommand(validarFuncionQuery, con))
                {
                    cmd.Parameters.AddWithValue("@NombreFuncion", nombreFuncion);
                    int existeFuncion = (int)cmd.ExecuteScalar();

                    if (existeFuncion == 0)
                    {
                        MessageBox.Show("La función o vista SQL especificada no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        BtnGenerar.Enabled = true;
                        return;
                    }
                }

                // **Construcción de la consulta dependiendo del tipo**
                string query = tipoFuncion == 0
                    ? $"SELECT * FROM {nombreFuncion}({string.Join(", ", parametros.Keys.Select(p => "@" + p))})"
                    : $"SELECT * FROM {nombreFuncion}";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (tipoFuncion == 0) // Solo agrega parámetros si es una función
                    {
                        foreach (var param in parametros)
                        {
                            object valor = param.Value;

                            if (valor is DateTime dateValue)
                            {
                                if (dateValue.TimeOfDay == TimeSpan.Zero)
                                {
                                    string fechaFormateada = ((DateTime)param.Value).ToString("yyyyMMdd");
                                    cmd.Parameters.Add(new SqlParameter("@" + param.Key, SqlDbType.VarChar, 8) { Value = fechaFormateada });
                                }
                                else
                                {
                                    cmd.Parameters.Add("@" + param.Key, SqlDbType.DateTime).Value = dateValue; 
                                }
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@" + param.Key, valor);
                            }
                        }
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        DataTable dtNumerado = AgregarColumnaNumeracion(dt);
                        MostrarResultados(dtNumerado);

                        CrearAuditoria(idFuncion);
                    }
                }
                BtnGenerar.Enabled = true;
            }
        }
        private DataTable AgregarColumnaNumeracion(DataTable dt)
        {
            // Clonar estructura de la DataTable original
            DataTable dtNumerado = dt.Clone();

            // Agregar columna de numeración al inicio
            dtNumerado.Columns.Add("#", typeof(int)).SetOrdinal(0); // Insertarla como primera columna

            // Llenar la nueva tabla con la numeración
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow newRow = dtNumerado.NewRow();
                newRow["#"] = i + 1; // Numeración secuencial

                // Copiar los valores de las otras columnas
                foreach (DataColumn col in dt.Columns)
                {
                    newRow[col.ColumnName] = dt.Rows[i][col.ColumnName];
                }

                dtNumerado.Rows.Add(newRow);
            }

            return dtNumerado;
        }
        private void MostrarResultados(DataTable dt)
        {
            if (dgvResultados.InvokeRequired)
            {
                dgvResultados.Invoke(new Action(() => dgvResultados.DataSource = dt));
            }
            else
            {
                dgvResultados.DataSource = dt;
            }
        }
        private void CrearAuditoria(int idFuncion)
        {
            int idAuditoria = 0;
            string usuario = SessionManager.Usuario;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO AUD_ANALITICOS (USUARIO, IDFUNCION, FECHA, EXPORTAR) " +
                               "OUTPUT INSERTED.ID_AUD " +  // Esto devuelve el ID recién insertado
                               "VALUES (@Usuario, @idFuncion, @Fecha, @Exportar)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@idFuncion", idFuncion);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@Exportar", 0); // Inicialmente en 0

                    connection.Open();
                    idAuditoria = (int)command.ExecuteScalar(); // Obtener el ID generado
                }
            }
            SessionManager.IdAuditoria = idAuditoria;
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarExcel();
            MarcarComoExportado(SessionManager.IdAuditoria);
        }
        private void ExportarExcel()
        {
            if (dgvResultados.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Files|*.xlsx", Title = "Guardar archivo de Excel" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            DataTable dt = ((DataTable)dgvResultados.DataSource);
                            wb.Worksheets.Add(dt, "Resultados");

                            wb.SaveAs(sfd.FileName);
                            MessageBox.Show("Exportación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void MarcarComoExportado(int idAuditoria)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE AUD_ANALITICOS SET EXPORTAR = 1 WHERE ID_AUD = @IdAuditoria";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAuditoria", idAuditoria);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
