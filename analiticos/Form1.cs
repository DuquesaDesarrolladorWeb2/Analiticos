using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static analiticos.DatabaseHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Configuration;

namespace analiticos
{
    public partial class Form1 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Analiticos"].ConnectionString;
        string connectionDuquesa = ConfigurationManager.ConnectionStrings["Duquesa"].ConnectionString;
        string connectionPermisos = ConfigurationManager.ConnectionStrings["Permisos"].ConnectionString;
        private FlowLayoutPanel panel;
        private bool _formCargado = false;

        public Form1()
        {
            InitializeComponent();
            _formCargado = true;
            string usuario = SessionManager.Usuario;
            VerificarPermisos(usuario);
            //this.WindowState = FormWindowState.Maximized;
            this.Text = "Analiticos";

            // ListBox para seleccionar funciones
            listBoxFunciones = new ListBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left, // Mantiene la expansión con la ventana
                Location = new Point(20, 230),
                Size = new Size((int)(ClientSize.Width * 0.15), (int)(ClientSize.Height * 0.55)),
                BackColor = Color.White,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Mangueria", 12, FontStyle.Regular),
                ScrollAlwaysVisible = true,
                IntegralHeight = false,
                ItemHeight = 40
            };

            // Permite personalizar el diseño de los ítems
            listBoxFunciones.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxFunciones.DrawItem += (sender, e) =>
            {
                if (e.Index < 0) return;

                e.DrawBackground();

                bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                Brush textBrush = isSelected ? Brushes.White : Brushes.Black;
                Brush backgroundBrush = isSelected ? new SolidBrush(Color.FromArgb(206, 0, 0)) : Brushes.White;

                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

                // Obtener solo el 'Value' del KeyValuePair
                if (listBoxFunciones.Items[e.Index] is KeyValuePair<int, string> funcion)
                {
                    e.Graphics.DrawString(funcion.Value, e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
                }

                e.DrawFocusRectangle();
            };


            listBoxFunciones.SelectedIndexChanged += listBoxFunciones_SelectedIndexChanged;

            panel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, // Mantiene expansión horizontal
                Location = new Point(200, 125), // Ajuste de posición
                Size = new Size(900, 150), // Más espacio para los controles internos
                Padding = new Padding(10), // Espaciado interno
                AutoSize = true, // Ajusta automáticamente su tamaño según el contenido
                AutoSizeMode = AutoSizeMode.GrowAndShrink, // Se expande si es necesario
                FlowDirection = FlowDirection.LeftToRight, // Distribuye los elementos en fila
                WrapContents = true // Permite que los elementos se ajusten a la siguiente línea si no caben
            };


            // Agregar controles al formulario
            this.Controls.Add(listBoxFunciones);
            this.Controls.Add(panel);

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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT IdFuncion, NombreVisual FROM Funciones";

                    using (SqlCommand cmd = new SqlCommand(query, con))
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
            panel.Controls.Clear();

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

                            FlowLayoutPanel container = new FlowLayoutPanel
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
                            container.Controls.Add(lbl);

                            Control input = null;
                            switch (tipo)
                            {
                                case "date":
                                    input = new DateTimePicker() { Width = 220 ,
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
                            }

                            if (input != null)
                            {
                                input.Name = nombre;
                                input.Margin = new Padding(0, 5, 10, 10);
                                container.Controls.Add(input);
                            }

                            panel.Controls.Add(container);
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

            foreach (Control control in panel.Controls)
            {
                if (control is FlowLayoutPanel container)
                {
                    string nombreParametro = container.Controls[0].Text; // Primer control es el Label

                    Control inputControl = container.Controls[1]; // Segundo control es el Input
                    object valor = null;

                    if (inputControl is TextBox textBox)
                    {
                        if (string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            MessageBox.Show($"El campo '{nombreParametro}' no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            BtnGenerar.Enabled = true;
                            return;
                        }
                        valor = textBox.Text;
                    }
                    else if (inputControl is NumericUpDown numericUpDown)
                    {
                        valor = numericUpDown.Value;
                    }
                    else if (inputControl is DateTimePicker dateTimePicker)
                    {
                        valor = dateTimePicker.Value.Date;
                    }
                    else if (inputControl is CheckBox checkBox)
                    {
                        valor = checkBox.Checked;
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
                            cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                        }
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // **Agregar la columna de numeración antes de mostrar resultados**
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
