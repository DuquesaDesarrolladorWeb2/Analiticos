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
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace analiticos
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        string connectionString = ConfigurationManager.ConnectionStrings["Permisos"].ConnectionString;
        public LoginForm()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (dbHelper.ValidateUser(username, password)) // Verifica usuario y contraseña
            {
                if (ValidarPermisos(username)) // Verifica si tiene permisos
                {
                    MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    this.DialogResult = DialogResult.OK;
                    Form1 mainForm = new Form1();
                }
                else
                {
                    MessageBox.Show("No tiene permisos para acceder", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidarPermisos(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM PermAnaliticos WHERE USUARIO = @username AND ESTADO = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        SessionManager.Usuario = username; // Guarda el usuario en sesión
                        return true;
                    }
                    return false;
                }
            }
        }


        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            txtUsername.Text = txtUsername.Text.ToUpper();
            txtUsername.SelectionStart = txtUsername.Text.Length; // Mantiene el cursor al final
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido de "ding"
                IniciarSesion(); // Llama a la función que procesa el inicio de sesión
            }
        }
        private void IniciarSesion()
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (dbHelper.ValidateUser(username, password)) // Verifica usuario y contraseña
            {
                if (ValidarPermisos(username)) // Verifica si tiene permisos
                {
                    MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    this.DialogResult = DialogResult.OK;
                    Form1 mainForm = new Form1();
                }
                else
                {
                    MessageBox.Show("No tiene permisos para acceder", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
