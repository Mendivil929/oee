namespace OEE1.PL
{
    partial class ConsultaScrap
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
            this.dgvDatosScrap = new System.Windows.Forms.DataGridView();
            this.nombreScrap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numPiezas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExcel = new System.Windows.Forms.Button();
            this.resolver_lavadoras = new System.Windows.Forms.Button();
            this.dgvLavadoras = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosScrap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLavadoras)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDatosScrap, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExcel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(73, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 180);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvDatosScrap
            // 
            this.dgvDatosScrap.AllowUserToAddRows = false;
            this.dgvDatosScrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatosScrap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatosScrap.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDatosScrap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosScrap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreScrap,
            this.numPiezas});
            this.dgvDatosScrap.Location = new System.Drawing.Point(3, 3);
            this.dgvDatosScrap.Name = "dgvDatosScrap";
            this.dgvDatosScrap.Size = new System.Drawing.Size(384, 29);
            this.dgvDatosScrap.TabIndex = 0;
            // 
            // nombreScrap
            // 
            this.nombreScrap.HeaderText = "Nombre De Scrap";
            this.nombreScrap.Name = "nombreScrap";
            // 
            // numPiezas
            // 
            this.numPiezas.HeaderText = "Numero De Piezas";
            this.numPiezas.Name = "numPiezas";
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExcel.Location = new System.Drawing.Point(157, 147);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 1;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // resolver_lavadoras
            // 
            this.resolver_lavadoras.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resolver_lavadoras.Location = new System.Drawing.Point(236, 236);
            this.resolver_lavadoras.Name = "resolver_lavadoras";
            this.resolver_lavadoras.Size = new System.Drawing.Size(69, 23);
            this.resolver_lavadoras.TabIndex = 1;
            this.resolver_lavadoras.Text = "Corregir";
            this.resolver_lavadoras.UseVisualStyleBackColor = true;
            this.resolver_lavadoras.Visible = false;
            this.resolver_lavadoras.Click += new System.EventHandler(this.resolver_lavadoras_Click);
            // 
            // dgvLavadoras
            // 
            this.dgvLavadoras.AllowUserToAddRows = false;
            this.dgvLavadoras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLavadoras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLavadoras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLavadoras.Location = new System.Drawing.Point(12, 276);
            this.dgvLavadoras.Name = "dgvLavadoras";
            this.dgvLavadoras.Size = new System.Drawing.Size(512, 51);
            this.dgvLavadoras.TabIndex = 2;
            this.dgvLavadoras.Visible = false;
            // 
            // ConsultaScrap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(547, 357);
            this.Controls.Add(this.dgvLavadoras);
            this.Controls.Add(this.resolver_lavadoras);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConsultaScrap";
            this.Text = "Consulta de Contribuidores de Scrap";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosScrap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLavadoras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvDatosScrap;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreScrap;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPiezas;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button resolver_lavadoras;
        private System.Windows.Forms.DataGridView dgvLavadoras;
    }
}