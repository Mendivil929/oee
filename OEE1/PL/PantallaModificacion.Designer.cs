namespace OEE1.PL
{
    partial class PantallaModificacion
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
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.cboStartTime = new System.Windows.Forms.ComboBox();
            this.cboEndTime = new System.Windows.Forms.ComboBox();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.lblPiecesProduced = new System.Windows.Forms.Label();
            this.cboPartNumber = new System.Windows.Forms.ComboBox();
            this.numUDPiecesProduced = new System.Windows.Forms.NumericUpDown();
            this.lblDowntime = new System.Windows.Forms.Label();
            this.lblScrapPieces = new System.Windows.Forms.Label();
            this.numUDDowntime = new System.Windows.Forms.NumericUpDown();
            this.numUDScrapPieces = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPiecesProduced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDDowntime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDScrapPieces)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.btnActualizar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblStartTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEndTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboStartTime, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboEndTime, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPartNumber, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPiecesProduced, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboPartNumber, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numUDPiecesProduced, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDowntime, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblScrapPieces, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUDDowntime, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.numUDScrapPieces, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelar, 4, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 109);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.btnActualizar, 3);
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Location = new System.Drawing.Point(200, 79);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(80, 27);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblStartTime
            // 
            this.lblStartTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(37, 9);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(86, 19);
            this.lblStartTime.TabIndex = 2;
            this.lblStartTime.Text = "Start Time";
            // 
            // lblEndTime
            // 
            this.lblEndTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTime.Location = new System.Drawing.Point(199, 9);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(81, 19);
            this.lblEndTime.TabIndex = 3;
            this.lblEndTime.Text = "End Time";
            // 
            // cboStartTime
            // 
            this.cboStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStartTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStartTime.FormattingEnabled = true;
            this.cboStartTime.Location = new System.Drawing.Point(3, 41);
            this.cboStartTime.Name = "cboStartTime";
            this.cboStartTime.Size = new System.Drawing.Size(154, 26);
            this.cboStartTime.TabIndex = 4;
            this.cboStartTime.SelectedIndexChanged += new System.EventHandler(this.cboStartTime_SelectedIndexChanged);
            // 
            // cboEndTime
            // 
            this.cboEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEndTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEndTime.FormattingEnabled = true;
            this.cboEndTime.Location = new System.Drawing.Point(163, 41);
            this.cboEndTime.Name = "cboEndTime";
            this.cboEndTime.Size = new System.Drawing.Size(154, 26);
            this.cboEndTime.TabIndex = 5;
            // 
            // lblPartNumber
            // 
            this.lblPartNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPartNumber.AutoSize = true;
            this.lblPartNumber.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartNumber.Location = new System.Drawing.Point(347, 9);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(105, 19);
            this.lblPartNumber.TabIndex = 6;
            this.lblPartNumber.Text = "Part Number";
            // 
            // lblPiecesProduced
            // 
            this.lblPiecesProduced.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPiecesProduced.AutoSize = true;
            this.lblPiecesProduced.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPiecesProduced.Location = new System.Drawing.Point(490, 9);
            this.lblPiecesProduced.Name = "lblPiecesProduced";
            this.lblPiecesProduced.Size = new System.Drawing.Size(139, 19);
            this.lblPiecesProduced.TabIndex = 7;
            this.lblPiecesProduced.Text = "Pieces Produced";
            // 
            // cboPartNumber
            // 
            this.cboPartNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPartNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPartNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPartNumber.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPartNumber.FormattingEnabled = true;
            this.cboPartNumber.Location = new System.Drawing.Point(323, 41);
            this.cboPartNumber.Name = "cboPartNumber";
            this.cboPartNumber.Size = new System.Drawing.Size(154, 26);
            this.cboPartNumber.TabIndex = 8;
            // 
            // numUDPiecesProduced
            // 
            this.numUDPiecesProduced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numUDPiecesProduced.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDPiecesProduced.Location = new System.Drawing.Point(483, 41);
            this.numUDPiecesProduced.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUDPiecesProduced.Name = "numUDPiecesProduced";
            this.numUDPiecesProduced.Size = new System.Drawing.Size(154, 26);
            this.numUDPiecesProduced.TabIndex = 9;
            // 
            // lblDowntime
            // 
            this.lblDowntime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDowntime.AutoSize = true;
            this.lblDowntime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDowntime.Location = new System.Drawing.Point(645, 9);
            this.lblDowntime.Name = "lblDowntime";
            this.lblDowntime.Size = new System.Drawing.Size(150, 19);
            this.lblDowntime.TabIndex = 10;
            this.lblDowntime.Text = "Downtime Minutes";
            // 
            // lblScrapPieces
            // 
            this.lblScrapPieces.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblScrapPieces.AutoSize = true;
            this.lblScrapPieces.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScrapPieces.Location = new System.Drawing.Point(825, 9);
            this.lblScrapPieces.Name = "lblScrapPieces";
            this.lblScrapPieces.Size = new System.Drawing.Size(109, 19);
            this.lblScrapPieces.TabIndex = 11;
            this.lblScrapPieces.Text = "Scrap Pieces";
            // 
            // numUDDowntime
            // 
            this.numUDDowntime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numUDDowntime.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDDowntime.Location = new System.Drawing.Point(643, 41);
            this.numUDDowntime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUDDowntime.Name = "numUDDowntime";
            this.numUDDowntime.Size = new System.Drawing.Size(154, 26);
            this.numUDDowntime.TabIndex = 12;
            // 
            // numUDScrapPieces
            // 
            this.numUDScrapPieces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numUDScrapPieces.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDScrapPieces.Location = new System.Drawing.Point(803, 41);
            this.numUDScrapPieces.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUDScrapPieces.Name = "numUDScrapPieces";
            this.numUDScrapPieces.Size = new System.Drawing.Size(154, 26);
            this.numUDScrapPieces.TabIndex = 13;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(680, 79);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 27);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // PantallaModificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 173);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PantallaModificacion";
            this.Text = "Apartado para la modificacion de valores";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPiecesProduced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDDowntime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDScrapPieces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.ComboBox cboStartTime;
        private System.Windows.Forms.ComboBox cboEndTime;
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.Label lblPiecesProduced;
        private System.Windows.Forms.ComboBox cboPartNumber;
        private System.Windows.Forms.NumericUpDown numUDPiecesProduced;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblDowntime;
        private System.Windows.Forms.Label lblScrapPieces;
        private System.Windows.Forms.NumericUpDown numUDDowntime;
        private System.Windows.Forms.NumericUpDown numUDScrapPieces;
        private System.Windows.Forms.Button btnCancelar;
    }
}