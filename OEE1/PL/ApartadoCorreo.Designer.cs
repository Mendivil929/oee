namespace OEE1.PL
{
    partial class ApartadoCorreo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dgvFiltro = new System.Windows.Forms.DataGridView();
            this.dgvEstatusProduccionPorArea = new System.Windows.Forms.DataGridView();
            this.Machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PiecesProduced = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Downtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scrap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Availability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Performance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvScrap = new System.Windows.Forms.DataGridView();
            this.dgvDowntime = new System.Windows.Forms.DataGridView();
            this.dgvEstatusMensual = new System.Windows.Forms.DataGridView();
            this.chartEstatusMensual = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelDataSets = new System.Windows.Forms.Panel();
            this.lblDataSets = new System.Windows.Forms.Label();
            this.chartAcumuladoMensual = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTituloDataSets = new System.Windows.Forms.Label();
            this.lblTituloApartadoMensual = new System.Windows.Forms.Label();
            this.lblTituloFabricacion = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.cboMaquina = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstatusProduccionPorArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScrap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDowntime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstatusMensual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEstatusMensual)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelDataSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAcumuladoMensual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFiltro
            // 
            this.dgvFiltro.AllowUserToAddRows = false;
            this.dgvFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiltro.Location = new System.Drawing.Point(7, 206);
            this.dgvFiltro.Margin = new System.Windows.Forms.Padding(7);
            this.dgvFiltro.Name = "dgvFiltro";
            this.dgvFiltro.Size = new System.Drawing.Size(615, 126);
            this.dgvFiltro.TabIndex = 0;
            // 
            // dgvEstatusProduccionPorArea
            // 
            this.dgvEstatusProduccionPorArea.AllowUserToAddRows = false;
            this.dgvEstatusProduccionPorArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEstatusProduccionPorArea.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstatusProduccionPorArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstatusProduccionPorArea.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Machine,
            this.Target,
            this.PiecesProduced,
            this.Variation,
            this.Downtime,
            this.Scrap,
            this.Availability,
            this.Performance,
            this.Quality,
            this.OEE});
            this.dgvEstatusProduccionPorArea.Location = new System.Drawing.Point(7, 66);
            this.dgvEstatusProduccionPorArea.Margin = new System.Windows.Forms.Padding(7);
            this.dgvEstatusProduccionPorArea.Name = "dgvEstatusProduccionPorArea";
            this.dgvEstatusProduccionPorArea.Size = new System.Drawing.Size(615, 126);
            this.dgvEstatusProduccionPorArea.TabIndex = 1;
            // 
            // Machine
            // 
            this.Machine.HeaderText = "Machine";
            this.Machine.Name = "Machine";
            // 
            // Target
            // 
            this.Target.HeaderText = "Target";
            this.Target.Name = "Target";
            // 
            // PiecesProduced
            // 
            this.PiecesProduced.HeaderText = "Pieces Produced";
            this.PiecesProduced.Name = "PiecesProduced";
            // 
            // Variation
            // 
            this.Variation.HeaderText = "Variation";
            this.Variation.Name = "Variation";
            // 
            // Downtime
            // 
            this.Downtime.HeaderText = "Downtime";
            this.Downtime.Name = "Downtime";
            // 
            // Scrap
            // 
            this.Scrap.HeaderText = "Scrap";
            this.Scrap.Name = "Scrap";
            // 
            // Availability
            // 
            this.Availability.HeaderText = "Availability";
            this.Availability.Name = "Availability";
            // 
            // Performance
            // 
            this.Performance.HeaderText = "Performance";
            this.Performance.Name = "Performance";
            // 
            // Quality
            // 
            this.Quality.HeaderText = "Quality";
            this.Quality.Name = "Quality";
            // 
            // OEE
            // 
            this.OEE.HeaderText = "OEE";
            this.OEE.Name = "OEE";
            // 
            // dgvScrap
            // 
            this.dgvScrap.AllowUserToAddRows = false;
            this.dgvScrap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvScrap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScrap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScrap.Location = new System.Drawing.Point(7, 346);
            this.dgvScrap.Margin = new System.Windows.Forms.Padding(7);
            this.dgvScrap.Name = "dgvScrap";
            this.dgvScrap.Size = new System.Drawing.Size(615, 126);
            this.dgvScrap.TabIndex = 2;
            // 
            // dgvDowntime
            // 
            this.dgvDowntime.AllowUserToAddRows = false;
            this.dgvDowntime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDowntime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDowntime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDowntime.Location = new System.Drawing.Point(7, 486);
            this.dgvDowntime.Margin = new System.Windows.Forms.Padding(7);
            this.dgvDowntime.Name = "dgvDowntime";
            this.dgvDowntime.Size = new System.Drawing.Size(615, 126);
            this.dgvDowntime.TabIndex = 3;
            // 
            // dgvEstatusMensual
            // 
            this.dgvEstatusMensual.AllowUserToAddRows = false;
            this.dgvEstatusMensual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEstatusMensual.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstatusMensual.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvEstatusMensual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEstatusMensual.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEstatusMensual.Location = new System.Drawing.Point(7, 626);
            this.dgvEstatusMensual.Margin = new System.Windows.Forms.Padding(7);
            this.dgvEstatusMensual.Name = "dgvEstatusMensual";
            this.dgvEstatusMensual.RowHeadersVisible = false;
            this.dgvEstatusMensual.Size = new System.Drawing.Size(615, 128);
            this.dgvEstatusMensual.TabIndex = 4;
            // 
            // chartEstatusMensual
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Angle = -30;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.Interval = 0D;
            chartArea1.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea1.AxisX.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chartEstatusMensual.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 2.39F;
            legend1.Position.Width = 25.3268F;
            legend1.Position.X = 10F;
            legend1.Position.Y = 1F;
            this.chartEstatusMensual.Legends.Add(legend1);
            this.chartEstatusMensual.Location = new System.Drawing.Point(689, 101);
            this.chartEstatusMensual.Name = "chartEstatusMensual";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.CustomProperties = "DrawingStyle=LightToDark";
            series1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Label = "#VAL%";
            series1.Legend = "Legend1";
            series1.LegendText = "OEE General Diario";
            series1.MarkerBorderWidth = 10;
            series1.MarkerSize = 10;
            series1.MarkerStep = 2;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.Diamond;
            series1.YValuesPerPoint = 2;
            this.chartEstatusMensual.Series.Add(series1);
            this.chartEstatusMensual.Size = new System.Drawing.Size(896, 547);
            this.chartEstatusMensual.TabIndex = 5;
            this.chartEstatusMensual.Text = "chart1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvEstatusProduccionPorArea, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvFiltro, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvEstatusMensual, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.dgvScrap, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvDowntime, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelDataSets, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 101);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.883598F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.42328F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.42328F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.42328F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.42328F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.42328F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(629, 761);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panelDataSets
            // 
            this.panelDataSets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataSets.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelDataSets.Controls.Add(this.lblDataSets);
            this.panelDataSets.Location = new System.Drawing.Point(5, 5);
            this.panelDataSets.Margin = new System.Windows.Forms.Padding(5);
            this.panelDataSets.Name = "panelDataSets";
            this.panelDataSets.Size = new System.Drawing.Size(619, 49);
            this.panelDataSets.TabIndex = 5;
            // 
            // lblDataSets
            // 
            this.lblDataSets.AutoSize = true;
            this.lblDataSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataSets.Location = new System.Drawing.Point(259, 15);
            this.lblDataSets.Name = "lblDataSets";
            this.lblDataSets.Size = new System.Drawing.Size(97, 24);
            this.lblDataSets.TabIndex = 0;
            this.lblDataSets.Text = "Data Sets";
            // 
            // chartAcumuladoMensual
            // 
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Angle = -30;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Stacked;
            chartArea2.Name = "ChartArea1";
            this.chartAcumuladoMensual.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            legend2.Position.Auto = false;
            legend2.Position.Height = 2.39F;
            legend2.Position.Width = 25.3268F;
            legend2.Position.X = 10F;
            legend2.Position.Y = 1F;
            this.chartAcumuladoMensual.Legends.Add(legend2);
            this.chartAcumuladoMensual.Location = new System.Drawing.Point(689, 672);
            this.chartAcumuladoMensual.Name = "chartAcumuladoMensual";
            series2.ChartArea = "ChartArea1";
            series2.CustomProperties = "DrawingStyle=Cylinder";
            series2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.IsVisibleInLegend = false;
            series2.Label = "#VAL%";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartAcumuladoMensual.Series.Add(series2);
            this.chartAcumuladoMensual.Size = new System.Drawing.Size(896, 547);
            this.chartAcumuladoMensual.TabIndex = 7;
            this.chartAcumuladoMensual.Text = "chart1";
            // 
            // chart1
            // 
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Angle = -30;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            legend3.Position.Auto = false;
            legend3.Position.Height = 2.39F;
            legend3.Position.Width = 25.3268F;
            legend3.Position.X = 10F;
            legend3.Position.Y = 1F;
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(1605, 101);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(389, 327);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // lblTituloDataSets
            // 
            this.lblTituloDataSets.AutoSize = true;
            this.lblTituloDataSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloDataSets.Location = new System.Drawing.Point(188, 54);
            this.lblTituloDataSets.Name = "lblTituloDataSets";
            this.lblTituloDataSets.Size = new System.Drawing.Size(298, 24);
            this.lblTituloDataSets.TabIndex = 9;
            this.lblTituloDataSets.Text = "Data Sets para el flujo de datos";
            // 
            // lblTituloApartadoMensual
            // 
            this.lblTituloApartadoMensual.AutoSize = true;
            this.lblTituloApartadoMensual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloApartadoMensual.Location = new System.Drawing.Point(979, 54);
            this.lblTituloApartadoMensual.Name = "lblTituloApartadoMensual";
            this.lblTituloApartadoMensual.Size = new System.Drawing.Size(381, 24);
            this.lblTituloApartadoMensual.TabIndex = 10;
            this.lblTituloApartadoMensual.Text = "Charts para apartado mensual y general\r\n";
            // 
            // lblTituloFabricacion
            // 
            this.lblTituloFabricacion.AutoSize = true;
            this.lblTituloFabricacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloFabricacion.Location = new System.Drawing.Point(1698, 54);
            this.lblTituloFabricacion.Name = "lblTituloFabricacion";
            this.lblTituloFabricacion.Size = new System.Drawing.Size(232, 24);
            this.lblTituloFabricacion.TabIndex = 11;
            this.lblTituloFabricacion.Text = "Charts para Fabricacion";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cboArea, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboMaquina, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(30, 906);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(290, 94);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // cboArea
            // 
            this.cboArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(3, 3);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(284, 32);
            this.cboArea.TabIndex = 0;
            // 
            // cboMaquina
            // 
            this.cboMaquina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMaquina.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaquina.FormattingEnabled = true;
            this.cboMaquina.Location = new System.Drawing.Point(3, 50);
            this.cboMaquina.Name = "cboMaquina";
            this.cboMaquina.Size = new System.Drawing.Size(284, 32);
            this.cboMaquina.TabIndex = 1;
            // 
            // ApartadoCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1211, 638);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.lblTituloFabricacion);
            this.Controls.Add(this.lblTituloApartadoMensual);
            this.Controls.Add(this.lblTituloDataSets);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chartAcumuladoMensual);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.chartEstatusMensual);
            this.Name = "ApartadoCorreo";
            this.Text = "ApartadoCorreo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstatusProduccionPorArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScrap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDowntime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstatusMensual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEstatusMensual)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelDataSets.ResumeLayout(false);
            this.panelDataSets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAcumuladoMensual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFiltro;
        private System.Windows.Forms.DataGridView dgvEstatusProduccionPorArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn Target;
        private System.Windows.Forms.DataGridViewTextBoxColumn PiecesProduced;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Downtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scrap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Availability;
        private System.Windows.Forms.DataGridViewTextBoxColumn Performance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quality;
        private System.Windows.Forms.DataGridViewTextBoxColumn OEE;
        private System.Windows.Forms.DataGridView dgvScrap;
        private System.Windows.Forms.DataGridView dgvDowntime;
        private System.Windows.Forms.DataGridView dgvEstatusMensual;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEstatusMensual;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelDataSets;
        private System.Windows.Forms.Label lblDataSets;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAcumuladoMensual;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lblTituloDataSets;
        private System.Windows.Forms.Label lblTituloApartadoMensual;
        private System.Windows.Forms.Label lblTituloFabricacion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.ComboBox cboMaquina;
    }
}