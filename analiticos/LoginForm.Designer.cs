namespace analiticos
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsuario;
        private Label lblContraseña;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblUsuario = new Label();
            lblContraseña = new Label();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Arial", 12F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.Location = new Point(141, 50);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 26);
            txtUsername.TabIndex = 0;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Arial", 12F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(141, 100);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(200, 26);
            txtPassword.TabIndex = 1;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(200, 0, 0);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(141, 150);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 31);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "INICIAR SESIÓN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.Black;
            lblUsuario.Location = new Point(30, 50);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(70, 30);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario";
            // 
            // lblContraseña
            // 
            lblContraseña.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblContraseña.ForeColor = Color.Black;
            lblContraseña.Location = new Point(30, 100);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(100, 30);
            lblContraseña.TabIndex = 4;
            lblContraseña.Text = "Contraseña";
            // 
            // LoginForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(391, 220);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(lblUsuario);
            Controls.Add(lblContraseña);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}