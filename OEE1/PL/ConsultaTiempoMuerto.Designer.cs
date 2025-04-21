namespace OEE1.PL
{
    partial class ConsultaTiempoMuerto
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDatosTiempoMuerto = new System.Windows.Forms.DataGridView();
            this.razonTiempoMuerto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Minutos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExcel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosTiempoMuerto)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDatosTiempoMuerto, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExcel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(73, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 180);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvDatosTiempoMuerto
            // 
            this.dgvDatosTiempoMuerto.AllowUserToAddRows = false;
            this.dgvDatosTiempoMuerto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatosTiempoMuerto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatosTiempoMuerto.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDatosTiempoMuerto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosTiempoMuerto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.razonTiempoMuerto,
            this.Minutos});
            this.dgvDatosTiempoMuerto.Location = new System.Drawing.Point(3, 3);
            this.dgvDatosTiempoMuerto.Name = "dgvDatosTiempoMuerto";
            this.dgvDatosTiempoMuerto.Size = new System.Drawing.Size(510, 29);
            this.dgvDatosTiempoMuerto.TabIndex = 0;
            // 
            // razonTiempoMuerto
            // 
            this.razonTiempoMuerto.HeaderText = "Razon De Tiempo Muerto";
            this.razonTiempoMuerto.Name = "razonTiempoMuerto";
            // 
            // Minutos
            // 
            this.Minutos.HeaderText = "Minutos";
            this.Minutos.Name = "Minutos";
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExcel.Location = new System.Drawing.Point(220, 147);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 1;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // ConsultaTiempoMuerto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(673, 435);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConsultaTiempoMuerto";
            this.Text = "Consulta De Contribuidores De Tiempo Muerto";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosTiempoMuerto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvDatosTiempoMuerto;
        private System.Windows.Forms.DataGridViewTextBoxColumn razonTiempoMuerto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Minutos;
        private System.Windows.Forms.Button btnExcel;
    }
}