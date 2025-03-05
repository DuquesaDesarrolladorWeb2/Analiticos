using System.Windows.Forms;

namespace analiticos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            BtnGenerar = new Button();
            BtnExportar = new Button();
            label1 = new Label();
            dgvResultados = new DataGridView();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 40);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(166, 79);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // BtnGenerar
            // 
            BtnGenerar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnGenerar.BackColor = Color.FromArgb(206, 0, 0);
            BtnGenerar.Cursor = Cursors.Hand;
            BtnGenerar.FlatAppearance.BorderColor = Color.Black;
            BtnGenerar.FlatAppearance.BorderSize = 2;
            BtnGenerar.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 0, 0);
            BtnGenerar.FlatStyle = FlatStyle.Flat;
            BtnGenerar.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            BtnGenerar.ForeColor = Color.White;
            BtnGenerar.Location = new Point(950, 223);
            BtnGenerar.Name = "BtnGenerar";
            BtnGenerar.Size = new Size(138, 35);
            BtnGenerar.TabIndex = 4;
            BtnGenerar.Text = "GENERAR";
            BtnGenerar.UseVisualStyleBackColor = false;
            BtnGenerar.Click += BtnGenerar_Click;
            // 
            // BtnExportar
            // 
            BtnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExportar.BackColor = Color.FromArgb(206, 0, 0);
            BtnExportar.Cursor = Cursors.Hand;
            BtnExportar.FlatAppearance.BorderColor = Color.Black;
            BtnExportar.FlatAppearance.BorderSize = 2;
            BtnExportar.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 0, 0);
            BtnExportar.FlatStyle = FlatStyle.Flat;
            BtnExportar.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            BtnExportar.ForeColor = Color.White;
            BtnExportar.Location = new Point(950, 264);
            BtnExportar.Name = "BtnExportar";
            BtnExportar.Size = new Size(138, 33);
            BtnExportar.TabIndex = 5;
            BtnExportar.Text = "EXPORTAR";
            BtnExportar.UseVisualStyleBackColor = false;
            BtnExportar.Click += btnExportar_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("MS Reference Sans Serif", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(206, 0, 0);
            label1.Location = new Point(316, 70);
            label1.Name = "label1";
            label1.Size = new Size(522, 49);
            label1.TabIndex = 0;
            label1.Text = "Analíticos Empresariales";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvResultados
            // 
            // Configuración general del DataGridView
            dgvResultados = new DataGridView
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                GridColor = Color.Black,
                EnableHeadersVisualStyles = false,
                Location = new Point(187, 223),
                Name = "dgvResultados",
                RowTemplate = { Height = 30 },
                Size = new Size(758, 302),
                TabIndex = 3,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None // Control manual del tamaño
            };

            // Estilos para filas alternas
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle
            {
                BackColor = Color.White
            };
            dgvResultados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            // Estilos de encabezado
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(150, 0, 0), // Rojo oscuro
                Font = new Font("Arial", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText,
                WrapMode = DataGridViewTriState.True
            };
            dgvResultados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Estilos de celdas normales
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = SystemColors.Window,
                Font = new Font("Segoe UI", 9F),
                ForeColor = SystemColors.ControlText,
                SelectionBackColor = Color.FromArgb(206, 0, 0), // Rojo corporativo
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.False
            };
            dgvResultados.DefaultCellStyle = dataGridViewCellStyle3;

            // Estilos de filas
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle
            {
                BackColor = Color.LightGray,
                Font = new Font("Arial", 10F),
                ForeColor = Color.Black
            };
            dgvResultados.RowsDefaultCellStyle = dataGridViewCellStyle4;

            // Ajustar el ancho mínimo de cada columna cuando se llenen los datos
            dgvResultados.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn col in dgvResultados.Columns)
                {
                    col.MinimumWidth = 150; // Ancho mínimo para evitar que se compriman
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Ajuste dinámico
                    col.FillWeight = 1; // Distribución uniforme
                }
            };

            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(206, 0, 0);
            label2.Location = new Point(37, 176);
            label2.Name = "label2";
            label2.Size = new Size(116, 33);
            label2.TabIndex = 2;
            label2.Text = "Reportes";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 534);
            Controls.Add(label2);
            Controls.Add(dgvResultados);
            Controls.Add(label1);
            Controls.Add(BtnExportar);
            Controls.Add(BtnGenerar);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Formulario Dinámico";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private PictureBox pictureBox1;
        private ListBox listBoxFunciones;
        private Button BtnGenerar;
        private Button BtnExportar;
        private DataGridView dataGridViewParametros;
        private Label label1;
        private DataGridView dgvResultados;
        private Label label2;
    }
}
