namespace analiticos
{
    partial class Analiticos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Analiticos));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            dgvResultados = new DataGridView();
            BtnGenerar = new Button();
            BtnExportar = new Button();
            listBoxFunciones = new ListBox();
            panel1 = new FlowLayoutPanel();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("MS Reference Sans Serif", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(206, 0, 0);
            label1.Location = new Point(316, 70);
            label1.Name = "label1";
            label1.Size = new Size(511, 49);
            label1.TabIndex = 0;
            label1.Text = "Analíticos Empresariales";
            label1.TextAlign = ContentAlignment.MiddleCenter;
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
            // dgvResultados
            // 
            dgvResultados.AllowUserToDeleteRows = false;   // No permite borrar filas
            dgvResultados.AllowUserToAddRows = false;      // No permite agregar filas manualmente
            dgvResultados.AllowUserToOrderColumns = false; // No permite mover columnas

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
                Size = new Size(758, 360),
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
                BackColor = Color.FromArgb(150, 0, 0),
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
                SelectionBackColor = Color.FromArgb(206, 0, 0), 
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
                    col.MinimumWidth = 50; // Ancho mínimo para evitar que se compriman
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Ajuste dinámico
                    col.FillWeight = 1; // Distribución uniforme
                }
            };
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
            BtnGenerar.Location = new Point(964, 223);
            BtnGenerar.Name = "BtnGenerar";
            BtnGenerar.Size = new Size(150, 35);
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
            BtnExportar.Location = new Point(964, 264);
            BtnExportar.Name = "BtnExportar";
            BtnExportar.Size = new Size(150, 33);
            BtnExportar.TabIndex = 5;
            BtnExportar.Text = "EXPORTAR";
            BtnExportar.UseVisualStyleBackColor = false;
            BtnExportar.Click += btnExportar_Click;
            // 
            // listBoxFunciones
            // 
            listBoxFunciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listBoxFunciones.BackColor = Color.White;
            listBoxFunciones.BorderStyle = BorderStyle.FixedSingle;
            listBoxFunciones.Font = new Font("Arial", 10F, FontStyle.Bold);
            listBoxFunciones.ForeColor = Color.Black;
            listBoxFunciones.FormattingEnabled = true;
            listBoxFunciones.Location = new Point(12, 223);
            listBoxFunciones.Name = "listBoxFunciones";
            listBoxFunciones.ScrollAlwaysVisible = true;
            listBoxFunciones.Size = new Size(166, 370);
            listBoxFunciones.TabIndex = 6;
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
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(184, 152);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(774, 65);
            panel1.TabIndex = 7;
            // 
            // label3
            // 
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 0;
            // 
            // Analiticos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1126, 607);
            Controls.Add(label3);
            Controls.Add(panel1);
            Controls.Add(listBoxFunciones);
            Controls.Add(BtnExportar);
            Controls.Add(BtnGenerar);
            Controls.Add(dgvResultados);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "Analiticos";
            Text = "Analiticos";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private DataGridView dgvResultados;
        private Button BtnGenerar;
        private Button BtnExportar;
        private ListBox listBoxFunciones;
        private Panel panel1;
        private Label label3;
    }
}