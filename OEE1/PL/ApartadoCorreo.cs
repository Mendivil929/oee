// Namespaces for Pdf
using iTextSharp.text.pdf;
using iTextSharp.text;
// Namespaces for PowerPoint
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;
using P = DocumentFormat.OpenXml.Presentation;
//Namespaces for Outlook
using Outlook = Microsoft.Office.Interop.Outlook;
// Namespaces for all Drawings
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
//Namespaces for Forms
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
//Namespaces for SQL 
using System.Data.SqlClient;
// Local Namespaces
using OEE1.DAL;
using System.Linq;

namespace OEE1.PL
{
    public partial class ApartadoCorreo : Form
    {
        consultaDAL objectConsultaDAL;
        float OEEAcumulado = 0;
        string sqlConnectionString = "server=PDNPD014; database=OEEDB; integrated security=false; user=AdminOEE; password=AdminOEE";
        SqlConnection conn;
        SqlCommand comando = new SqlCommand();
        public ApartadoCorreo()
        {
            conn = new SqlConnection(sqlConnectionString);
            InitializeComponent();
            objectConsultaDAL = new consultaDAL();
            cargarComboBoxes();
            GenerarDatos();
            CrearPresentacionOEEPortadaDesdePlantilla("C:\\Users\\imendivi\\Downloads\\PresentacionValidaBase.pptx", OEEAcumulado, chartEstatusMensual, chartAcumuladoMensual);
            //crearPdf();
            //correo.enviarCorreo();
        }
        public void cargarComboBoxes()
        {
            /* Carga de datos cboArea -> Combo Box para especificar el area */
            cboArea.DataSource = objectConsultaDAL.MostrarDatos("nombreAreas").Tables[0];
            cboArea.DisplayMember = "area";
            cboArea.ValueMember = "area";

            /* Carga de datos cboMaquina -> Combo Box para maquinas */
            cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM AreaMaquinas WHERE Area='Fabricacion' " +
                "AND Maquina!='Todas'").Tables[0];
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";
        }

        public DataGridView UpdateSizeDgv(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);

            return dgv;
        }

        public void GenerarDatos()
        {
            int diasMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            dgvEstatusMensual = EstablecerDiasDelMes(dgvEstatusMensual, diasMes);
            ConsultaPorPlanta(diasMes);
            chartEstatusMensual = CargarChartEstatusMensual(dgvEstatusMensual, chartEstatusMensual);
            chartAcumuladoMensual = CargarChartAcumuladoMensual(dgvEstatusMensual, chartAcumuladoMensual);
        }

        public int enviarCorreo()
        {
            try
            {
                //Create the Outlook application by using inline initialization.
                Outlook.Application oApp = new Outlook.Application();
                //Create the new message by using the simplest approach.
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                // OBSERVACIONES -> Para cambiar de remitente es necesario que la cuenta deseada este guardada en el outlook de la
                // maquina local
                oMsg.SendUsingAccount = GetAccountForEmailAddress(oApp, "IMENDIVI@hanonsystems.com");   // El correo escrito si se encuentra en el
                //Add a recipient.                                                                      // outlook de la maquina local
                //TODO: Change the following recipient where appropriate.
                Outlook.Recipient oRecip = (Outlook.Recipient)oMsg.Recipients.Add("imendivi@hanonsystems.com");
                ////oRecip = oMsg.Recipients.Add("al177102@alumnos.uacj.mx");
                ////oRecip = oMsg.Recipients.Add("al182072@alumnos.uacj.mx");
                ////oRecip = oMsg.Recipients.Add("clopezpa@hanonsystems.com");
                oRecip.Resolve();

                //Set the basic properties.
                oMsg.Subject = "Reporte diario de OEE";
                oMsg.Body = "Buen día\nReporte diario de OEE es anexado\nBest Wishes";

                //Add an attachment.
                // TODO: change file path where appropriate
                String sSource = @"\\pdnpd014\InventarioIT\OEE App_Nueva\Reporte Mensual_Planta - Todas_11-2024.pdf";
                String sDisplayName = "ReporteOEE";
                int iPosition = (int)oMsg.Body.Length + 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                Outlook.Attachment oAttach = oMsg.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);

                // If you want to, display the message
                // oMsg.Display(true);  //modal

                //Send the message
                oMsg.Save();
                oMsg.Send();

                //Explicitly release objects.
                //oRecip = null;
                oAttach = null;
                oMsg = null;
            }
            // Simple error handler
            catch (System.Exception e)
            {
                MessageBox.Show("{0} Exception caught: ", e.ToString());
            }
            //Default return value
            return 0;
        }

        public void crearPdf()
        {
            DateTime fecha = DateTime.Now;
            //float indicadorDisponibilidad = 0, indicadorRendimiento = 0, indicadorCalidad = 0, indicadorOEE = 0;
            //string[] areas = { "Fabricacion", "Dobladoras", "Lavadoras", "Ensamble Final", "Soldadoras", "Robot SLs" };
            //string[] turnos = { "1er Turno", "2do Turno" };
            //CREAMOS EL PDF A TRABAJAR
            string reportPath = "Reporte_" + fecha.ToString("MM") + "" + fecha.ToString("dd") + ".pdf";
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(userProfile, "Downloads");
            string reportFile = Path.Combine(downloadsPath, reportPath);
            //MessageBox.Show(reportFile);
            FileStream stream = new FileStream(reportFile, FileMode.Create);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, stream);

            //Open document to write
            doc.Open();

            //Add Header Image
            insertarEncabezado(doc);

            //Write Title
            #region QUE DIA DE LA SEMANA ES
            int verificadorDia = -1;
            if (fecha.ToString("ddd") == "Lun")
                verificadorDia = -3;
            #endregion
            Paragraph paragrahp = new Paragraph("Planta - " + fecha.AddDays(verificadorDia).ToString("dd/MMM/yyyy"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            //Add paragraph to doc
            doc.Add(paragrahp);
            doc.Add(new Paragraph("\n"));
            
            //SECCIÓN PARA OEE FABRICACION

            generarContenido(doc, wri);
            ////  PRIMER TURNO  //
            ////Loop para cargar las areas en el pdf de los 3 turnos
            //foreach (string turno in turnos)
            //{
            //    doc.Add(new Paragraph(turno));
            //    doc.Add(new Paragraph("\n"));
            //    foreach (string area in areas)
            //    {
            //        Paragraph areaTitle = new Paragraph(area);
            //        doc.Add(areaTitle);
            //        doc.Add(new Paragraph("\n"));
            //        //Estatus de maquinas e indicadores generales por area
            //        dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT fecha, turno, area, machine, target, piecesProduced, " +
            //            "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
            //            "OEE FROM OEEPerMachine WHERE fecha='" + fecha.AddDays(-1).ToString("dd/MM/yyyy") + "' AND turno='" + turno + "' " +
            //            "AND area='" + area + "'").Tables[0];
            //        #region CONFIGURACION DE HEADERS
            //        dgvFiltro.Columns[0].HeaderText = "Fecha";
            //        dgvFiltro.Columns[1].HeaderText = "Turno";
            //        dgvFiltro.Columns[2].HeaderText = "Area";
            //        dgvFiltro.Columns[3].HeaderText = "Machine";
            //        dgvFiltro.Columns[4].HeaderText = "Target";
            //        dgvFiltro.Columns[5].HeaderText = "Pieces Produced";
            //        dgvFiltro.Columns[6].HeaderText = "Downtime";
            //        dgvFiltro.Columns[7].HeaderText = "Scrap";
            //        dgvFiltro.Columns[8].HeaderText = "Availability";
            //        dgvFiltro.Columns[9].HeaderText = "Peformance";
            //        dgvFiltro.Columns[10].HeaderText = "Quality";
            //        dgvFiltro.Columns[11].HeaderText = "OEE";
            //        #endregion
            //        consultarEstatusProduccionArea(dgvFiltro);
            //        indicadorDisponibilidad = (float)Math.Round(CalcularIndicador(6), 2);
            //        indicadorRendimiento = (float)Math.Round(CalcularIndicador(7), 2);
            //        indicadorCalidad = (float)Math.Round(CalcularIndicador(8), 2);
            //        indicadorOEE = (float)Math.Round(CalcularIndicador(9), 2);
            //        //Estatus de Scrap
            //        dgvScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
            //            "nombreScrap, numPiezas FROM RegistroScrap WHERE fecha='" + fecha.AddDays(-1).ToString("dd/MM/yyyy") + "' AND turno='" + turno + "' " +            //            "AND area='" + area + "'").Tables[0]; // INTEGRA LA INFORMACION DE SCRAP Y TIEMPO MUERTO Y YA
            //        dgvScrap = objectConsultaDAL.SumarElementosRepetidos(dgvScrap);
            //        DataGridViewColumn numPiezas = dgvScrap.Columns[1];
            //        dgvScrap.Sort(numPiezas, ListSortDirection.Descending);
            //        //Estatus de tiempo muerto
            //        dgvDowntime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
            //            "razonTiempoMuerto, minutosTiempoMuerto FROM RegistroTiempoMuerto WHERE fecha='" + fecha.AddDays(-1).ToString("dd/MM/yyyy") + "' AND turno='" + turno + "' " +
            //            "AND area='" + area + "'").Tables[0]; // INTEGRA LA INFORMACION DE SCRAP Y TIEMPO MUERTO Y YA
            //        dgvDowntime = objectConsultaDAL.SumarElementosRepetidos(dgvDowntime);
            //        DataGridViewColumn minutos = dgvDowntime.Columns[1];
            //        dgvDowntime.Sort(minutos, ListSortDirection.Descending);
            //        generarPdf(indicadorDisponibilidad, indicadorRendimiento, indicadorCalidad, indicadorOEE, doc, wri);
            //        dgvEstatusProduccionPorArea.Rows.Clear();
            //    }
            //}
            doc.Close();
            stream.Close();
        }


        private void generarContenido(/*float indicadorDisponibilidad, float indicadorRendimiento, float indicadorCalidad, float indicadorOEE,*/ Document doc, PdfWriter wri)
        {
            try
            {
                //Mi estilo predeterminado de texto
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                //Mi estilo para texto resaltado
                iTextSharp.text.Font acumuladoFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK);

                //GENERANDO GRAFICA DE OEE DIARIO DE LA PLANTA
                PdfPTable tblPlanta = new PdfPTable(2);
                tblPlanta.WidthPercentage = 95;
                PdfPCell clOEEDiarioGeneral = new PdfPCell(new Phrase("OEE Diario General", _standardFont));
                clOEEDiarioGeneral.BorderWidth = 0;
                clOEEDiarioGeneral.HorizontalAlignment = 1;
                tblPlanta.AddCell(clOEEDiarioGeneral);
                PdfPCell clTextoOEEAcumulado = new PdfPCell(new Phrase("OEE Acumulado General", _standardFont));
                clTextoOEEAcumulado.BorderWidth = 0;
                clTextoOEEAcumulado.HorizontalAlignment = 1;
                tblPlanta.AddCell(clTextoOEEAcumulado);
                PdfPCell clGraficaOEEDiarioGeneral = new PdfPCell(SetChart(chartEstatusMensual));
                clGraficaOEEDiarioGeneral.BorderWidth = 0;
                clGraficaOEEDiarioGeneral.HorizontalAlignment = 1;
                tblPlanta.AddCell(clGraficaOEEDiarioGeneral);
                #region CONDICIONALES PARA EL COLOR DEL OEE ACUMULADO GENERAL
                Paragraph texto = new Paragraph("Texto");
                if (OEEAcumulado < 70)
                    acumuladoFont.Color = BaseColor.RED;
                else if (OEEAcumulado < 76 && OEEAcumulado >= 70)
                    acumuladoFont.Color = BaseColor.ORANGE;
                else if(OEEAcumulado >= 76)
                    acumuladoFont.Color = BaseColor.GREEN;
                #endregion
                PdfPCell clOEEAcumulado = new PdfPCell(new Phrase(OEEAcumulado.ToString() + "%", acumuladoFont));
                clOEEAcumulado.BorderWidth = 0;
                clOEEAcumulado.HorizontalAlignment = 1;
                tblPlanta.AddCell(clOEEAcumulado);
                doc.Add(tblPlanta);

                //GENERANDO GRAFICA DE OEE DIARIO POR AREAS
                PdfPTable tblAreas = new PdfPTable(1);
                tblAreas.WidthPercentage = 95;
                PdfPCell clAcumuladoPorArea = new PdfPCell(new Phrase("Acumulado por Area", _standardFont));
                clAcumuladoPorArea.BorderWidth = 0;
                clAcumuladoPorArea.HorizontalAlignment = 1;
                PdfPCell clGraficaAcumuladoPorArea = new PdfPCell(SetChart(chartAcumuladoMensual));
                clGraficaAcumuladoPorArea.BorderWidth = 0;
                clGraficaAcumuladoPorArea.HorizontalAlignment = 1;
                tblAreas.AddCell(clAcumuladoPorArea);
                tblAreas.AddCell(clGraficaAcumuladoPorArea);
                doc.Add(tblAreas);

                //GENERANDO GRAFICAS DE FABRICACION
                iTextSharp.text.Font titlesFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                doc.Add(new Phrase("Fabricacion " + DateTime.Now.AddDays(-1).ToString("dd/MMM"), titlesFont));
                PdfPTable tblFabricacion = new PdfPTable(3);
                tblFabricacion.WidthPercentage = 95;
                PdfPCell clTurno1Fabricacion = new PdfPCell(new Phrase("1er Turno", _standardFont));
                clTurno1Fabricacion.BorderWidth = 0;
                clTurno1Fabricacion.HorizontalAlignment = 1;
                tblFabricacion.AddCell(clTurno1Fabricacion);
                PdfPCell clTurno2Fabricacion = new PdfPCell(new Phrase("2do Turno", _standardFont));
                clTurno2Fabricacion.BorderWidth = 0;
                clTurno2Fabricacion.HorizontalAlignment = 1;
                tblFabricacion.AddCell(clTurno2Fabricacion);
                PdfPCell clTotalTurnos = new PdfPCell(new Phrase("Total", _standardFont));
                clTotalTurnos.BorderWidth = 0;
                clTotalTurnos.HorizontalAlignment = 1;
                tblFabricacion.AddCell(clTotalTurnos);

                doc.Add(tblFabricacion);
                //PdfPCell clPiecesProduced = new PdfPCell(new Phrase("Pieces Produced", _standardFont));
                //clPiecesProduced.BorderWidth = 0;
                //clPiecesProduced.BorderWidthBottom = 0.75f;
                //PdfPCell clVariation = new PdfPCell(new Phrase("Variation", _standardFont));
                //clVariation.BorderWidth = 0;
                //clVariation.BorderWidthBottom = 0.75f;
                //PdfPCell clDowntime = new PdfPCell(new Phrase("Downtime", _standardFont));
                //clDowntime.BorderWidth = 0;
                //clDowntime.BorderWidthBottom = 0.75f;
                //PdfPCell clScrap = new PdfPCell(new Phrase("Scrap", _standardFont));
                //clScrap.BorderWidth = 0;
                //clScrap.BorderWidthBottom = 0.75f;
                //PdfPCell clAvailability = new PdfPCell(new Phrase("Availability", _standardFont));
                //clAvailability.BorderWidth = 0;
                //clAvailability.BorderWidthBottom = 0.75f;
                //PdfPCell clPerformance = new PdfPCell(new Phrase("Performance", _standardFont));
                //clPerformance.BorderWidth = 0;
                //clPerformance.BorderWidthBottom = 0.75f;
                //PdfPCell clQuality = new PdfPCell(new Phrase("Quality", _standardFont));
                //clQuality.BorderWidth = 0;
                //clQuality.BorderWidthBottom = 0.75f;
                //PdfPCell clOEE = new PdfPCell(new Phrase("OEE", _standardFont));
                //clOEE.BorderWidth = 0;
                //clOEE.BorderWidthBottom = 0.75f;
                //Add cells to the table


                //tblReport.AddCell(clPiecesProduced);
                //tblReport.AddCell(clVariation);
                //tblReport.AddCell(clDowntime);
                //tblReport.AddCell(clScrap);
                //tblReport.AddCell(clAvailability);
                //tblReport.AddCell(clPerformance);
                //tblReport.AddCell(clQuality);
                //tblReport.AddCell(clOEE);
                //Scanning DataGridView Estatus de Producción por Area
                //for (int i = 0; i < dgvEstatusProduccionPorArea.Rows.Count; i++)
                //{
                //    for (int j = 0; j < 10; j++)
                //    {
                //        string cadenaCelda = dgvEstatusProduccionPorArea.Rows[i].Cells[j].Value.ToString();
                //        PdfPCell clAdder = new PdfPCell(new Phrase(cadenaCelda, _standardFont));
                //        if (j > 5)
                //        {
                //            clAdder.HorizontalAlignment = 1;
                //            float evaluador; float.TryParse(dgvEstatusProduccionPorArea.Rows[i].Cells[j].Value.ToString(), out evaluador);
                //            if (evaluador < 75)
                //                clAdder.BackgroundColor = BaseColor.RED;
                //            else if (evaluador >= 75 && evaluador < 80)
                //                clAdder.BackgroundColor = BaseColor.YELLOW;
                //            else
                //                clAdder.BackgroundColor = BaseColor.GREEN;
                //        }
                //        tblPlanta.AddCell(clAdder);
                //    }
                //}

                //Add table to the doc

                #region TABLA CONTENEDORA DE SUBTABLAS -> Scrap y Tiempo muerto
                //PdfPTable tblContenedor = new PdfPTable(2);
                //tblContenedor.DefaultCell.BorderWidth = 0;
                //#region SUBTABLA SCRAP
                ////CREACION DE TABLA
                //PdfPTable tblScrap = new PdfPTable(2);
                //tblScrap.WidthPercentage = 47.5f;
                //tblScrap.DefaultCell.BorderWidth = 0;
                ////CELDAS
                //PdfPCell clRazonScrap = new PdfPCell(new Phrase("Razon de Scrap", _standardFont));
                //clRazonScrap.BorderWidth = 0;
                //clRazonScrap.BorderWidthBottom = 0.75f;
                //PdfPCell clPiezasScrap = new PdfPCell(new Phrase("Numero de Piezas", _standardFont));
                //clPiezasScrap.BorderWidth = 0;
                //clPiezasScrap.BorderWidthBottom = 0.75f;
                ////Add Cells to Scrap Table
                //tblScrap.AddCell(clRazonScrap);
                //tblScrap.AddCell(clPiezasScrap);
                ////Scanning DataGridView Scrap
                //for (int i = 0; i < dgvScrap.Rows.Count; i++)
                //{
                //    string razonScrap = dgvScrap.Rows[i].Cells[0].Value.ToString();
                //    string piezasScrap = dgvScrap.Rows[i].Cells[1].Value.ToString();
                //    PdfPCell clRazonScrapAdder = new PdfPCell(new Phrase(razonScrap, _standardFont));
                //    clRazonScrapAdder.BorderWidth = 0;
                //    PdfPCell clPiezasScrapAdder = new PdfPCell(new Phrase(piezasScrap, _standardFont));
                //    clPiezasScrapAdder.BorderWidth = 0;
                //    tblScrap.AddCell(clRazonScrapAdder);
                //    tblScrap.AddCell(clPiezasScrapAdder);
                //}
                //#endregion
                //tblContenedor.AddCell(tblScrap);
                //#region SUBTABLA TIEMPO MUERTO
                ////CREACION DE TABLA
                //PdfPTable tblDowntime = new PdfPTable(2);
                //tblDowntime.WidthPercentage = 47.5f;
                ////CELDAS
                //PdfPCell clRazonDowntime = new PdfPCell(new Phrase("Razon de Tiempo Muerto", _standardFont));
                //clRazonDowntime.BorderWidth = 0;
                //clRazonDowntime.BorderWidthBottom = 0.75f;
                //PdfPCell clMinutos = new PdfPCell(new Phrase("Minutos", _standardFont));
                //clMinutos.BorderWidth = 0;
                //clMinutos.BorderWidthBottom = 0.75f;
                ////Add Cells to Scrap Table
                //tblDowntime.AddCell(clRazonDowntime);
                //tblDowntime.AddCell(clMinutos);
                ////Scanning DataGridView Scrap
                //for (int i = 0; i < dgvDowntime.Rows.Count; i++)
                //{
                //    string razonDowntime = dgvDowntime.Rows[i].Cells[0].Value.ToString();
                //    string minutos = dgvDowntime.Rows[i].Cells[1].Value.ToString();
                //    PdfPCell clRazonDowntimeAdder = new PdfPCell(new Phrase(razonDowntime, _standardFont));
                //    clRazonDowntimeAdder.BorderWidth = 0;
                //    PdfPCell clMinutosAdder = new PdfPCell(new Phrase(minutos, _standardFont));
                //    clMinutosAdder.BorderWidth = 0;
                //    tblDowntime.AddCell(clRazonDowntimeAdder);
                //    tblDowntime.AddCell(clMinutosAdder);
                //}
                //#endregion
                //tblContenedor.AddCell(tblDowntime);

                //doc.Add(tblContenedor);
                #endregion

                #region CREACION DE TABLA PARA INDICADORES 
                //PdfPTable tblIndicadores = new PdfPTable(4);
                //tblIndicadores.WidthPercentage = 95;
                //iTextSharp.text.Font fontIndicadores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                ////HEADERS
                //PdfPCell clHeaderDisp = new PdfPCell();
                //Paragraph paragraph1 = new Paragraph("Disponibilidad", fontIndicadores);
                //paragraph1.Alignment = Element.ALIGN_CENTER;
                //clHeaderDisp.AddElement(paragraph1);
                //clHeaderDisp.BorderWidth = 0;
                //PdfPCell clHeaderRend = new PdfPCell();
                //Paragraph paragraph2 = new Paragraph("Rendimiento", fontIndicadores);
                //paragraph2.Alignment = Element.ALIGN_CENTER;
                //clHeaderRend.AddElement(paragraph2);
                //clHeaderRend.BorderWidth = 0;
                //PdfPCell clHeaderCalidad = new PdfPCell();
                //Paragraph paragraph3 = new Paragraph("Calidad", fontIndicadores);
                //paragraph3.Alignment = Element.ALIGN_CENTER;
                //clHeaderCalidad.AddElement(paragraph3);
                //clHeaderCalidad.BorderWidth = 0;
                //PdfPCell clHeaderOEE = new PdfPCell();
                //Paragraph paragraph4 = new Paragraph("OEE", fontIndicadores);
                //paragraph4.Alignment = Element.ALIGN_CENTER;
                //clHeaderOEE.AddElement(paragraph4);
                //clHeaderOEE.BorderWidth = 0;
                ////Add Headers to tblIndicadores
                //tblIndicadores.AddCell(clHeaderDisp);
                //tblIndicadores.AddCell(clHeaderRend);
                //tblIndicadores.AddCell(clHeaderCalidad);
                //tblIndicadores.AddCell(clHeaderOEE);
                ////INDICADOR DISPONIBILIDAD
                //var chartImage1 = new MemoryStream();
                //chartDisponibilidad.SaveImage(chartImage1, ChartImageFormat.Png);
                //iTextSharp.text.Image ChartDisponibilidad_image = iTextSharp.text.Image.GetInstance(chartImage1.GetBuffer());
                //ChartDisponibilidad_image.ScalePercent(40f);
                //PdfPCell clIndicador1 = new PdfPCell(ChartDisponibilidad_image);
                //clIndicador1.BorderWidth = 0;
                ////INDICADOR RENDIMIENTO
                //var chartImage2 = new MemoryStream();
                //chartRendimiento.SaveImage(chartImage2, ChartImageFormat.Png);
                //iTextSharp.text.Image ChartRendimiento_image = iTextSharp.text.Image.GetInstance(chartImage2.GetBuffer());
                //ChartRendimiento_image.ScalePercent(40f);
                //PdfPCell clIndicador2 = new PdfPCell(ChartRendimiento_image);
                //clIndicador2.BorderWidth = 0;
                ////INDICADOR CALIDAD
                //var chartImage3 = new MemoryStream();
                //chartCalidad.SaveImage(chartImage3, ChartImageFormat.Png);
                //iTextSharp.text.Image ChartCalidad_image = iTextSharp.text.Image.GetInstance(chartImage3.GetBuffer());
                //ChartCalidad_image.ScalePercent(40f);
                //PdfPCell clIndicador3 = new PdfPCell(ChartCalidad_image);
                //clIndicador3.BorderWidth = 0;
                ////INDICADOR OEE
                //var chartImage4 = new MemoryStream();
                //chartOEE.SaveImage(chartImage4, ChartImageFormat.Png);
                //iTextSharp.text.Image ChartOEE_image = iTextSharp.text.Image.GetInstance(chartImage4.GetBuffer());
                //ChartOEE_image.ScalePercent(40f);
                //PdfPCell clIndicador4 = new PdfPCell(ChartOEE_image);
                //clIndicador4.BorderWidth = 0;
                ////Add indicadores to the table
                //tblIndicadores.AddCell(clIndicador1);
                //tblIndicadores.AddCell(clIndicador2);
                //tblIndicadores.AddCell(clIndicador3);
                //tblIndicadores.AddCell(clIndicador4);
                ////Add second table to the doc
                //doc.Add(tblIndicadores);
                #endregion

                #region CREACION DE TABLA PARA CONTRIBUIDORES
                //PdfPTable tblContrubuidores = new PdfPTable(2);
                //tblContrubuidores.WidthPercentage = 95;
                //iTextSharp.text.Font fontContribuidores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                ////HEADERS
                //PdfPCell clHeaderScrapContrib = new PdfPCell();
                //Paragraph paragraphScrapContrib = new Paragraph("Contribuidores de Scrap", fontContribuidores);
                //paragraphScrapContrib.Alignment = Element.ALIGN_CENTER;
                //clHeaderScrapContrib.AddElement(paragraphScrapContrib);
                //clHeaderScrapContrib.BorderWidth = 0;
                //PdfPCell clHeaderDowntimeContrib = new PdfPCell();
                //Paragraph paragraphDowntimeContrib = new Paragraph("Contribuidores de Tiempo Muerto", fontContribuidores);
                //paragraphDowntimeContrib.Alignment = Element.ALIGN_CENTER;
                //clHeaderDowntimeContrib.AddElement(paragraphDowntimeContrib);
                //clHeaderDowntimeContrib.BorderWidth = 0;
                ////Add Headers to Table
                //tblContrubuidores.AddCell(clHeaderScrapContrib);
                //tblContrubuidores.AddCell(clHeaderDowntimeContrib);
                ////CONTRIBUIDOR SCRAP
                //var chartImageContribScrap = new MemoryStream();
                //chartMuestreo1.SaveImage(chartImageContribScrap, ChartImageFormat.Png);
                //iTextSharp.text.Image Chart_imageContribScrap = iTextSharp.text.Image.GetInstance(chartImageContribScrap.GetBuffer());
                //Chart_imageContribScrap.ScalePercent(35f);
                //PdfPCell clScrapContrib = new PdfPCell(Chart_imageContribScrap);
                //clScrapContrib.BorderWidth = 0;
                ////CONTRIBUIDOR TIEMPO MUERTO
                //var chartImageContribDowntime = new MemoryStream();
                //chartMuestreo2.SaveImage(chartImageContribDowntime, ChartImageFormat.Png);
                //iTextSharp.text.Image Chart_imageContribDowntime = iTextSharp.text.Image.GetInstance(chartImageContribDowntime.GetBuffer());
                //Chart_imageContribDowntime.ScalePercent(35f);
                //PdfPCell clDowntimeContrib = new PdfPCell(Chart_imageContribDowntime);
                //clDowntimeContrib.BorderWidth = 0;
                ////Add Contribuidores to table
                //tblContrubuidores.AddCell(clScrapContrib);
                //tblContrubuidores.AddCell(clDowntimeContrib);
                //Add third table to doc
                //doc.Add(tblContrubuidores);
                #endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("El archivo no se pudo generar, intentelo más tarde..." + ex.ToString());
            }
        }

        private void insertarEncabezado(Document doc)
        {
            iTextSharp.text.Image headerImg = iTextSharp.text.Image.GetInstance("../../Resources/hanonLogoPng.png");
            headerImg.ScaleToFit(100f, 60f);
            headerImg.SetAbsolutePosition(650, 540);
            doc.Add(headerImg);
        }

        #region INSERCION DE ACUMULADO MENSUAL

        private void ConsultaPorPlanta(int diasMes)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        dgvEstatusMensual.Rows.Add("Fabricacion");
                        CargarArea("Fabricacion", i);
                        break;
                    case 1:
                        dgvEstatusMensual.Rows.Add("Dobladoras");
                        CargarArea("Dobladoras", i);
                        break;
                    case 2:
                        dgvEstatusMensual.Rows.Add("Lavadoras");
                        CargarArea("Lavadoras", i);
                        break;
                    case 3:
                        dgvEstatusMensual.Rows.Add("Ensamble Final");
                        CargarArea("Ensamble Final", i);
                        break;
                    case 4:
                        dgvEstatusMensual.Rows.Add("Soldadoras");
                        CargarArea("Soldadoras", i);
                        break;
                    case 5:
                        dgvEstatusMensual.Rows.Add("Robot SLs");
                        CargarArea("Robot SLs", i);
                        break;
                }
                dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, i, diasMes);
            }
            dgvEstatusMensual.Rows.Add("Planta");
            for (int j = 0; j <= diasMes; j++)
            {
                dgvEstatusMensual = CalcularPromedioPorPlanta(dgvEstatusMensual, j);
            }
        }

        public void CargarArea(string area, int i)
        {
            //MensualBLL objectMensualBLL = RecuperarInformacion();
            int mes = DateTime.Now.Month;
            int anual = DateTime.Now.Year;
            int diasMes = DateTime.DaysInMonth(anual, mes);
            float OEEDia = 0;
            int contadorDias = 1;
            DateTime Fecha = new DateTime(anual, mes, contadorDias);
            for (int j = 0; j < diasMes; j++)
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT fecha, turno, area, machine, target, piecesProduced, " +
                "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
                "OEE FROM NewOEEPerMachine WHERE fecha='" + Fecha.ToString("dd/MM/yyyy") + "' AND area='" + area + "'").Tables[0];
                if (dgvFiltro.Rows.Count > 0)
                {
                    ConsultarEstatusProduccionArea(dgvFiltro);
                    OEEDia = (float)Math.Round(CalcularOEEDia(dgvEstatusProduccionPorArea, 9), 2);
                    #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
                    if (Double.IsNaN(OEEDia))
                    {
                        OEEDia = 0;
                    }
                    #endregion

                }
                dgvEstatusMensual.Rows[i].Cells[j + 1].Value = OEEDia * 100;
                dgvEstatusMensual.Rows[i].Cells[j + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
                if (contadorDias < diasMes)
                {
                    contadorDias++;
                    Fecha = new DateTime(DateTime.Now.Year, mes, contadorDias);
                    dgvEstatusProduccionPorArea.DataSource = null;
                    dgvEstatusProduccionPorArea.Rows.Clear();
                    dgvFiltro.DataSource = null;
                    dgvFiltro.Rows.Clear();
                }
                OEEDia = 0;
            }

        }

        #endregion

        #region INSERCION FABRICACION
        //private void ConsultaPorArea(int diasMes, string area)
        //{
        //    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM AreaMaquinas WHERE Area='" + area + "' " +
        //        "AND Maquina!='Todas'").Tables[0];
        //    int contadorMaquina = cboMaquina.Items.Count;

        //    //HAY QUE REINICIAR EL dgvEstatusMensual

        //    for (int i = 0; i < contadorMaquina; i++)
        //    {
        //        cboMaquina.SelectedIndex = i;
        //        //MessageBox.Show(cboMaquina.Text);
        //        dgvEstatusMensual.Rows.Add(cboMaquina.Text);
        //        CargarMaquina(i);

        //    }
        //    dgvEstatusMensual.Rows.Add(cboArea.Text);

        //    CargarAreaMaquinas(cboArea.Text, contadorMaquina);
        //    dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, contadorMaquina - 1, diasMes);
        //}

        #endregion

        #region APARTADO DE OPERACIONES TECNICAS
        //public void CargarMaquina(int contador)
        //{
        //    MensualBLL objectMensualBLL = RecuperarInformacion();
        //    string area = objectMensualBLL.Area;
        //    string maquina = objectMensualBLL.Maquina;
        //    int mes = Convert.ToInt32(objectMensualBLL.Fecha);
        //    int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
        //    float OEE = 0, Disponibilidad = 0, Rendimiento = 0, Calidad = 0;
        //    int contadorDias = 1;
        //    DateTime Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
        //    for (int i = 0; i < diasMes; i++)
        //    {
        //        dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT startTime, endTime, partNumber, " +
        //        "target, piecesProduced, variation, downTime, scrapPieces, Disponibilidad, " +
        //        "Rendimiento, Calidad, OEE FROM OEEPerMachine WHERE fecha='" + Fecha.ToString("dd/MM/yyyy") + "' AND area='" + area + "' " +
        //        "AND machine='" + maquina + "'").Tables[0];

        //        Disponibilidad = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 8), 2) * 100;
        //        Rendimiento = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 9), 2) * 100;
        //        Calidad = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 10), 2) * 100;
        //        OEE = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 11), 2) * 100;
        //        #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
        //        if (Double.IsNaN(OEE))
        //        {
        //            OEE = 0;
        //        }
        //        if (Double.IsNaN(Disponibilidad))
        //        {
        //            Disponibilidad = 0;
        //        }
        //        if (Double.IsNaN(Rendimiento))
        //        {
        //            Rendimiento = 0;
        //        }
        //        if (Double.IsNaN(Calidad))
        //        {
        //            Calidad = 0;
        //        }

        //        #endregion
        //        dgvEstatusMensual.Rows[contador].Cells[i + 1].Value = OEE;
        //        dgvEstatusMensual.Rows[contador].Cells[i + 1].Style.ForeColor = Color.White;
        //        dgvEstatusMensual.Rows[contador].Cells[i + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
        //        //#region DATAGRIDVIEWS PARA CALCULO DE INDICADORES DE DISPONIBILIDAD, RENDIMIENTO Y CALIDAD
        //        //dgvEstatusMensualDisponibilidad.Rows[0].Cells[i + 1].Value = Disponibilidad;
        //        //dgvEstatusMensualRendimiento.Rows[0].Cells[i + 1].Value = Rendimiento;
        //        //dgvEstatusMensualCalidad.Rows[0].Cells[i + 1].Value = Calidad;
        //        //#endregion
        //        if (contadorDias < diasMes)
        //        {
        //            contadorDias++;
        //            Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
        //            dgvFiltro.DataSource = null;
        //            dgvFiltro.Rows.Clear();
        //        }
        //    }
        //    dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, contador, diasMes);

        //}

        public float CalcularIndicador(int campoIndicador)
        {
            float indicador = 0;
            float valorCampo = 0, contador = 0;
            for (int i = 0; i < dgvEstatusProduccionPorArea.Rows.Count; i++)
            {
                float.TryParse(dgvEstatusProduccionPorArea.Rows[i].Cells[campoIndicador].Value.ToString(), out valorCampo);
                contador += valorCampo;
            }
            indicador = contador / dgvEstatusProduccionPorArea.Rows.Count;
            return indicador;
        }

        public void ConsultarEstatusProduccionArea(DataGridView objectDataGridView)
        {
            string maquina = "N/A";
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
                promedioDisponibilidad = (Disponibilidad / factorDisponibilidad);
                promedioRendimiento = (Rendimiento / factorRendimiento);
                promedioCalidad = (Calidad / factorCalidad);
                promedioOEE = (OEE / factorOEE);
                variation = piecesProduced - target;
                //MessageBox.Show(maquina.ToString());
                dgvEstatusProduccionPorArea.Rows.Add(maquina, target, piecesProduced, variation, downTime, scrapPieces, promedioDisponibilidad, promedioRendimiento, promedioCalidad, promedioOEE);
                factorDisponibilidad = 0; factorRendimiento = 0; factorCalidad = 0; factorOEE = 0;
                promedioDisponibilidad = 0; promedioRendimiento = 0; promedioCalidad = 0; promedioOEE = 0;
            }
        }

        public float CalcularOEEDia(DataGridView objectDataGridView, int campoIndicador)
        {
            float indicador = 0;
            float valorCampo = 0, contador = 0;
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                float.TryParse(objectDataGridView.Rows[i].Cells[campoIndicador].Value.ToString(), out valorCampo);
                contador += valorCampo;
            }
            indicador = contador / objectDataGridView.Rows.Count;
            return indicador;
        }

        public DataGridView CalcularPromedioPorArea(DataGridView objectDataGridView, int i, int dias)
        {
            float sumaOEE = 0;
            float contadorOEE = 0;
            float promOEE = 0;
            int restador = 0;
            for (int j = 0; j < dias; j++)
            {
                float.TryParse(objectDataGridView.Rows[i].Cells[j + 1].Value.ToString(), out contadorOEE);
                if (contadorOEE != 0)
                {
                    sumaOEE += contadorOEE;
                }
                else
                {
                    restador += 1;
                }
            }
            if (restador == dias)
                restador = dias - 1;
            promOEE = (float)Math.Round((sumaOEE / (dias - restador)), 2);
            objectDataGridView.Rows[i].Cells[dias + 1].Value = promOEE;
            //objectDataGridView.Rows[i].Cells[dias + 1].Style.ForeColor = Color.White;
            objectDataGridView.Rows[i].Cells[dias + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
            return objectDataGridView;
        }

        public DataGridView CalcularPromedioPorPlanta(DataGridView objectDataGridView, int j)
        {
            float sumaPlanta = 0;
            float contadorPlanta = 0;
            float promPlanta = 0;
            int restador = 0;
            for (int i = 0; i < 6; i++)
            {
                float.TryParse(objectDataGridView.Rows[i].Cells[j + 1].Value.ToString(), out contadorPlanta);
                if (contadorPlanta != 0)
                {
                    sumaPlanta += contadorPlanta;
                }
                else
                {
                    restador += 1;
                }
            }
            if (restador == 6)
                restador = 1;
            promPlanta = (float)Math.Round((sumaPlanta / (6 - restador)), 2);
            objectDataGridView.Rows[6].Cells[j + 1].Value = promPlanta;
            //objectDataGridView.Rows[6].Cells[j + 1].Style.ForeColor = Color.White;
            objectDataGridView.Rows[6].Cells[j + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);

            return objectDataGridView;
        }

        private DataGridView EstablecerDiasDelMes(DataGridView objectDataGridView, int dias)
        {
            DataGridViewTextBoxColumn columnaInicial = new DataGridViewTextBoxColumn();
            columnaInicial.HeaderText = "Area";
            columnaInicial.Name = "Area";
            objectDataGridView.Columns.Add(columnaInicial);
            for (int i = 0; i < dias; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = "D" + (i + 1).ToString();
                column.Name = "D" + (i + 1).ToString();
                objectDataGridView.Columns.Add(column);
            }
            DataGridViewTextBoxColumn columnaFinal = new DataGridViewTextBoxColumn();
            columnaFinal.HeaderText = "Prom";
            columnaFinal.Name = "Prom";
            objectDataGridView.Columns.Add(columnaFinal);
            objectDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 6.25f, FontStyle.Bold);

            return objectDataGridView;
        }

        public Chart CargarChartEstatusMensual(DataGridView objectDataGridView, Chart objectChart)
        {
            //ValorDia nos permite verificar si el estatus mensual en tal dia es 0 para descartarlo
            float valorDia = 0;
            for (int i = 0; i < objectDataGridView.Columns.Count - 2; i++)
            {
                float.TryParse(objectDataGridView.Rows[objectDataGridView.Rows.Count - 1].Cells[i + 1].Value.ToString(), out valorDia);

                if (valorDia == 0)
                    continue;
                objectChart.Series[0].Points.AddXY("D" + (i + 1).ToString(), Math.Round(valorDia, 2));
            }
            return objectChart;
        }

        public Chart CargarChartAcumuladoMensual(DataGridView objectDataGridView, Chart objectChart)
        {
            float valorArea = 0;
            string nombreArea = "";
            for (int i = 0; i < objectDataGridView.Rows.Count - 1; i++)
            {
                float.TryParse(objectDataGridView.Rows[i].Cells[objectDataGridView.Columns.Count - 1].Value.ToString(), out valorArea);
                nombreArea = objectDataGridView.Rows[i].Cells[0].Value.ToString();
                objectChart.Series[0].Points.AddXY(nombreArea, Math.Round(valorArea, 2));
            }
            float.TryParse(objectDataGridView.Rows[objectDataGridView.Rows.Count - 1].Cells[objectDataGridView.Columns.Count - 1].Value.ToString(), out OEEAcumulado);
            return objectChart;
        }

        public static Microsoft.Office.Interop.Outlook.Account GetAccountForEmailAddress(Outlook.Application application, string smtpAddress)
        {
            // Loop over the Accounts collection of the current Outlook session.
            Outlook.Accounts accounts = application.Session.Accounts;
            MessageBox.Show(application.Session.Accounts.Count.ToString());
            foreach (Outlook.Account account in accounts)
            {
                MessageBox.Show(account.SmtpAddress);
                // When the email address matches, return the account.
                if (account.SmtpAddress == smtpAddress)
                {
                    return account;
                }
            }
            throw new System.Exception(string.Format("No Account with SmtpAddress: {0} exists!", smtpAddress));
        }

        #endregion

        private iTextSharp.text.Image SetChart(Chart objectChart)
        {
            var chartImageMemory = new MemoryStream();
            objectChart.SaveImage(chartImageMemory, ChartImageFormat.Png);
            iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartImageMemory.GetBuffer());
            chartImage.ScalePercent(42f);
            return chartImage;
        }

        #region GENERACION DE PRESENTACION POWERPOINT
        public void CrearPresentacionOEEPortadaDesdePlantilla(string plantillaPath, float oeeAcumulado, System.Windows.Forms.DataVisualization.Charting.Chart chartOeeDiario, System.Windows.Forms.DataVisualization.Charting.Chart chartOeeAcumulado)
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(userProfile, "Downloads");
            string nombreArchivo = "ReporteOEE_" + DateTime.Now.ToString("ddMMyyyy") + ".pptx";
            string salidaPath = Path.Combine(downloadsPath, nombreArchivo);

            File.Copy(plantillaPath, salidaPath, true);

            using (PresentationDocument presentationDocument = PresentationDocument.Open(salidaPath, true))
            {
                PresentationPart presentationPart = presentationDocument.PresentationPart;

                SlidePart plantillaSlidePart = presentationPart.SlideParts.First();
                SlidePart nuevoSlidePart = presentationPart.AddNewPart<SlidePart>();

                // Copiar contenido XML del slide
                nuevoSlidePart.Slide = (Slide)plantillaSlidePart.Slide.CloneNode(true);

                // Copiar el layout del slide
                SlideLayoutPart layoutPart = plantillaSlidePart.SlideLayoutPart;
                SlideLayoutPart newLayoutPart = nuevoSlidePart.AddPart(layoutPart);
                nuevoSlidePart.AddPart(newLayoutPart);


                SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;
                uint maxSlideId = slideIdList.ChildElements.Cast<SlideId>().Max(s => s.Id.Value);
                string rId = presentationPart.GetIdOfPart(nuevoSlidePart);

                SlideId newSlideId = new SlideId() { Id = maxSlideId + 1, RelationshipId = rId };
                slideIdList.Append(newSlideId);

                AddTextToSlide(nuevoSlidePart, "Planta - " + DateTime.Now.ToString("MMMM"), 2000000, 200000, 6000000, 700000);
                AddTextToSlide(nuevoSlidePart, $"OEE Acumulado General: {oeeAcumulado:F2}%", 2000000, 800000, 6000000, 600000);

                AddChartImageToSlide(nuevoSlidePart, chartOeeDiario, 500000, 1500000, 4000000, 3000000);
                AddChartImageToSlide(nuevoSlidePart, chartOeeAcumulado, 5000000, 1500000, 4000000, 3000000);

                presentationPart.Presentation.Save();
            }
        }

        private void AddTextToSlide(SlidePart slidePart, string text, int x, int y, int cx, int cy)
        {
            ShapeTree shapeTree = slidePart.Slide.CommonSlideData.ShapeTree;
            uint shapeId = (uint)(shapeTree.ChildElements.Count + 1);

            Shape shape = new Shape(
                new NonVisualShapeProperties(
                    new NonVisualDrawingProperties() { Id = shapeId, Name = "Texto" },
                    new NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties()
                ),
                new ShapeProperties(
                    new A.Transform2D(
                        new A.Offset() { X = x, Y = y },
                        new A.Extents() { Cx = cx, Cy = cy }
                    )
                ),
                new TextBody(
                    new A.BodyProperties(),
                    new A.ListStyle(),
                    new A.Paragraph(new A.Run(new A.Text(text)))
                )
            );
            shapeTree.Append(shape);
        }

        private void AddChartImageToSlide(SlidePart slidePart, System.Windows.Forms.DataVisualization.Charting.Chart chart, int x, int y, int cx, int cy)
        {
            using (MemoryStream chartImageStream = new MemoryStream())
            {
                chart.SaveImage(chartImageStream, ChartImageFormat.Png);
                chartImageStream.Position = 0;

                ImagePart imagePart = slidePart.AddImagePart(ImagePartType.Png);
                imagePart.FeedData(chartImageStream);

                string imagePartId = slidePart.GetIdOfPart(imagePart);

                P.Picture picture = new P.Picture(
                    new P.NonVisualPictureProperties(
                        new P.NonVisualDrawingProperties() { Id = 4U, Name = "ChartImage" },
                        new P.NonVisualPictureDrawingProperties(new A.PictureLocks() { NoChangeAspect = true }),
                        new ApplicationNonVisualDrawingProperties()
                    ),
                    new P.BlipFill(
                        new A.Blip() { Embed = imagePartId },
                        new A.Stretch(new A.FillRectangle())
                    ),
                    new P.ShapeProperties(
                        new A.Transform2D(
                            new A.Offset() { X = x, Y = y },
                            new A.Extents() { Cx = cx, Cy = cy }
                        ),
                        new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle }
                    )
                );

                slidePart.Slide.CommonSlideData.ShapeTree.AppendChild(picture);
            }
        }


        #endregion
    }
}
