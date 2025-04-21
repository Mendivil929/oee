namespace OEE1.PL
{
    partial class registro
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblConfiguracion = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblMaquina = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblHoraInicial = new System.Windows.Forms.Label();
            this.lblHoraFinal = new System.Windows.Forms.Label();
            this.lblNumeroParte = new System.Windows.Forms.Label();
            this.lblPiezasProducidas = new System.Windows.Forms.Label();
            this.lblTiempoMuertoPlaneado = new System.Windows.Forms.Label();
            this.cboTurno = new System.Windows.Forms.ComboBox();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.cboMaquina = new System.Windows.Forms.ComboBox();
            this.cboHoraInicial = new System.Windows.Forms.ComboBox();
            this.cboHoraFinal = new System.Windows.Forms.ComboBox();
            this.dtFecha = new System.Windows.Forms.DateTimePicker();
            this.numUDPiezasProducidas = new System.Windows.Forms.NumericUpDown();
            this.cboTiempoMuertoPlaneado = new System.Windows.Forms.ComboBox();
            this.btnSubirDatos = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.cboNumeroParte = new System.Windows.Forms.ComboBox();
            this.btnRatePerHour = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTablaScrap = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboRazonScrap = new System.Windows.Forms.ComboBox();
            this.dgvScrap = new System.Windows.Forms.DataGridView();
            this.Scrap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numUDPiezasScrap = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarScrap = new System.Windows.Forms.Button();
            this.btnEliminarScrap = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRazonTiempoMuerto = new System.Windows.Forms.Label();
            this.lblMinTiempoMuerto = new System.Windows.Forms.Label();
            this.cboRazonTiempoMuerto = new System.Windows.Forms.ComboBox();
            this.numericUDMinTiempoMuerto = new System.Windows.Forms.NumericUpDown();
            this.dgvTiempoMuerto = new System.Windows.Forms.DataGridView();
            this.TiempoMuerto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Minutos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAgregarTiempoMuerto = new System.Windows.Forms.Button();
            this.btnEliminarTiempoMuerto = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPiezasProducidas)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScrap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPiezasScrap)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUDMinTiempoMuerto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiempoMuerto)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 30);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(986, 650);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTurno, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblArea, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblMaquina, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblFecha, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblHoraInicial, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.lblHoraFinal, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.lblNumeroParte, 0, 13);
            this.tableLayoutPanel2.Controls.Add(this.lblPiezasProducidas, 0, 15);
            this.tableLayoutPanel2.Controls.Add(this.lblTiempoMuertoPlaneado, 0, 17);
            this.tableLayoutPanel2.Controls.Add(this.cboTurno, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cboArea, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.cboMaquina, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.cboHoraInicial, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.cboHoraFinal, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this.dtFecha, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.numUDPiezasProducidas, 0, 16);
            this.tableLayoutPanel2.Controls.Add(this.cboTiempoMuertoPlaneado, 0, 18);
            this.tableLayoutPanel2.Controls.Add(this.btnSubirDatos, 0, 19);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel8, 0, 14);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(11, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 20;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.42884F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.717979F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.716893F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.657303F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(273, 650);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblConfiguracion, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(269, 57);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblConfiguracion
            // 
            this.lblConfiguracion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConfiguracion.AutoSize = true;
            this.lblConfiguracion.Font = new System.Drawing.Font("Arial Narrow", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfiguracion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblConfiguracion.Location = new System.Drawing.Point(51, 13);
            this.lblConfiguracion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfiguracion.Name = "lblConfiguracion";
            this.lblConfiguracion.Size = new System.Drawing.Size(166, 31);
            this.lblConfiguracion.TabIndex = 0;
            this.lblConfiguracion.Text = "Configuracion";
            // 
            // lblTurno
            // 
            this.lblTurno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurno.Location = new System.Drawing.Point(2, 62);
            this.lblTurno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(56, 23);
            this.lblTurno.TabIndex = 1;
            this.lblTurno.Text = "Turno";
            // 
            // lblArea
            // 
            this.lblArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblArea.AutoSize = true;
            this.lblArea.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArea.Location = new System.Drawing.Point(2, 123);
            this.lblArea.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(45, 23);
            this.lblArea.TabIndex = 2;
            this.lblArea.Text = "Area";
            // 
            // lblMaquina
            // 
            this.lblMaquina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaquina.AutoSize = true;
            this.lblMaquina.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaquina.Location = new System.Drawing.Point(2, 184);
            this.lblMaquina.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaquina.Name = "lblMaquina";
            this.lblMaquina.Size = new System.Drawing.Size(75, 23);
            this.lblMaquina.TabIndex = 3;
            this.lblMaquina.Text = "Maquina";
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(2, 245);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(57, 23);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "Fecha";
            // 
            // lblHoraInicial
            // 
            this.lblHoraInicial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHoraInicial.AutoSize = true;
            this.lblHoraInicial.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraInicial.Location = new System.Drawing.Point(2, 306);
            this.lblHoraInicial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHoraInicial.Name = "lblHoraInicial";
            this.lblHoraInicial.Size = new System.Drawing.Size(94, 23);
            this.lblHoraInicial.TabIndex = 5;
            this.lblHoraInicial.Text = "Hora Inicial";
            // 
            // lblHoraFinal
            // 
            this.lblHoraFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHoraFinal.AutoSize = true;
            this.lblHoraFinal.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraFinal.Location = new System.Drawing.Point(2, 367);
            this.lblHoraFinal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHoraFinal.Name = "lblHoraFinal";
            this.lblHoraFinal.Size = new System.Drawing.Size(87, 23);
            this.lblHoraFinal.TabIndex = 6;
            this.lblHoraFinal.Text = "Hora Final";
            // 
            // lblNumeroParte
            // 
            this.lblNumeroParte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumeroParte.AutoSize = true;
            this.lblNumeroParte.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroParte.Location = new System.Drawing.Point(2, 428);
            this.lblNumeroParte.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNumeroParte.Name = "lblNumeroParte";
            this.lblNumeroParte.Size = new System.Drawing.Size(137, 23);
            this.lblNumeroParte.TabIndex = 7;
            this.lblNumeroParte.Text = "Numero De Parte";
            // 
            // lblPiezasProducidas
            // 
            this.lblPiezasProducidas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPiezasProducidas.AutoSize = true;
            this.lblPiezasProducidas.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPiezasProducidas.Location = new System.Drawing.Point(2, 489);
            this.lblPiezasProducidas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPiezasProducidas.Name = "lblPiezasProducidas";
            this.lblPiezasProducidas.Size = new System.Drawing.Size(150, 23);
            this.lblPiezasProducidas.TabIndex = 8;
            this.lblPiezasProducidas.Text = "Piezas Producidas";
            // 
            // lblTiempoMuertoPlaneado
            // 
            this.lblTiempoMuertoPlaneado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTiempoMuertoPlaneado.AutoSize = true;
            this.lblTiempoMuertoPlaneado.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempoMuertoPlaneado.Location = new System.Drawing.Point(2, 550);
            this.lblTiempoMuertoPlaneado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTiempoMuertoPlaneado.Name = "lblTiempoMuertoPlaneado";
            this.lblTiempoMuertoPlaneado.Size = new System.Drawing.Size(199, 23);
            this.lblTiempoMuertoPlaneado.TabIndex = 9;
            this.lblTiempoMuertoPlaneado.Text = "Tiempo Muerto Planeado";
            // 
            // cboTurno
            // 
            this.cboTurno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTurno.FormattingEnabled = true;
            this.cboTurno.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboTurno.Location = new System.Drawing.Point(2, 87);
            this.cboTurno.Margin = new System.Windows.Forms.Padding(2);
            this.cboTurno.Name = "cboTurno";
            this.cboTurno.Size = new System.Drawing.Size(241, 32);
            this.cboTurno.TabIndex = 10;
            // 
            // cboArea
            // 
            this.cboArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(2, 148);
            this.cboArea.Margin = new System.Windows.Forms.Padding(2);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(241, 32);
            this.cboArea.TabIndex = 11;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // cboMaquina
            // 
            this.cboMaquina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMaquina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaquina.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaquina.FormattingEnabled = true;
            this.cboMaquina.Location = new System.Drawing.Point(2, 209);
            this.cboMaquina.Margin = new System.Windows.Forms.Padding(2);
            this.cboMaquina.Name = "cboMaquina";
            this.cboMaquina.Size = new System.Drawing.Size(241, 32);
            this.cboMaquina.TabIndex = 12;
            this.cboMaquina.SelectedIndexChanged += new System.EventHandler(this.cboMaquina_SelectedIndexChanged);
            // 
            // cboHoraInicial
            // 
            this.cboHoraInicial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboHoraInicial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoraInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHoraInicial.FormattingEnabled = true;
            this.cboHoraInicial.Location = new System.Drawing.Point(2, 331);
            this.cboHoraInicial.Margin = new System.Windows.Forms.Padding(2);
            this.cboHoraInicial.Name = "cboHoraInicial";
            this.cboHoraInicial.Size = new System.Drawing.Size(241, 32);
            this.cboHoraInicial.TabIndex = 14;
            this.cboHoraInicial.SelectedIndexChanged += new System.EventHandler(this.cboHoraInicial_SelectedIndexChanged);
            // 
            // cboHoraFinal
            // 
            this.cboHoraFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboHoraFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoraFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHoraFinal.FormattingEnabled = true;
            this.cboHoraFinal.Location = new System.Drawing.Point(2, 392);
            this.cboHoraFinal.Margin = new System.Windows.Forms.Padding(2);
            this.cboHoraFinal.Name = "cboHoraFinal";
            this.cboHoraFinal.Size = new System.Drawing.Size(241, 32);
            this.cboHoraFinal.TabIndex = 15;
            // 
            // dtFecha
            // 
            this.dtFecha.CustomFormat = "MM-DD-YY";
            this.dtFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFecha.Location = new System.Drawing.Point(2, 270);
            this.dtFecha.Margin = new System.Windows.Forms.Padding(2);
            this.dtFecha.MaxDate = new System.DateTime(2026, 1, 1, 0, 0, 0, 0);
            this.dtFecha.MinDate = new System.DateTime(2024, 8, 1, 0, 0, 0, 0);
            this.dtFecha.Name = "dtFecha";
            this.dtFecha.Size = new System.Drawing.Size(241, 29);
            this.dtFecha.TabIndex = 16;
            this.dtFecha.Value = new System.DateTime(2024, 9, 29, 20, 48, 46, 0);
            // 
            // numUDPiezasProducidas
            // 
            this.numUDPiezasProducidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUDPiezasProducidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDPiezasProducidas.Location = new System.Drawing.Point(2, 514);
            this.numUDPiezasProducidas.Margin = new System.Windows.Forms.Padding(2);
            this.numUDPiezasProducidas.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUDPiezasProducidas.Name = "numUDPiezasProducidas";
            this.numUDPiezasProducidas.Size = new System.Drawing.Size(241, 29);
            this.numUDPiezasProducidas.TabIndex = 18;
            // 
            // cboTiempoMuertoPlaneado
            // 
            this.cboTiempoMuertoPlaneado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTiempoMuertoPlaneado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiempoMuertoPlaneado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTiempoMuertoPlaneado.FormattingEnabled = true;
            this.cboTiempoMuertoPlaneado.Location = new System.Drawing.Point(2, 575);
            this.cboTiempoMuertoPlaneado.Margin = new System.Windows.Forms.Padding(2);
            this.cboTiempoMuertoPlaneado.Name = "cboTiempoMuertoPlaneado";
            this.cboTiempoMuertoPlaneado.Size = new System.Drawing.Size(241, 32);
            this.cboTiempoMuertoPlaneado.TabIndex = 19;
            // 
            // btnSubirDatos
            // 
            this.btnSubirDatos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubirDatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubirDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirDatos.Location = new System.Drawing.Point(66, 614);
            this.btnSubirDatos.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubirDatos.Name = "btnSubirDatos";
            this.btnSubirDatos.Size = new System.Drawing.Size(112, 31);
            this.btnSubirDatos.TabIndex = 20;
            this.btnSubirDatos.Text = "Subir Datos";
            this.btnSubirDatos.UseVisualStyleBackColor = true;
            this.btnSubirDatos.Click += new System.EventHandler(this.btnSubirDatos_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.Controls.Add(this.cboNumeroParte, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnRatePerHour, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 454);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(239, 31);
            this.tableLayoutPanel8.TabIndex = 21;
            // 
            // cboNumeroParte
            // 
            this.cboNumeroParte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboNumeroParte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNumeroParte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNumeroParte.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNumeroParte.FormattingEnabled = true;
            this.cboNumeroParte.Location = new System.Drawing.Point(3, 3);
            this.cboNumeroParte.Name = "cboNumeroParte";
            this.cboNumeroParte.Size = new System.Drawing.Size(173, 32);
            this.cboNumeroParte.TabIndex = 0;
            this.cboNumeroParte.SelectedIndexChanged += new System.EventHandler(this.cboNumeroParte_SelectedIndexChanged);
            // 
            // btnRatePerHour
            // 
            this.btnRatePerHour.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRatePerHour.Enabled = false;
            this.btnRatePerHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRatePerHour.Location = new System.Drawing.Point(182, 3);
            this.btnRatePerHour.Name = "btnRatePerHour";
            this.btnRatePerHour.Size = new System.Drawing.Size(54, 25);
            this.btnRatePerHour.TabIndex = 1;
            this.btnRatePerHour.Text = "Rate";
            this.btnRatePerHour.UseVisualStyleBackColor = true;
            this.btnRatePerHour.Click += new System.EventHandler(this.btnRatePerHour_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.cboRazonScrap, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.dgvScrap, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.numUDPiezasScrap, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnAgregarScrap, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.btnEliminarScrap, 1, 4);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(306, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(11, 0, 11, 12);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(669, 313);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel4.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.lblTablaScrap, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(665, 42);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lblTablaScrap
            // 
            this.lblTablaScrap.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTablaScrap.AutoSize = true;
            this.lblTablaScrap.Font = new System.Drawing.Font("Arial Narrow", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTablaScrap.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTablaScrap.Location = new System.Drawing.Point(2, 5);
            this.lblTablaScrap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTablaScrap.Name = "lblTablaScrap";
            this.lblTablaScrap.Size = new System.Drawing.Size(172, 31);
            this.lblTablaScrap.TabIndex = 1;
            this.lblTablaScrap.Text = "Tabla de Scrap";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Razon de Scrap";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(336, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "No. Piezas";
            // 
            // cboRazonScrap
            // 
            this.cboRazonScrap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRazonScrap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRazonScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRazonScrap.FormattingEnabled = true;
            this.cboRazonScrap.Location = new System.Drawing.Point(2, 73);
            this.cboRazonScrap.Margin = new System.Windows.Forms.Padding(2);
            this.cboRazonScrap.Name = "cboRazonScrap";
            this.cboRazonScrap.Size = new System.Drawing.Size(330, 32);
            this.cboRazonScrap.TabIndex = 3;
            // 
            // dgvScrap
            // 
            this.dgvScrap.AllowUserToAddRows = false;
            this.dgvScrap.AllowUserToDeleteRows = false;
            this.dgvScrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvScrap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScrap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScrap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Scrap,
            this.Cantidad});
            this.tableLayoutPanel4.SetColumnSpan(this.dgvScrap, 2);
            this.dgvScrap.Location = new System.Drawing.Point(10, 118);
            this.dgvScrap.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.dgvScrap.Name = "dgvScrap";
            this.dgvScrap.ReadOnly = true;
            this.dgvScrap.RowHeadersWidth = 51;
            this.dgvScrap.RowTemplate.Height = 24;
            this.dgvScrap.Size = new System.Drawing.Size(649, 33);
            this.dgvScrap.TabIndex = 5;
            this.dgvScrap.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SeleccionarScrap);
            // 
            // Scrap
            // 
            this.Scrap.HeaderText = "Scrap";
            this.Scrap.MinimumWidth = 6;
            this.Scrap.Name = "Scrap";
            this.Scrap.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // numUDPiezasScrap
            // 
            this.numUDPiezasScrap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUDPiezasScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDPiezasScrap.Location = new System.Drawing.Point(336, 73);
            this.numUDPiezasScrap.Margin = new System.Windows.Forms.Padding(2);
            this.numUDPiezasScrap.Name = "numUDPiezasScrap";
            this.numUDPiezasScrap.Size = new System.Drawing.Size(331, 29);
            this.numUDPiezasScrap.TabIndex = 6;
            this.numUDPiezasScrap.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAgregarScrap
            // 
            this.btnAgregarScrap.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgregarScrap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarScrap.Location = new System.Drawing.Point(118, 278);
            this.btnAgregarScrap.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarScrap.Name = "btnAgregarScrap";
            this.btnAgregarScrap.Size = new System.Drawing.Size(98, 30);
            this.btnAgregarScrap.TabIndex = 7;
            this.btnAgregarScrap.Text = "Agregar";
            this.btnAgregarScrap.UseVisualStyleBackColor = true;
            this.btnAgregarScrap.Click += new System.EventHandler(this.btnAgregarScrap_Click);
            // 
            // btnEliminarScrap
            // 
            this.btnEliminarScrap.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEliminarScrap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarScrap.Enabled = false;
            this.btnEliminarScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarScrap.Location = new System.Drawing.Point(452, 278);
            this.btnEliminarScrap.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarScrap.Name = "btnEliminarScrap";
            this.btnEliminarScrap.Size = new System.Drawing.Size(98, 30);
            this.btnEliminarScrap.TabIndex = 8;
            this.btnEliminarScrap.Text = "Eliminar";
            this.btnEliminarScrap.UseVisualStyleBackColor = true;
            this.btnEliminarScrap.Click += new System.EventHandler(this.btnEliminarScrap_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblRazonTiempoMuerto, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblMinTiempoMuerto, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.cboRazonTiempoMuerto, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.numericUDMinTiempoMuerto, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.dgvTiempoMuerto, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.btnAgregarTiempoMuerto, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.btnEliminarTiempoMuerto, 1, 4);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(306, 337);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(11, 12, 11, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(669, 313);
            this.tableLayoutPanel6.TabIndex = 6;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel6.SetColumnSpan(this.tableLayoutPanel7, 2);
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(665, 42);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tabla de Tiempo Muerto";
            // 
            // lblRazonTiempoMuerto
            // 
            this.lblRazonTiempoMuerto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRazonTiempoMuerto.AutoSize = true;
            this.lblRazonTiempoMuerto.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonTiempoMuerto.Location = new System.Drawing.Point(2, 48);
            this.lblRazonTiempoMuerto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRazonTiempoMuerto.Name = "lblRazonTiempoMuerto";
            this.lblRazonTiempoMuerto.Size = new System.Drawing.Size(199, 23);
            this.lblRazonTiempoMuerto.TabIndex = 1;
            this.lblRazonTiempoMuerto.Text = "Razon de Tiempo Muerto";
            // 
            // lblMinTiempoMuerto
            // 
            this.lblMinTiempoMuerto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMinTiempoMuerto.AutoSize = true;
            this.lblMinTiempoMuerto.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinTiempoMuerto.Location = new System.Drawing.Point(336, 48);
            this.lblMinTiempoMuerto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMinTiempoMuerto.Name = "lblMinTiempoMuerto";
            this.lblMinTiempoMuerto.Size = new System.Drawing.Size(71, 23);
            this.lblMinTiempoMuerto.TabIndex = 2;
            this.lblMinTiempoMuerto.Text = "Minutos";
            // 
            // cboRazonTiempoMuerto
            // 
            this.cboRazonTiempoMuerto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRazonTiempoMuerto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRazonTiempoMuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRazonTiempoMuerto.FormattingEnabled = true;
            this.cboRazonTiempoMuerto.Location = new System.Drawing.Point(2, 73);
            this.cboRazonTiempoMuerto.Margin = new System.Windows.Forms.Padding(2);
            this.cboRazonTiempoMuerto.Name = "cboRazonTiempoMuerto";
            this.cboRazonTiempoMuerto.Size = new System.Drawing.Size(330, 32);
            this.cboRazonTiempoMuerto.TabIndex = 3;
            // 
            // numericUDMinTiempoMuerto
            // 
            this.numericUDMinTiempoMuerto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUDMinTiempoMuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUDMinTiempoMuerto.Location = new System.Drawing.Point(336, 73);
            this.numericUDMinTiempoMuerto.Margin = new System.Windows.Forms.Padding(2);
            this.numericUDMinTiempoMuerto.Name = "numericUDMinTiempoMuerto";
            this.numericUDMinTiempoMuerto.Size = new System.Drawing.Size(331, 29);
            this.numericUDMinTiempoMuerto.TabIndex = 4;
            this.numericUDMinTiempoMuerto.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dgvTiempoMuerto
            // 
            this.dgvTiempoMuerto.AllowUserToAddRows = false;
            this.dgvTiempoMuerto.AllowUserToDeleteRows = false;
            this.dgvTiempoMuerto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTiempoMuerto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTiempoMuerto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTiempoMuerto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TiempoMuerto,
            this.Minutos});
            this.tableLayoutPanel6.SetColumnSpan(this.dgvTiempoMuerto, 2);
            this.dgvTiempoMuerto.Location = new System.Drawing.Point(10, 118);
            this.dgvTiempoMuerto.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.dgvTiempoMuerto.Name = "dgvTiempoMuerto";
            this.dgvTiempoMuerto.ReadOnly = true;
            this.dgvTiempoMuerto.RowHeadersWidth = 51;
            this.dgvTiempoMuerto.RowTemplate.Height = 24;
            this.dgvTiempoMuerto.Size = new System.Drawing.Size(649, 33);
            this.dgvTiempoMuerto.TabIndex = 5;
            this.dgvTiempoMuerto.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SeleccionarTiempoMuerto);
            // 
            // TiempoMuerto
            // 
            this.TiempoMuerto.HeaderText = "Razon De Tiempo Muerto";
            this.TiempoMuerto.MinimumWidth = 6;
            this.TiempoMuerto.Name = "TiempoMuerto";
            this.TiempoMuerto.ReadOnly = true;
            // 
            // Minutos
            // 
            this.Minutos.HeaderText = "Minutos";
            this.Minutos.MinimumWidth = 6;
            this.Minutos.Name = "Minutos";
            this.Minutos.ReadOnly = true;
            // 
            // btnAgregarTiempoMuerto
            // 
            this.btnAgregarTiempoMuerto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgregarTiempoMuerto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarTiempoMuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarTiempoMuerto.Location = new System.Drawing.Point(118, 278);
            this.btnAgregarTiempoMuerto.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarTiempoMuerto.Name = "btnAgregarTiempoMuerto";
            this.btnAgregarTiempoMuerto.Size = new System.Drawing.Size(98, 30);
            this.btnAgregarTiempoMuerto.TabIndex = 6;
            this.btnAgregarTiempoMuerto.Text = "Agregar";
            this.btnAgregarTiempoMuerto.UseVisualStyleBackColor = true;
            this.btnAgregarTiempoMuerto.Click += new System.EventHandler(this.btnAgregarTiempoMuerto_Click);
            // 
            // btnEliminarTiempoMuerto
            // 
            this.btnEliminarTiempoMuerto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEliminarTiempoMuerto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarTiempoMuerto.Enabled = false;
            this.btnEliminarTiempoMuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarTiempoMuerto.Location = new System.Drawing.Point(452, 278);
            this.btnEliminarTiempoMuerto.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarTiempoMuerto.Name = "btnEliminarTiempoMuerto";
            this.btnEliminarTiempoMuerto.Size = new System.Drawing.Size(98, 30);
            this.btnEliminarTiempoMuerto.TabIndex = 7;
            this.btnEliminarTiempoMuerto.Text = "Eliminar";
            this.btnEliminarTiempoMuerto.UseVisualStyleBackColor = true;
            this.btnEliminarTiempoMuerto.Click += new System.EventHandler(this.btnEliminarTiempoMuerto_Click);
            // 
            // registro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1009, 687);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "registro";
            this.Text = "Registro de Datos";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPiezasProducidas)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScrap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPiezasScrap)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUDMinTiempoMuerto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiempoMuerto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblConfiguracion;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblMaquina;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblHoraInicial;
        private System.Windows.Forms.Label lblHoraFinal;
        private System.Windows.Forms.Label lblNumeroParte;
        private System.Windows.Forms.Label lblPiezasProducidas;
        private System.Windows.Forms.Label lblTiempoMuertoPlaneado;
        private System.Windows.Forms.ComboBox cboTurno;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.ComboBox cboMaquina;
        private System.Windows.Forms.ComboBox cboHoraInicial;
        private System.Windows.Forms.ComboBox cboHoraFinal;
        private System.Windows.Forms.DateTimePicker dtFecha;
        private System.Windows.Forms.NumericUpDown numUDPiezasProducidas;
        private System.Windows.Forms.ComboBox cboTiempoMuertoPlaneado;
        private System.Windows.Forms.Button btnSubirDatos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lblTablaScrap;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboRazonScrap;
        private System.Windows.Forms.DataGridView dgvScrap;
        private System.Windows.Forms.NumericUpDown numUDPiezasScrap;
        private System.Windows.Forms.Button btnAgregarScrap;
        private System.Windows.Forms.Button btnEliminarScrap;
        private System.Windows.Forms.Label lblRazonTiempoMuerto;
        private System.Windows.Forms.Label lblMinTiempoMuerto;
        private System.Windows.Forms.ComboBox cboRazonTiempoMuerto;
        private System.Windows.Forms.NumericUpDown numericUDMinTiempoMuerto;
        private System.Windows.Forms.DataGridView dgvTiempoMuerto;
        private System.Windows.Forms.Button btnAgregarTiempoMuerto;
        private System.Windows.Forms.Button btnEliminarTiempoMuerto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scrap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn TiempoMuerto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Minutos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.ComboBox cboNumeroParte;
        private System.Windows.Forms.Button btnRatePerHour;
    }
}