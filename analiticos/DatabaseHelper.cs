using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace analiticos
{
    internal class DatabaseHelper
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Ofima"].ConnectionString;

        public static class SessionManager
        {
            public static string Usuario { get; set; }
            public static int IdAuditoria { get; set; }

        }
        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM MTUSUARIO WHERE CODUSUARIO = @username AND PASSWORD = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        SessionManager.Usuario = username;

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
        }
    }
}
