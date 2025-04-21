namespace OEE1.PL
{
    partial class MenuOEE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuOEE));
            this.panelMenuLateral = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnReporteMensual = new System.Windows.Forms.Button();
            this.btnOEEArea = new System.Windows.Forms.Button();
            this.btnOEEMaquina = new System.Windows.Forms.Button();
            this.btnRegistroDatos = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelLogo = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.panelMenuLateral.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenuLateral
            // 
            this.panelMenuLateral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(67)))), ((int)(((byte)(163)))));
            this.panelMenuLateral.Controls.Add(this.label1);
            this.panelMenuLateral.Controls.Add(this.lblTime);
            this.panelMenuLateral.Controls.Add(this.btnReporteMensual);
            this.panelMenuLateral.Controls.Add(this.btnOEEArea);
            this.panelMenuLateral.Controls.Add(this.btnOEEMaquina);
            this.panelMenuLateral.Controls.Add(this.btnRegistroDatos);
            this.panelMenuLateral.Controls.Add(this.panelLogo);
            this.panelMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuLateral.Location = new System.Drawing.Point(0, 0);
            this.panelMenuLateral.Name = "panelMenuLateral";
            this.panelMenuLateral.Size = new System.Drawing.Size(182, 687);
            this.panelMenuLateral.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 598);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hora actual";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(14, 634);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(54, 22);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "Hora";
            // 
            // btnReporteMensual
            // 
            this.btnReporteMensual.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReporteMensual.FlatAppearance.BorderSize = 0;
            this.btnReporteMensual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteMensual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReporteMensual.ForeColor = System.Drawing.Color.White;
            this.btnReporteMensual.Image = global::OEE1.Properties.Resources.clipboard;
            this.btnReporteMensual.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReporteMensual.Location = new System.Drawing.Point(0, 248);
            this.btnReporteMensual.Name = "btnReporteMensual";
            this.btnReporteMensual.Size = new System.Drawing.Size(182, 50);
            this.btnReporteMensual.TabIndex = 4;
            this.btnReporteMensual.Text = "Reporte Mensual";
            this.btnReporteMensual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReporteMensual.UseVisualStyleBackColor = true;
            this.btnReporteMensual.Click += new System.EventHandler(this.btnReporteMensual_Click);
            // 
            // btnOEEArea
            // 
            this.btnOEEArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOEEArea.FlatAppearance.BorderSize = 0;
            this.btnOEEArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOEEArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnOEEArea.ForeColor = System.Drawing.Color.White;
            this.btnOEEArea.Image = global::OEE1.Properties.Resources.flujo;
            this.btnOEEArea.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOEEArea.Location = new System.Drawing.Point(0, 198);
            this.btnOEEArea.Name = "btnOEEArea";
            this.btnOEEArea.Size = new System.Drawing.Size(182, 50);
            this.btnOEEArea.TabIndex = 3;
            this.btnOEEArea.Text = "OEE por Area";
            this.btnOEEArea.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOEEArea.UseVisualStyleBackColor = true;
            this.btnOEEArea.Click += new System.EventHandler(this.btnOEEArea_Click);
            // 
            // btnOEEMaquina
            // 
            this.btnOEEMaquina.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOEEMaquina.FlatAppearance.BorderSize = 0;
            this.btnOEEMaquina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOEEMaquina.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnOEEMaquina.ForeColor = System.Drawing.Color.White;
            this.btnOEEMaquina.Image = global::OEE1.Properties.Resources.bot;
            this.btnOEEMaquina.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOEEMaquina.Location = new System.Drawing.Point(0, 148);
            this.btnOEEMaquina.Name = "btnOEEMaquina";
            this.btnOEEMaquina.Size = new System.Drawing.Size(182, 50);
            this.btnOEEMaquina.TabIndex = 2;
            this.btnOEEMaquina.Text = "OEE por Maquina";
            this.btnOEEMaquina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOEEMaquina.UseVisualStyleBackColor = true;
            this.btnOEEMaquina.Click += new System.EventHandler(this.btnOEEMaquina_Click);
            // 
            // btnRegistroDatos
            // 
            this.btnRegistroDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegistroDatos.FlatAppearance.BorderSize = 0;
            this.btnRegistroDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistroDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRegistroDatos.ForeColor = System.Drawing.Color.White;
            this.btnRegistroDatos.Image = global::OEE1.Properties.Resources.pencil;
            this.btnRegistroDatos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRegistroDatos.Location = new System.Drawing.Point(0, 98);
            this.btnRegistroDatos.Name = "btnRegistroDatos";
            this.btnRegistroDatos.Size = new System.Drawing.Size(182, 50);
            this.btnRegistroDatos.TabIndex = 1;
            this.btnRegistroDatos.Text = "Registro de Datos";
            this.btnRegistroDatos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistroDatos.UseVisualStyleBackColor = true;
            this.btnRegistroDatos.Click += new System.EventHandler(this.btnRegistroDatos_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.panelMain);
            this.panelLogo.Controls.Add(this.labelLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(182, 98);
            this.panelLogo.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(251, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1268, 684);
            this.panelMain.TabIndex = 1;
            // 
            // labelLogo
            // 
            this.labelLogo.AutoSize = true;
            this.labelLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.labelLogo.ForeColor = System.Drawing.Color.White;
            this.labelLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelLogo.Location = new System.Drawing.Point(12, 39);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(165, 29);
            this.labelLogo.TabIndex = 0;
            this.labelLogo.Text = "Hanon - OEE";
            // 
            // panelContenedor
            // 
            this.panelContenedor.AllowDrop = true;
            this.panelContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedor.Controls.Add(this.tableLayoutPanel1);
            this.panelContenedor.Location = new System.Drawing.Point(188, 3);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1328, 681);
            this.panelContenedor.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(442, 231);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 218);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 48F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(91, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 75);
            this.label2.TabIndex = 0;
            this.label2.Text = "BIENVENIDO";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 48F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(18, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(506, 75);
            this.label3.TabIndex = 1;
            this.label3.Text = "CONTROL DE OEE";
            // 
            // Clock
            // 
            this.Clock.Enabled = true;
            this.Clock.Tick += new System.EventHandler(this.Clock_Tick);
            // 
            // MenuOEE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1521, 687);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelMenuLateral);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MenuOEE";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "OEE - Hanon Systems";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuOEE_FormClosing);
            this.Load += new System.EventHandler(this.MenuOEE_Load);
            this.panelMenuLateral.ResumeLayout(false);
            this.panelMenuLateral.PerformLayout();
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenuLateral;
        private System.Windows.Forms.Button btnRegistroDatos;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Button btnOEEMaquina;
        private System.Windows.Forms.Button btnOEEArea;
        private System.Windows.Forms.Button btnReporteMensual;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelLogo;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer Clock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}