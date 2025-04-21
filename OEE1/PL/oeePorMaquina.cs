using Microsoft.VisualBasic.PowerPacks.Printing;
using OEE1.BLL;
using OEE1.DAL;
using OEE1.PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using iTextSharp.text;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using static iTextSharp.text.TabStop;

namespace OEE1
{
    public partial class oeePorMaquina : Form
    {
        consultaDAL objectConsultaDAL = new consultaDAL();
        RegistroDAL objectRegistroDAL = new RegistroDAL();
        public oeePorMaquina()
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

            /* Carga de datos cboMaquina -> Combo Box para maquinas */
            cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM AreaMaquinas WHERE Area='Fabricacion' AND Maquina!='Todas'").Tables[0];
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboArea.SelectedIndex)
            {
                case 0:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Fabricacion' AND Maquina!='Todas'").Tables[0];
                    break;
                case 1:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM " +
                        "AreaMaquinas WHERE Area='Dobladoras' AND Maquina!='Todas'").Tables[0];
                    break;
                case 2:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Lavadoras' AND Maquina!='Todas'").Tables[0];
                    break;
                case 3:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='EnsambleFinal' AND Maquina!='Todas'").Tables[0];
                    break;
                case 4:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Soldadoras' AND Maquina!='Todas'").Tables[0];
                    break;
                case 5:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Robot SLs' AND Maquina!='Todas'").Tables[0];
                    break;
                default:
                    MessageBox.Show("No se ha hecho ninguna seleccion");
                    break;
            }
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";
        }

        private MaquinaBLL RecuperarInformacion()
        {
            MaquinaBLL objectMaquinaBLL = new MaquinaBLL();
            objectMaquinaBLL.Fecha = dtpFecha.Value.ToString("dd/MM/yyyy");
            objectMaquinaBLL.Turno = cboTurno.Text;
            objectMaquinaBLL.Area = cboArea.Text;
            objectMaquinaBLL.Maquina = cboMaquina.Text;

            return objectMaquinaBLL;
        }

        public DataGridView UpdateSizeDgv(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);
            dgv.BackgroundColor = SystemColors.Control;

            return dgv;
        }

        /* -----Evento boton Calcular-----*/

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            #region LIMPIEZA DE CHARTS
            chartDisponibilidad = objectConsultaDAL.limpiarCharts(chartDisponibilidad);
            chartRendimiento = objectConsultaDAL.limpiarCharts(chartRendimiento);
            chartCalidad = objectConsultaDAL.limpiarCharts(chartCalidad);
            chartOEE = objectConsultaDAL.limpiarCharts(chartOEE);
            chartMuestreo1 = objectConsultaDAL.limpiarCharts(chartMuestreo1);
            chartMuestreo2 = objectConsultaDAL.limpiarCharts(chartMuestreo2);
            #endregion
            /* -----DATAGRIDVIEW FILTRO DE CONSULTA------ */
            MaquinaBLL objectMaquinaBLL = RecuperarInformacion();
            string fecha = objectMaquinaBLL.Fecha;
            string turno = objectMaquinaBLL.Turno;
            string area = objectMaquinaBLL.Area;
            string maquina = objectMaquinaBLL.Maquina;
            if(turno != "Todos los turnos")
            {
                dgvEstatusProduccionPorMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand
                    ("SELECT ID, startTime, endTime, partNumber, " +
                    "target, piecesProduced, variation, downTime, scrapPieces, Disponibilidad, " +
                    "Rendimiento, Calidad, OEE FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND " +
                    "turno='" + turno + "' AND area='" + area + "' AND machine='" + maquina + "'").Tables[0];
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND turno='" + turno + "' " +
                "AND area='" + area + "' AND machine='" + maquina + "'");
            }
            else
            {
                dgvEstatusProduccionPorMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand
                    ("SELECT ID, startTime, endTime, partNumber, " +
                    "target, piecesProduced, variation, downTime, scrapPieces, Disponibilidad, " +
                    "Rendimiento, Calidad, OEE FROM NewOEEPerMachine WHERE fecha='" + fecha + "' AND area='" + area + "' AND " +
                    "machine='" + maquina + "'").Tables[0];
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE fecha='" + fecha + "' " +
                "AND area='" + area + "' AND machine='" + maquina + "'");
            }
            #region CONFIGURACION DE HEADERS
            dgvEstatusProduccionPorMaquina.Columns[0].HeaderText = "Register Number";
            dgvEstatusProduccionPorMaquina.Columns[1].HeaderText = "Start Time";
            dgvEstatusProduccionPorMaquina.Columns[2].HeaderText = "End Time";
            dgvEstatusProduccionPorMaquina.Columns[3].HeaderText = "Part Number";
            dgvEstatusProduccionPorMaquina.Columns[4].HeaderText = "Target";
            dgvEstatusProduccionPorMaquina.Columns[5].HeaderText = "Pieces Produced";
            dgvEstatusProduccionPorMaquina.Columns[6].HeaderText = "Variation";
            dgvEstatusProduccionPorMaquina.Columns[7].HeaderText = "Down Time";
            dgvEstatusProduccionPorMaquina.Columns[8].HeaderText = "Scrap";
            dgvEstatusProduccionPorMaquina.Columns[9].Visible = false;
            dgvEstatusProduccionPorMaquina.Columns[10].Visible = false;
            dgvEstatusProduccionPorMaquina.Columns[11].Visible = false;
            dgvEstatusProduccionPorMaquina.Columns[12].Visible = false;
            #endregion
            dgvEstatusProduccionPorMaquina = UpdateSizeDgv(dgvEstatusProduccionPorMaquina);
            dgvEstatusProduccionPorMaquina = EstablecerColorCeldas(dgvEstatusProduccionPorMaquina);
            if (dgvEstatusProduccionPorMaquina.Rows.Count > 0)
            {
                /* -----INDICADORES------ */
                //Obteniendo indicadores
                float Disponibilidad = ((float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorMaquina, 9), 2) * 100);
                float Rendimiento = ((float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorMaquina, 10), 2) * 100);
                float Calidad = ((float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorMaquina, 11), 2) * 100);
                float OEE = ((float)Math.Round(objectConsultaDAL.CalcularIndicador(dgvEstatusProduccionPorMaquina, 12), 2) * 100);
                //dgvEstatusProduccion.Rows[0].Cells[9].Style.BackColor = Color.Red; <- Pendiente segun interesado
                //Cargando graficas de indicadores
                CargarGraficasIndicadores(Disponibilidad, Rendimiento, Calidad, OEE);


                /*------GRAFICAS DE CONTRIBUIDORES-------*/
                //Cargamos la tabla de scrap con su filtro con clausula 'WHERE'
                if(turno != "Todos los turnos")
                    dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "llave, nombreScrap, numPiezas FROM NewRegistroScrap WHERE fecha='" + fecha + "' AND " +
                    "turno='" + turno + "' AND area='" + area + "' AND machine='" + maquina + "'").Tables[0];
                else
                    dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "llave, nombreScrap, numPiezas FROM NewRegistroScrap WHERE fecha='" + fecha + "' AND area='" + area + "' AND " +
                    "machine='" + maquina + "'").Tables[0];
                dgvRegistroScrap.Columns[0].Visible = false;
                dgvRegistroScrap.Columns[1].HeaderText = "Nombre de Scrap";
                dgvRegistroScrap.Columns[2].HeaderText = "Numero de piezas";
                //Sumamos los elementos repetidos del DataGridview
                dgvRegistroScrap = objectConsultaDAL.SumarElementosRepetidosPorMaquina(dgvRegistroScrap);
                //Ordenamos de mayor a menor (Orden descendente)
                DataGridViewColumn numPiezas = dgvRegistroScrap.Columns[2];
                dgvRegistroScrap.Sort(numPiezas, ListSortDirection.Descending);
                dgvRegistroScrap = UpdateSizeDgv(dgvRegistroScrap);
                //Cargamos el contribuidor de scrap
                chartMuestreo1 = objectConsultaDAL.CargarChartContribuidorPorMaquina(dgvRegistroScrap, chartMuestreo1);

                //Cargamos la tabla de tiempo muerto con su filtro con clausula 'WHERE'
                if (turno != "Todos los turnos")
                    dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "llave, razonTiempoMuerto, minutosTiempoMuerto FROM NewRegistroTiempoMuerto WHERE fecha='" + fecha + "' AND " +
                    "turno='" + turno + "' AND area='" + area + "' AND machine='" + maquina + "'").Tables[0];
                else
                    dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                    "llave, razonTiempoMuerto, minutosTiempoMuerto FROM NewRegistroTiempoMuerto WHERE fecha='" + fecha + "' AND " +
                    "area='" + area + "' AND machine='" + maquina + "'").Tables[0];
                dgvRegistroTiempoMuerto.Columns[0].Visible = false;
                dgvRegistroTiempoMuerto.Columns[1].HeaderText = "Razon de Tiempo Muerto";
                dgvRegistroTiempoMuerto.Columns[2].HeaderText = "Minutos";
                //Sumamos los elementos repetidos
                dgvRegistroTiempoMuerto = objectConsultaDAL.SumarElementosRepetidosPorMaquina(dgvRegistroTiempoMuerto);
                //Ordenamos de mayor a menor (Orden descendente)
                DataGridViewColumn minutosTiempoMuerto = dgvRegistroTiempoMuerto.Columns[2];
                dgvRegistroTiempoMuerto.Sort(minutosTiempoMuerto, ListSortDirection.Descending);
                dgvRegistroTiempoMuerto = UpdateSizeDgv(dgvRegistroTiempoMuerto);
                //Cargamos el contribuidor de tiempo muerto
                chartMuestreo2 = objectConsultaDAL.CargarChartContribuidorPorMaquina(dgvRegistroTiempoMuerto, chartMuestreo2);

                /*CONFIGURACION DE DISPONIBILIDAD DE CONTROLES*/
                btnCapturaPantalla.Enabled = true;
                btnGenerarPdf.Enabled = true;
                btnDescargarExcel.Enabled = true;
            }
            else
            {
                MessageBox.Show("Registros no disponibles");
                btnCapturaPantalla.Enabled = false;
                btnGenerarPdf.Enabled = false;
                btnDescargarExcel.Enabled = false;
            }
        }

        public DataGridView EstablecerColorCeldas(DataGridView objectDataGridView)
        {
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                if (Convert.ToInt32(objectDataGridView.Rows[i].Cells[6].Value) < 0)
                {
                    objectDataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red;
                }
                else
                {
                    objectDataGridView.Rows[i].Cells[6].Style.BackColor = Color.Green;
                }
                for (int j = 7; j <= 8; j++)
                {
                    if (Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) > 0)
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }
                }
            }
            return objectDataGridView;
        }

        public void CargarGraficasIndicadores(float Disponibilidad, float Rendimiento, float Calidad, float OEE)
        {

            /* CHART DISPONIBILIDAD */
            if(Disponibilidad > 100)
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
            
            if(indicador >= 76)
            {
                return Color.LimeGreen;
            }
            else if(indicador < 76 && indicador >= 70)
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

        #region BOTONES DE CONSULTA PARA CONTRIBUIDORES
        private void btnScrap_Click(object sender, EventArgs e)
        {
            ConsultaScrap consultaScrap = new ConsultaScrap(dgvRegistroScrap, "Por Maquina");
            consultaScrap.Show();
        }

        private void btnTiempoMuerto_Click(object sender, EventArgs e)
        {
            ConsultaTiempoMuerto consultaTiempoMuerto = new ConsultaTiempoMuerto(dgvRegistroTiempoMuerto, "Por Maquina");
            consultaTiempoMuerto.Show();
        }
        #endregion

        #region FUNCIONES EXCEL
        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            dgvEstatusProduccionPorMaquina.Columns[0].Name = "ID";
            dgvEstatusProduccionPorMaquina.Columns[1].Name = "Start Time";
            dgvEstatusProduccionPorMaquina.Columns[2].Name = "End Time";
            dgvEstatusProduccionPorMaquina.Columns[3].Name = "Part Number";
            dgvEstatusProduccionPorMaquina.Columns[4].Name = "Target";
            dgvEstatusProduccionPorMaquina.Columns[5].Name = "Pieces Produced";
            dgvEstatusProduccionPorMaquina.Columns[6].Name = "Variation";
            dgvEstatusProduccionPorMaquina.Columns[7].Name = "Downtime";
            dgvEstatusProduccionPorMaquina.Columns[8].Name = "Scrap Pieces";
            ExportToExcel(dgvEstatusProduccionPorMaquina);
            
        }
        
        public void ExportToExcel(DataGridView objectDataGridView)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int IndiceColumna = 0;

            foreach(DataGridViewColumn col in objectDataGridView.Columns) //Columnas
            {
                IndiceColumna++;
                excel.Cells[1, IndiceColumna] = col.Name;
            }

            int IndiceFila = 0;

            foreach (DataGridViewRow row in objectDataGridView.Rows)    //Filas
            {
                IndiceFila++;
                IndiceColumna = 0;

                foreach(DataGridViewColumn col in objectDataGridView.Columns)
                {
                    IndiceColumna++;
                    excel.Cells[IndiceFila + 1, IndiceColumna] = row.Cells[col.Name].Value;
                }
            }

            excel.Visible = true;
        }

        #endregion

        #region FUNCIONES PDF
        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte_" + cboMaquina.Text + "_" + dtpFecha.Value.ToString("ddMM") + ".pdf";
            if(guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                    PdfWriter wri = PdfWriter.GetInstance(doc, stream);
                    //Open document to write
                    doc.Open(); 
                    //Write Title
                    Paragraph paragrahp = new Paragraph("REPORTE DIARIO POR MAQUINA - " + cboMaquina.Text);
                    //Add paragraph to doc
                    doc.Add(paragrahp);
                    doc.Add(new Paragraph("\n"));

                    #region CREACION DE TABLA PARA DATAGRIDVIEW DE CONSULTA
                    //Create and display the table for DataGridView
                    PdfPTable tblReport = new PdfPTable(12);
                    tblReport.WidthPercentage = 95;
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    //Insertion Table Headers
                    PdfPCell clStartTime = new PdfPCell(new Phrase("Start Time", _standardFont));
                    clStartTime.BorderWidth = 0;
                    clStartTime.BorderWidthBottom = 0.75f;
                    PdfPCell clEndTime = new PdfPCell(new Phrase("End Time", _standardFont));
                    clEndTime.BorderWidth = 0;
                    clEndTime.BorderWidthBottom = 0.75f;
                    PdfPCell clPartNumber = new PdfPCell(new Phrase("Part Number", _standardFont));
                    clPartNumber.BorderWidth = 0;
                    clPartNumber.BorderWidthBottom = 0.75f;
                    PdfPCell clTarget = new PdfPCell(new Phrase("Target", _standardFont));
                    clTarget.BorderWidth = 0;
                    clTarget.BorderWidthBottom = 0.75f;
                    PdfPCell clPiecesProduced = new PdfPCell(new Phrase("Pieces Produced", _standardFont));
                    clPiecesProduced.BorderWidth = 0;
                    clPiecesProduced.BorderWidthBottom = 0.75f;
                    PdfPCell clVariation = new PdfPCell(new Phrase("Variation", _standardFont));
                    clVariation.BorderWidth = 0;
                    clVariation.BorderWidthBottom = 0.75f;
                    PdfPCell clDownTime = new PdfPCell(new Phrase("DownTime", _standardFont));
                    clDownTime.BorderWidth = 0;
                    clDownTime.BorderWidthBottom = 0.75f;
                    PdfPCell clScrap = new PdfPCell(new Phrase("Scrap", _standardFont));
                    clScrap.BorderWidth = 0;
                    clScrap.BorderWidthBottom = 0.75f;
                    PdfPCell clDisponibilidad = new PdfPCell(new Phrase("Disponibilidad", _standardFont));
                    clDisponibilidad.BorderWidth = 0;
                    clDisponibilidad.BorderWidthBottom = 0.75f;
                    PdfPCell clRendimiento = new PdfPCell(new Phrase("Rendimiento", _standardFont));
                    clRendimiento.BorderWidth = 0;
                    clRendimiento.BorderWidthBottom = 0.75f;
                    PdfPCell clCalidad = new PdfPCell(new Phrase("Calidad", _standardFont));
                    clCalidad.BorderWidth = 0;
                    clCalidad.BorderWidthBottom = 0.75f;
                    PdfPCell clOEE = new PdfPCell(new Phrase("OEE", _standardFont));
                    clOEE.BorderWidth = 0;
                    clOEE.BorderWidthBottom = 0.75f;
                    //Add cells to the table
                    tblReport.AddCell(clStartTime);
                    tblReport.AddCell(clEndTime);
                    tblReport.AddCell(clPartNumber);
                    tblReport.AddCell(clTarget);
                    tblReport.AddCell(clPiecesProduced);
                    tblReport.AddCell(clVariation);
                    tblReport.AddCell(clDownTime);
                    tblReport.AddCell(clScrap);
                    tblReport.AddCell(clDisponibilidad);
                    tblReport.AddCell(clRendimiento);
                    tblReport.AddCell(clCalidad);
                    tblReport.AddCell(clOEE); 
                    //Go through the DataGridView
                    for(int i = 0; i < dgvEstatusProduccionPorMaquina.Rows.Count; i++)
                    {
                        for(int j = 1; j < 13; j++)
                        {
                            string cadenaCelda = dgvEstatusProduccionPorMaquina.Rows[i].Cells[j].Value.ToString();
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
        #endregion

        private void btnCapturaPantalla_Click(object sender, EventArgs e)
        {
            CapturaPantalla capturaPantalla = new CapturaPantalla();
            capturaPantalla.Show();
        }

        private void dgvEstatusProduccionPorMaquina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int registerNumber = Convert.ToInt32(dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[0].Value.ToString());
                string horaInicial = dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[1].Value.ToString();
                string horaFinal = dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[2].Value.ToString();
                string numeroParte = dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[3].Value.ToString();
                string fecha = dtpFecha.Value.ToString("dd/MM/yyyy");
                string turno = cboTurno.Text;
                string area = cboArea.Text;
                string maquina = cboMaquina.Text;
                int target = Convert.ToInt32(dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[4].Value);
                int piezasProducidas = Convert.ToInt32(dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[5].Value);
                int minutosTiempoMuerto = Convert.ToInt32(dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[7].Value);
                int piezasScrap = Convert.ToInt32(dgvEstatusProduccionPorMaquina.Rows[dgvEstatusProduccionPorMaquina.CurrentRow.Index].Cells[8].Value);
                PantallaModificacion pantallaModificacion = new PantallaModificacion(registerNumber, fecha, turno, area, maquina, horaInicial, horaFinal, numeroParte, target, piezasProducidas, minutosTiempoMuerto, piezasScrap);
                pantallaModificacion.Show();
                e.Handled = true;
            }
        }

        #region FUNCIONES PARA ELIMINAR REGISTROS
        public int llave {  get; set; }
        private void dgvEstatusProduccionPorMaquina_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.ColumnIndex < 0)
            {
                dgvEstatusProduccionPorMaquina.Rows[e.RowIndex].Selected = true;
                dgvEstatusProduccionPorMaquina.CurrentCell = dgvEstatusProduccionPorMaquina.Rows[e.RowIndex].Cells[0];
                contextMenuStrip1.Show(dgvEstatusProduccionPorMaquina, e.Location);
                contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
                llave = Convert.ToInt32(dgvEstatusProduccionPorMaquina.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void opcEliminar_Click(object sender, EventArgs e)
        {
            objectRegistroDAL.EliminarRegistros(llave);
            btnCalcular_Click(sender, e);
        }
        #endregion

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
