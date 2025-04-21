using iTextSharp.text.pdf;
using iTextSharp.text;
using OEE1.BLL;
using OEE1.DAL;
using OEE1.PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq.Expressions;

namespace OEE1
{
    public partial class oeePorArea : Form
    {
        consultaDAL objectConsultaDAL = new consultaDAL();
        public oeePorArea()
        {
            InitializeComponent();
            cargarComboBoxes();
        }

        /* -----CARGA DE COMBO BOXES----- */

        private void cargarComboBoxes()
        {

            /* Carga de datos cboTurno -> Combo Box para el cambio de turno */
            cboTurno.DataSource = objectConsultaDAL.MostrarDatos("Turno").Tables[0];
            cboTurno.DisplayMember = "shiftChange";
            cboTurno.ValueMember = "shiftChange";

            /* Carga de datos cboArea -> Combo Box para especificar el area */
            cboArea.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Area FROM nombreAreas WHERE Area != 'Planta'").Tables[0];
            cboArea.DisplayMember = "area";
            cboArea.ValueMember = "area";
        }

        private AreaBLL RecuperarInformacion()
        {
            AreaBLL objectAreaBLL = new AreaBLL();
            objectAreaBLL.Fecha = dtFecha.Value.ToString("dd/MM/yyyy");
            objectAreaBLL.Turno = cboTurno.Text;
            objectAreaBLL.Area = cboArea.Text;
            return objectAreaBLL;
        }

        public DataGridView UpdateSizeDgv(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);

            return dgv;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            #region LIMPIEZA DE CHARTS Y DATAGRIDVIEW
            chartDisponibilidad = objectConsultaDAL.limpiarCharts(chartDisponibilidad);
            chartRendimiento = objectConsultaDAL.limpiarCharts(chartRendimiento);
            chartCalidad = objectConsultaDAL.limpiarCharts(chartCalidad);
            chartOEE = objectConsultaDAL.limpiarCharts(chartOEE);
            chartMuestreo1 = objectConsultaDAL.limpiarCharts(chartMuestreo1);
            chartMuestreo2 = objectConsultaDAL.limpiarCharts(chartMuestreo2);
            dgvEstatusProduccionPorArea.Rows.Clear();
            #endregion

            AreaBLL objectAreaBLL = RecuperarInformacion();
            string fecha = objectAreaBLL.Fecha;
            string turno = objectAreaBLL.Turno;
            string area = objectAreaBLL.Area;
            if(turno != "Todos los turnos")
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand
                ("SELECT fecha, turno, area, machine, target, piecesProduced, " +
                "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
                "OEE FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND turno='" + turno + "' AND area='" + area + "'").Tables[0];
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND turno='" + turno + "' " +
                "AND area='" + area + "'");
            }
            else
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand
                ("SELECT fecha, turno, area, machine, target, piecesProduced, " +
                "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
                "OEE FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND area='" + area + "'").Tables[0];
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND area='" + area + "'");
            }
                
            #region CONFIGURACION DE HEADERS
            dgvFiltro.Columns[0].HeaderText = "Fecha";
            dgvFiltro.Columns[1].HeaderText = "Turno";
            dgvFiltro.Columns[2].HeaderText = "Area";
            dgvFiltro.Columns[3].HeaderText = "Machine";
            dgvFiltro.Columns[4].HeaderText = "Target";
            dgvFiltro.Columns[5].HeaderText = "Pieces Produced";
            dgvFiltro.Columns[6].HeaderText = "Downtime";
            dgvFiltro.Columns[7].HeaderText = "Scrap";
            dgvFiltro.Columns[8].HeaderText = "Availability";
            dgvFiltro.Columns[9].HeaderText = "Peformance";
            dgvFiltro.Columns[10].HeaderText = "Quality";
            dgvFiltro.Columns[11].HeaderText = "OEE";
            #endregion

            if (dgvFiltro.Rows.Count - 1 > 0)
            {
                //Aplicar filtro
                consultarEstatusProduccionArea(dgvFiltro);
                dgvEstatusProduccionPorArea = UpdateSizeDgv(dgvEstatusProduccionPorArea);
                dgvEstatusProduccionPorArea = EstablecerColorCeldas(dgvEstatusProduccionPorArea);
                //Calculo de indicadores
                float indicadorDisponibilidad = (float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorArea, 6), 2);
                float indicadorRendimiento = (float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorArea, 7), 2);
                float indicadorCalidad = (float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorArea, 8), 2);
                float indicadorOEE = (float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorArea, 9), 2);
                //Graficacion de os indicadores
                CargarGraficasIndicadores(indicadorDisponibilidad, indicadorRendimiento, indicadorCalidad, indicadorOEE);

                /*------GRAFICAS DE CONTRIBUIDORES-------*/
                //Cargamos la tabla de scrap con su filtro con clausula 'WHERE'
                if (turno != "Todos los turnos")
                    dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "nombreScrap, numPiezas FROM NewRegistroScrap WHERE fecha='" + fecha + "' AND turno='" + turno + "' " +
                    "AND area='" + area + "'").Tables[0];
                else
                    dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "nombreScrap, numPiezas FROM NewRegistroScrap WHERE fecha='" + fecha + "' " +
                    "AND area='" + area + "'").Tables[0];
                dgvRegistroScrap.Columns[0].HeaderText = "Nombre de Scrap";
                dgvRegistroScrap.Columns[1].HeaderText = "Numero de piezas";
                //Sumamos los elementos repetidos del DataGridView
                dgvRegistroScrap = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroScrap);
                //Ordenamos de mayor a menor (Orden descendente)
                DataGridViewColumn numPiezas = dgvRegistroScrap.Columns[1];
                dgvRegistroScrap.Sort(numPiezas, ListSortDirection.Descending);
                dgvRegistroScrap = UpdateSizeDgv(dgvRegistroScrap);
                //Cargamos el contribuidor de scrap
                chartMuestreo1 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroScrap, chartMuestreo1);


                //Cargamos la tabla de tiempo muerto con su filtro con clausula 'WHERE'
                if (turno != "Todos los turnos")
                    dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "razonTiempoMuerto, minutosTiempoMuerto FROM NewRegistroTiempoMuerto WHERE fecha ='" + fecha + "' " +
                    "AND turno ='" + turno + "' AND area='" + area + "'").Tables[0];
                else
                    dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "razonTiempoMuerto, minutosTiempoMuerto FROM NewRegistroTiempoMuerto WHERE fecha ='" + fecha + "' AND " +
                    "area='" + area + "'").Tables[0];
                dgvRegistroTiempoMuerto.Columns[0].HeaderText = "Razon de Tiempo Muerto";
                dgvRegistroTiempoMuerto.Columns[1].HeaderText = "Minutos";
                //Sumamos los elementos repetidos del DataGridView
                dgvRegistroTiempoMuerto = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroTiempoMuerto);
                //Ordenamos de mayor a menor (Orden descendente)
                DataGridViewColumn minutos = dgvRegistroTiempoMuerto.Columns[1];
                dgvRegistroTiempoMuerto.Sort(minutos, ListSortDirection.Descending);
                dgvRegistroTiempoMuerto = UpdateSizeDgv(dgvRegistroTiempoMuerto);
                //Cargamos el contribuidor de tiempo muerto
                chartMuestreo2 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroTiempoMuerto, chartMuestreo2);

                /*CONFIGURACION DE DISPONIBILIDAD DE CONTROLES*/
                btnCapturaPantalla.Enabled = true;
                btnDescargarExcel.Enabled = true;
                btnGenerarPdf.Enabled = true;
            }
            else
            {
                MessageBox.Show("Registros no disponibles");
                btnCapturaPantalla.Enabled = false;
                btnGenerarPdf.Enabled = false;
                btnDescargarExcel.Enabled = false;
            }
        }

        /* -----CONSULTA DE DATOS EN EL DATAGRID-------*/
        public void consultarEstatusProduccionArea(DataGridView objectDataGridView)
        {
            string maquina = "";
            int target = 0, contadorTarget = 0;
            int piecesProduced = 0, contadorPiecesproduced = 0;
            int downTime = 0, contadorDownTime = 0;
            int scrapPieces = 0, contadorScrapPieces = 0;
            int variation = 0;
            float Disponibilidad = 0, contadorDisponibilidad = 0, factorDisponibilidad = 0, promedioDisponibilidad = 0;
            float Rendimiento = 0, contadorRendimiento = 0, factorRendimiento = 0, promedioRendimiento = 0;
            float Calidad = 0, contadorCalidad = 0, factorCalidad = 0, promedioCalidad = 0;
            float OEE = 0, contadorOEE = 0, factorOEE = 0, promedioOEE = 0;
            for (int i = 0; i < objectDataGridView.Rows.Count - 1; i++)
            {
                bool verificado = false;
                maquina = objectDataGridView.Rows[i].Cells[3].Value.ToString();
                for (int z = 0; z < i; z++)
                {
                    if (maquina == objectDataGridView.Rows[z].Cells[3].Value.ToString())
                    {
                        verificado = true;
                        break;
                    }
                }
                if (verificado)
                {
                    continue;
                }
                    int.TryParse(objectDataGridView.Rows[i].Cells[4].Value.ToString(), out target);
                    int.TryParse(objectDataGridView.Rows[i].Cells[5].Value.ToString(), out piecesProduced);
                    int.TryParse(objectDataGridView.Rows[i].Cells[6].Value.ToString(), out downTime);
                    int.TryParse(objectDataGridView.Rows[i].Cells[7].Value.ToString(), out scrapPieces);

                    float.TryParse(objectDataGridView.Rows[i].Cells[8].Value.ToString(), out Disponibilidad); 
                    factorDisponibilidad += 1;

                    float.TryParse(objectDataGridView.Rows[i].Cells[9].Value.ToString(), out Rendimiento); 
                    factorRendimiento += 1;

                    float.TryParse(objectDataGridView.Rows[i].Cells[10].Value.ToString(), out Calidad); 
                    factorCalidad += 1;

                    float.TryParse(objectDataGridView.Rows[i].Cells[11].Value.ToString(), out OEE); 
                    factorOEE += 1;

                    for (int j = 0; j < objectDataGridView.Rows.Count - 1; j++)
                    {
                            
                        if (i != j && maquina == objectDataGridView.Rows[j].Cells[3].Value.ToString())
                        {
                                
                            int.TryParse(objectDataGridView.Rows[j].Cells[4].Value.ToString(), out contadorTarget);
                            int.TryParse(objectDataGridView.Rows[j].Cells[5].Value.ToString(), out contadorPiecesproduced);
                            int.TryParse(objectDataGridView.Rows[j].Cells[6].Value.ToString(), out contadorDownTime);
                            int.TryParse(objectDataGridView.Rows[j].Cells[7].Value.ToString(), out contadorScrapPieces);
                            float.TryParse(objectDataGridView.Rows[j].Cells[8].Value.ToString(), out contadorDisponibilidad);
                            float.TryParse(objectDataGridView.Rows[j].Cells[9].Value.ToString(), out contadorRendimiento);
                            float.TryParse(objectDataGridView.Rows[j].Cells[10].Value.ToString(), out contadorCalidad);
                            float.TryParse(objectDataGridView.Rows[j].Cells[11].Value.ToString(), out contadorOEE);
                            target += contadorTarget;
                            piecesProduced += contadorPiecesproduced;
                            downTime += contadorDownTime;
                            scrapPieces += contadorScrapPieces;
                            Disponibilidad += contadorDisponibilidad; factorDisponibilidad += 1;
                            Rendimiento += contadorRendimiento; factorRendimiento += 1;
                            Calidad += contadorCalidad; factorCalidad += 1;
                            OEE += contadorOEE; factorOEE += 1;
                        }
                    }
                promedioDisponibilidad = (float)Math.Round((Disponibilidad / factorDisponibilidad) * 100, 2);
                promedioRendimiento = (float)Math.Round((Rendimiento / factorRendimiento) * 100, 2);
                promedioCalidad = (float)Math.Round((Calidad / factorCalidad) * 100, 2);
                promedioOEE= (float)Math.Round((OEE/ factorOEE) * 100, 2);
                variation = piecesProduced - target;
                //MessageBox.Show(maquina.ToString());
                dgvEstatusProduccionPorArea.Rows.Add(maquina, target, piecesProduced, variation, downTime, scrapPieces, promedioDisponibilidad, promedioRendimiento, promedioCalidad, promedioOEE);
                factorDisponibilidad = 0; factorRendimiento = 0; factorCalidad = 0; factorOEE = 0;
                promedioDisponibilidad = 0; promedioRendimiento = 0; promedioCalidad = 0; promedioOEE = 0;
                    
                    
            }
        }

        public void CargarGraficasIndicadores(float Disponibilidad, float Rendimiento, float Calidad, float OEE)
        {

            /* CHART DISPONIBILIDAD */
            if (Disponibilidad > 100)
                Disponibilidad = 100;
            chartDisponibilidad.Series[0].Points.AddXY(Disponibilidad.ToString() + "%", Disponibilidad);
            chartDisponibilidad.Series[0].Points.AddXY("", 100 - Disponibilidad);
            chartDisponibilidad.Series[0].Points[0].Color = DefinirColor(Disponibilidad);
            chartDisponibilidad.Series[0].Points[1].Color = Color.Gray;
            chartDisponibilidad.Series[0].IsVisibleInLegend = false;
            /* CHART RENDIMIENTO */
            if (Rendimiento > 100)
                Rendimiento = 100;
            chartRendimiento.Series[0].Points.AddXY(Rendimiento.ToString() + "%", Rendimiento);
            chartRendimiento.Series[0].Points.AddXY("", 100 - Rendimiento);
            chartRendimiento.Series[0].Points[0].Color = DefinirColor(Rendimiento);
            chartRendimiento.Series[0].Points[1].Color = Color.Gray;
            chartRendimiento.Series[0].IsVisibleInLegend = false;
            /* CHART CALIDAD */
            if(Calidad > 100)
                Calidad = 100;
            chartCalidad.Series[0].Points.AddXY(Calidad.ToString() + "%", Calidad);
            chartCalidad.Series[0].Points.AddXY("", 100 - Calidad);
            chartCalidad.Series[0].Points[0].Color = DefinirColor(Calidad);
            chartCalidad.Series[0].Points[1].Color = Color.Gray;
            chartCalidad.Series[0].IsVisibleInLegend = false;
            /* CHART OEE */
            if(OEE > 100)
                OEE = 100;
            chartOEE.Series[0].Points.AddXY(OEE.ToString() + "%", OEE);
            chartOEE.Series[0].Points.AddXY("", 100 - OEE);
            chartOEE.Series[0].Points[0].Color = DefinirColor(OEE);
            chartOEE.Series[0].Points[1].Color = Color.Gray;
            chartOEE.Series[0].IsVisibleInLegend = false;
        }
        public Color DefinirColor(float indicador)
        {

            if (indicador >= 75)
            {
                return Color.LimeGreen;
            }
            else if (indicador < 75 && indicador >= 70)
            {
                return Color.Orange;
            }
            else if (indicador < 70)
            {
                return Color.Red;
            }
            else
            {
                return Color.Gray;
            }

        }

        public DataGridView EstablecerColorCeldas(DataGridView objectDataGridView)
        {
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                for (int j = 6; j <= 9; j++)
                {
                    if (Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) >= 75)
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }
                    else if(Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) < 75 && Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) >= 70)
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                }
            }
            return objectDataGridView;
        }

        private void btnScrap_Click(object sender, EventArgs e)
        {
            ConsultaScrap consultaScrap = new ConsultaScrap(dgvRegistroScrap);
            consultaScrap.Show();
        }

        private void btnTiempoMuerto_Click(object sender, EventArgs e)
        {
            ConsultaTiempoMuerto consultaTiempoMuerto = new ConsultaTiempoMuerto(dgvRegistroTiempoMuerto);
            consultaTiempoMuerto.Show();
        }

        #region FUNCIONES EXCEL
        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            dgvEstatusProduccionPorArea.Columns[2].Name = "Pieces Produced";
            ExportToExcel(dgvEstatusProduccionPorArea);
        }

        public void ExportToExcel(DataGridView objectDataGridView)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int IndiceColumna = 0;

            foreach (DataGridViewColumn col in objectDataGridView.Columns) //Columnas
            {
                IndiceColumna++;
                excel.Cells[1, IndiceColumna] = col.Name;
            }

            int IndiceFila = 0;

            foreach (DataGridViewRow row in objectDataGridView.Rows)    //Filas
            {
                IndiceFila++;
                IndiceColumna = 0;

                foreach (DataGridViewColumn col in objectDataGridView.Columns)
                {
                    IndiceColumna++;
                    excel.Cells[IndiceFila + 1, IndiceColumna] = row.Cells[col.Name].Value;
                }
            }

            excel.Visible = true;
        }
        #endregion

        private void btnCapturaPantalla_Click(object sender, EventArgs e)
        {
            CapturaPantalla capturaPantalla = new CapturaPantalla();
            capturaPantalla.Show();
        }

        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            /*PrintForm documento = new PrintForm();
            documento.Form = this;
            documento.PrinterSettings.DefaultPageSettings.Landscape = true;
            documento.PrintAction = System.Drawing.Printing.PrintAction.PrintToPreview;
            documento.Print(this, PrintForm.PrintOption.FullWindow);
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            Bitmap memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);*/
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte_" + cboArea.Text + "_" + dtFecha.Value.ToString("ddMM") + ".pdf";
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 42, 35);
                    PdfWriter wri = PdfWriter.GetInstance(doc, stream);
                    //Open document to write
                    doc.Open();
                    //Write Title
                    Paragraph paragrahp = new Paragraph("REPORTE DIARIO POR AREA - " + cboArea.Text);
                    //Add paragraph to doc
                    doc.Add(paragrahp);
                    doc.Add(new Paragraph("\n"));

                    #region CREACION DE TABLA PARA DATAGRIDVIEW DE CONSULTA
                    //Create and display the table for DataGridView
                    PdfPTable tblReport = new PdfPTable(10);
                    tblReport.WidthPercentage = 95;
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    //Insertion Table Headers
                    PdfPCell clMachine = new PdfPCell(new Phrase("Machine", _standardFont));
                    clMachine.BorderWidth = 0;
                    clMachine.BorderWidthBottom = 0.75f;
                    PdfPCell clTarget = new PdfPCell(new Phrase("Target", _standardFont));
                    clTarget.BorderWidth = 0;
                    clTarget.BorderWidthBottom = 0.75f;
                    PdfPCell clPiecesProduced = new PdfPCell(new Phrase("Pieces Produced", _standardFont));
                    clPiecesProduced.BorderWidth = 0;
                    clPiecesProduced.BorderWidthBottom = 0.75f;
                    PdfPCell clVariation = new PdfPCell(new Phrase("Variation", _standardFont));
                    clVariation.BorderWidth = 0;
                    clVariation.BorderWidthBottom = 0.75f;
                    PdfPCell clDowntime = new PdfPCell(new Phrase("Downtime", _standardFont));
                    clDowntime.BorderWidth = 0;
                    clDowntime.BorderWidthBottom = 0.75f;
                    PdfPCell clScrap = new PdfPCell(new Phrase("Scrap", _standardFont));
                    clScrap.BorderWidth = 0;
                    clScrap.BorderWidthBottom = 0.75f;
                    PdfPCell clAvailability = new PdfPCell(new Phrase("Availability", _standardFont));
                    clAvailability.BorderWidth = 0;
                    clAvailability.BorderWidthBottom = 0.75f;
                    PdfPCell clPerformance = new PdfPCell(new Phrase("Performance", _standardFont));
                    clPerformance.BorderWidth = 0;
                    clPerformance.BorderWidthBottom = 0.75f;
                    PdfPCell clQuality = new PdfPCell(new Phrase("Quality", _standardFont));
                    clQuality.BorderWidth = 0;
                    clQuality.BorderWidthBottom = 0.75f;
                    PdfPCell clOEE = new PdfPCell(new Phrase("OEE", _standardFont));
                    clOEE.BorderWidth = 0;
                    clOEE.BorderWidthBottom = 0.75f;
                    //Add cells to the table
                    tblReport.AddCell(clMachine);
                    tblReport.AddCell(clTarget);
                    tblReport.AddCell(clPiecesProduced);
                    tblReport.AddCell(clVariation);
                    tblReport.AddCell(clDowntime);
                    tblReport.AddCell(clScrap);
                    tblReport.AddCell(clAvailability);
                    tblReport.AddCell(clPerformance);
                    tblReport.AddCell(clQuality);
                    tblReport.AddCell(clOEE);
                    //Go through the DataGridView
                    for (int i = 0; i < dgvEstatusProduccionPorArea.Rows.Count; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            string cadenaCelda = dgvEstatusProduccionPorArea.Rows[i].Cells[j].Value.ToString();
                            PdfPCell clAdder = new PdfPCell(new Phrase(cadenaCelda, _standardFont));
                            tblReport.AddCell(clAdder);
                        }
                    }
                    //Add table to the doc
                    doc.Add(tblReport);
                    #endregion

                    doc.Add(new Paragraph("\n")); //Salto de línea

                    #region CREACION DE TABLA PARA INDICADORES 
                    PdfPTable tblIndicadores = new PdfPTable(4);
                    tblIndicadores.WidthPercentage = 95;
                    iTextSharp.text.Font fontIndicadores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    //HEADERS
                    PdfPCell clHeaderDisp = new PdfPCell();
                    Paragraph paragraph1 = new Paragraph("Disponibilidad", fontIndicadores);
                    paragraph1.Alignment = Element.ALIGN_CENTER;
                    clHeaderDisp.AddElement(paragraph1);
                    clHeaderDisp.BorderWidth = 0;
                    PdfPCell clHeaderRend = new PdfPCell();
                    Paragraph paragraph2 = new Paragraph("Rendimiento", fontIndicadores);
                    paragraph2.Alignment = Element.ALIGN_CENTER;
                    clHeaderRend.AddElement(paragraph2);
                    clHeaderRend.BorderWidth = 0;
                    PdfPCell clHeaderCalidad = new PdfPCell();
                    Paragraph paragraph3 = new Paragraph("Calidad", fontIndicadores);
                    paragraph3.Alignment = Element.ALIGN_CENTER;
                    clHeaderCalidad.AddElement(paragraph3);
                    clHeaderCalidad.BorderWidth = 0;
                    PdfPCell clHeaderOEE = new PdfPCell();
                    Paragraph paragraph4 = new Paragraph("OEE", fontIndicadores);
                    paragraph4.Alignment = Element.ALIGN_CENTER;
                    clHeaderOEE.AddElement(paragraph4);
                    clHeaderOEE.BorderWidth = 0;
                    //Add Headers to tblIndicadores
                    tblIndicadores.AddCell(clHeaderDisp);
                    tblIndicadores.AddCell(clHeaderRend);
                    tblIndicadores.AddCell(clHeaderCalidad);
                    tblIndicadores.AddCell(clHeaderOEE);
                    //INDICADOR DISPONIBILIDAD
                    var chartImage1 = new MemoryStream();
                    chartDisponibilidad.SaveImage(chartImage1, ChartImageFormat.Png);
                    iTextSharp.text.Image ChartDisponibilidad_image = iTextSharp.text.Image.GetInstance(chartImage1.GetBuffer());
                    ChartDisponibilidad_image.ScalePercent(40f);
                    PdfPCell clIndicador1 = new PdfPCell(ChartDisponibilidad_image);
                    clIndicador1.BorderWidth = 0;
                    //INDICADOR RENDIMIENTO
                    var chartImage2 = new MemoryStream();
                    chartRendimiento.SaveImage(chartImage2, ChartImageFormat.Png);
                    iTextSharp.text.Image ChartRendimiento_image = iTextSharp.text.Image.GetInstance(chartImage2.GetBuffer());
                    ChartRendimiento_image.ScalePercent(40f);
                    PdfPCell clIndicador2 = new PdfPCell(ChartRendimiento_image);
                    clIndicador2.BorderWidth = 0;
                    //INDICADOR CALIDAD
                    var chartImage3 = new MemoryStream();
                    chartCalidad.SaveImage(chartImage3, ChartImageFormat.Png);
                    iTextSharp.text.Image ChartCalidad_image = iTextSharp.text.Image.GetInstance(chartImage3.GetBuffer());
                    ChartCalidad_image.ScalePercent(40f);
                    PdfPCell clIndicador3 = new PdfPCell(ChartCalidad_image);
                    clIndicador3.BorderWidth = 0;
                    //INDICADOR OEE
                    var chartImage4 = new MemoryStream();
                    chartOEE.SaveImage(chartImage4, ChartImageFormat.Png);
                    iTextSharp.text.Image ChartOEE_image = iTextSharp.text.Image.GetInstance(chartImage4.GetBuffer());
                    ChartOEE_image.ScalePercent(40f);
                    PdfPCell clIndicador4 = new PdfPCell(ChartOEE_image);
                    clIndicador4.BorderWidth = 0;
                    //Add indicadores to the table
                    tblIndicadores.AddCell(clIndicador1);
                    tblIndicadores.AddCell(clIndicador2);
                    tblIndicadores.AddCell(clIndicador3);
                    tblIndicadores.AddCell(clIndicador4);
                    //Add second table to the doc
                    doc.Add(tblIndicadores);
                    #endregion

                    doc.Add(new Paragraph("\n")); //Salto de línea

                    #region CREACION DE TABLA PARA CONTRIBUIDORES
                    PdfPTable tblContrubuidores = new PdfPTable(2);
                    tblContrubuidores.WidthPercentage = 95;
                    iTextSharp.text.Font fontContribuidores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    //HEADERS
                    PdfPCell clHeaderScrapContrib = new PdfPCell();
                    Paragraph paragraphScrapContrib = new Paragraph("Contribuidores de Scrap", fontContribuidores);
                    paragraphScrapContrib.Alignment = Element.ALIGN_CENTER;
                    clHeaderScrapContrib.AddElement(paragraphScrapContrib);
                    clHeaderScrapContrib.BorderWidth = 0;
                    PdfPCell clHeaderDowntimeContrib = new PdfPCell();
                    Paragraph paragraphDowntimeContrib = new Paragraph("Contribuidores de Tiempo Muerto", fontContribuidores);
                    paragraphDowntimeContrib.Alignment = Element.ALIGN_CENTER;
                    clHeaderDowntimeContrib.AddElement(paragraphDowntimeContrib);
                    clHeaderDowntimeContrib.BorderWidth = 0;
                    //Add Headers to Table
                    tblContrubuidores.AddCell(clHeaderScrapContrib);
                    tblContrubuidores.AddCell(clHeaderDowntimeContrib);
                    //CONTRIBUIDOR SCRAP
                    var chartImageContribScrap = new MemoryStream();
                    chartMuestreo1.SaveImage(chartImageContribScrap, ChartImageFormat.Png);
                    iTextSharp.text.Image Chart_imageContribScrap = iTextSharp.text.Image.GetInstance(chartImageContribScrap.GetBuffer());
                    Chart_imageContribScrap.ScalePercent(35f);
                    PdfPCell clScrapContrib = new PdfPCell(Chart_imageContribScrap);
                    clScrapContrib.BorderWidth = 0;
                    //CONTRIBUIDOR TIEMPO MUERTO
                    var chartImageContribDowntime = new MemoryStream();
                    chartMuestreo2.SaveImage(chartImageContribDowntime, ChartImageFormat.Png);
                    iTextSharp.text.Image Chart_imageContribDowntime = iTextSharp.text.Image.GetInstance(chartImageContribDowntime.GetBuffer());
                    Chart_imageContribDowntime.ScalePercent(35f);
                    PdfPCell clDowntimeContrib = new PdfPCell(Chart_imageContribDowntime);
                    clDowntimeContrib.BorderWidth = 0;
                    //Add Contribuidores to table
                    tblContrubuidores.AddCell(clScrapContrib);
                    tblContrubuidores.AddCell(clDowntimeContrib);
                    //Add third table to doc
                    doc.Add(tblContrubuidores);
                    #endregion

                    #region ESPECIFICACIONES

                    #endregion
                    //Close the document to finalize the actions
                    doc.Close();
                    //Close stream (memory)
                    stream.Close();
                }

            }
        }

        private void CalcularPiezasMeta(string instruccion)
        {
            DataSet ds = objectConsultaDAL.MostrarDatosCommand(instruccion);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            int Meta = dt.AsEnumerable().Sum(x => x.Field<int>("target"));
            int Produccion = dt.AsEnumerable().Sum(x => x.Field<int>("piecesProduced"));
            lblTarget.Text = "Meta: " + Meta.ToString() + " unidades";
            lblProduction.Text = "Produccion: " + Produccion.ToString() + " unidades";
            //MessageBox.Show(Meta.ToString());
            //MessageBox.Show(Produccion.ToString());
        }
    }
}


