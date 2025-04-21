using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;
using System.Security.Principal;
using System.ComponentModel;
using System.Drawing;
using OEE1.BLL;

namespace OEE1.DAL
{
    internal class CorreoAutomatico
    {
        // Elementos para la insercion del acumulado mensual
        DataGridView dgvEstatusMensual = new DataGridView(), dgvEstatusMensualDisponibilidad = new DataGridView(), 
            dgvEstatusMensualRendimiento = new DataGridView(), dgvEstatusMensualCalidad = new DataGridView();
        DataGridView dgvEstatusProduccionPorArea = new DataGridView(), dgvScrap = new DataGridView(), dgvDowntime = new DataGridView();
        consultaDAL objectConsultaDAL;
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

        public void crearPdf(DataGridView dgvFiltro, DataGridView objectDgvEstatus, DataGridView objectDgvScrap, DataGridView objectDgvDowntime)
        {
            dgvEstatusProduccionPorArea = objectDgvEstatus;
            dgvScrap = objectDgvScrap;
            dgvDowntime = objectDgvDowntime;
            objectConsultaDAL = new consultaDAL();
            DateTime fecha = DateTime.Now;
            float indicadorDisponibilidad = 0, indicadorRendimiento = 0, indicadorCalidad = 0, indicadorOEE = 0;
            string[] areas = { "Fabricacion", "Dobladoras", "Lavadoras", "Ensamble Final", "Soldadoras", "Robot SLs" };
            string[] turnos = { "1er Turno", "2do Turno" };
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
            Paragraph paragrahp = new Paragraph("REPORTE DIARIO DE OEE - " + fecha.AddDays(-1).ToString("dd/MM/yyyy"));
            //Add paragraph to doc
            doc.Add(paragrahp);
            doc.Add(new Paragraph("\n"));
            //  PRIMER TURNO  //
            //Loop para cargar las areas en el pdf de los 3 turnos
            foreach (string turno in turnos)
            {
                doc.Add(new Paragraph(turno));
                doc.Add(new Paragraph("\n"));
                foreach (string area in areas)
                {
                    Paragraph areaTitle = new Paragraph(area);
                    doc.Add(areaTitle);
                    doc.Add(new Paragraph("\n"));
                    //Estatus de maquinas e indicadores generales por area
                    dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT fecha, turno, area, machine, target, piecesProduced, " +
                        "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
                        "OEE FROM OEEPerMachine WHERE fecha='" + fecha.AddDays(-3).ToString("dd/MM/yyyy") + "' AND turno='" + turno + "' " +
                        "AND area='" + area + "'").Tables[0];
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
                    consultarEstatusProduccionArea(dgvFiltro);
                    indicadorDisponibilidad = (float)Math.Round(CalcularIndicador(6), 2);
                    indicadorRendimiento = (float)Math.Round(CalcularIndicador(7), 2);
                    indicadorCalidad = (float)Math.Round(CalcularIndicador(8), 2);
                    indicadorOEE = (float)Math.Round(CalcularIndicador(9), 2);
                    //Estatus de Scrap
                    dgvScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                        "nombreScrap, numPiezas FROM RegistroScrap WHERE fecha='" + fecha.AddDays(-1).ToString("dd/MM/yyyy") + "' AND turno='" + turno + "' " +
                        "AND area='" + area + "'").Tables[0]; // INTEGRA LA INFORMACION DE SCRAP Y TIEMPO MUERTO Y YA
                    dgvScrap = objectConsultaDAL.SumarElementosRepetidos(dgvScrap);
                    DataGridViewColumn numPiezas = dgvScrap.Columns[1];
                    dgvScrap.Sort(numPiezas, ListSortDirection.Descending);
                    //Estatus de tiempo muerto
                    dgvDowntime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT " +
                        "razonTiempoMuerto, minutosTiempoMuerto FROM RegistroTiempoMuerto WHERE fecha='" + fecha.AddDays(-1).ToString("dd/MM/yyyy") + "' AND turno='" + turno + "' " +
                        "AND area='" + area + "'").Tables[0]; // INTEGRA LA INFORMACION DE SCRAP Y TIEMPO MUERTO Y YA
                    dgvDowntime = objectConsultaDAL.SumarElementosRepetidos(dgvDowntime);
                    DataGridViewColumn minutos = dgvDowntime.Columns[1];
                    dgvDowntime.Sort(minutos, ListSortDirection.Descending);
                    generarPdf(indicadorDisponibilidad, indicadorRendimiento, indicadorCalidad, indicadorOEE, doc, wri);
                    dgvEstatusProduccionPorArea.Rows.Clear();
                }
            }
            doc.Close();
            stream.Close();
        }

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
                promedioOEE = (float)Math.Round((OEE / factorOEE) * 100, 2);
                variation = piecesProduced - target;
                //MessageBox.Show(maquina.ToString());
                dgvEstatusProduccionPorArea.Rows.Add(maquina, target, piecesProduced, variation, downTime, scrapPieces, promedioDisponibilidad, promedioRendimiento, promedioCalidad, promedioOEE);
                factorDisponibilidad = 0; factorRendimiento = 0; factorCalidad = 0; factorOEE = 0;
                promedioDisponibilidad = 0; promedioRendimiento = 0; promedioCalidad = 0; promedioOEE = 0;


            }
        }

        private void generarPdf(float indicadorDisponibilidad, float indicadorRendimiento, float indicadorCalidad, float indicadorOEE, Document doc, PdfWriter wri)
        {
            try
            {
                #region TABLA PRINCIPAL DE CONSULTA DIARIA DE AREA
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
                //Scanning DataGridView Estatus de Producción por Area
                for (int i = 0; i < dgvEstatusProduccionPorArea.Rows.Count; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        string cadenaCelda = dgvEstatusProduccionPorArea.Rows[i].Cells[j].Value.ToString();
                        PdfPCell clAdder = new PdfPCell(new Phrase(cadenaCelda, _standardFont));
                        if(j > 5)
                        {
                            clAdder.HorizontalAlignment = 1;
                            float evaluador; float.TryParse(dgvEstatusProduccionPorArea.Rows[i].Cells[j].Value.ToString(), out evaluador);
                            if (evaluador < 75)
                                clAdder.BackgroundColor = BaseColor.RED;
                            else if (evaluador >= 75 && evaluador < 80)
                                clAdder.BackgroundColor = BaseColor.YELLOW;
                            else
                                clAdder.BackgroundColor = BaseColor.GREEN;
                        }
                        tblReport.AddCell(clAdder);
                    }
                }
                //Add table to the doc
                doc.Add(tblReport);
                #endregion
                doc.Add(new Paragraph("\n"));

                #region TABLA CONTENEDORA DE SUBTABLAS -> Scrap y Tiempo muerto
                PdfPTable tblContenedor = new PdfPTable(2);
                tblContenedor.DefaultCell.BorderWidth = 0;
                    #region SUBTABLA SCRAP
                    //CREACION DE TABLA
                    PdfPTable tblScrap = new PdfPTable(2);
                    tblScrap.WidthPercentage = 47.5f;
                    tblScrap.DefaultCell.BorderWidth = 0;
                    //CELDAS
                    PdfPCell clRazonScrap = new PdfPCell(new Phrase("Razon de Scrap", _standardFont));
                    clRazonScrap.BorderWidth = 0;
                    clRazonScrap.BorderWidthBottom = 0.75f;
                    PdfPCell clPiezasScrap = new PdfPCell(new Phrase("Numero de Piezas", _standardFont));
                    clPiezasScrap.BorderWidth = 0;
                    clPiezasScrap.BorderWidthBottom = 0.75f;
                    //Add Cells to Scrap Table
                    tblScrap.AddCell(clRazonScrap);
                    tblScrap.AddCell(clPiezasScrap);
                    //Scanning DataGridView Scrap
                    for(int i = 0; i < dgvScrap.Rows.Count; i++)
                    {
                        string razonScrap = dgvScrap.Rows[i].Cells[0].Value.ToString();
                        string piezasScrap = dgvScrap.Rows[i].Cells[1].Value.ToString();
                        PdfPCell clRazonScrapAdder = new PdfPCell(new Phrase(razonScrap, _standardFont));
                        clRazonScrapAdder.BorderWidth = 0;
                        PdfPCell clPiezasScrapAdder = new PdfPCell(new Phrase(piezasScrap, _standardFont));
                        clPiezasScrapAdder.BorderWidth = 0;
                        tblScrap.AddCell(clRazonScrapAdder);
                        tblScrap.AddCell(clPiezasScrapAdder);
                    }
                    #endregion
                tblContenedor.AddCell(tblScrap);
                    #region SUBTABLA TIEMPO MUERTO
                    //CREACION DE TABLA
                    PdfPTable tblDowntime = new PdfPTable(2);
                    tblDowntime.WidthPercentage = 47.5f;
                    //CELDAS
                    PdfPCell clRazonDowntime = new PdfPCell(new Phrase("Razon de Tiempo Muerto", _standardFont));
                    clRazonDowntime.BorderWidth = 0;
                    clRazonDowntime.BorderWidthBottom = 0.75f;
                    PdfPCell clMinutos = new PdfPCell(new Phrase("Minutos", _standardFont));
                    clMinutos.BorderWidth = 0;
                    clMinutos.BorderWidthBottom = 0.75f;
                    //Add Cells to Scrap Table
                    tblDowntime.AddCell(clRazonDowntime);
                    tblDowntime.AddCell(clMinutos);
                    //Scanning DataGridView Scrap
                    for (int i = 0; i < dgvDowntime.Rows.Count; i++)
                    {
                        string razonDowntime = dgvDowntime.Rows[i].Cells[0].Value.ToString();
                        string minutos = dgvDowntime.Rows[i].Cells[1].Value.ToString();
                        PdfPCell clRazonDowntimeAdder = new PdfPCell(new Phrase(razonDowntime, _standardFont));
                        clRazonDowntimeAdder.BorderWidth = 0;
                        PdfPCell clMinutosAdder = new PdfPCell(new Phrase(minutos, _standardFont));
                        clMinutosAdder.BorderWidth = 0;
                        tblDowntime.AddCell(clRazonDowntimeAdder);
                        tblDowntime.AddCell(clMinutosAdder);
                    }
                    #endregion
                tblContenedor.AddCell(tblDowntime);

                doc.Add(tblContenedor);
                #endregion

                doc.Add(new Paragraph("\n")); //Salto de línea

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

                #region ESPECIFICACIONES

                #endregion
                //Close the document to finalize the actions
                //doc.Close();
                //Close stream (memory)
                //stream.Close();
                //MessageBox.Show("Saved successfully");
                
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
        private void insertarAcumuladoMensual(Document doc)
        {
            string[] areas = { "Fabricacion", "Dobladoras", "Lavadoras", "Ensamble Final", "Soldadoras", "Robot SLs" };
        }

        //private void ConsultaPorPlanta(int diasMes)
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        switch (i)
        //        {
        //            case 0:
        //                dgvEstatusMensual.Rows.Add("Fabricacion");
        //                dgvEstatusMensualDisponibilidad.Rows.Add("Fabricacion");
        //                dgvEstatusMensualRendimiento.Rows.Add("Fabricacion");
        //                dgvEstatusMensualCalidad.Rows.Add("Fabricacion");
        //                CargarArea("Fabricacion", i);
        //                break;
        //            case 1:
        //                dgvEstatusMensual.Rows.Add("Dobladoras");
        //                dgvEstatusMensualDisponibilidad.Rows.Add("Dobladoras");
        //                dgvEstatusMensualRendimiento.Rows.Add("Dobladoras");
        //                dgvEstatusMensualCalidad.Rows.Add("Dobladoras");
        //                CargarArea("Dobladoras", i);
        //                break;
        //            case 2:
        //                dgvEstatusMensual.Rows.Add("Lavadoras");
        //                dgvEstatusMensualDisponibilidad.Rows.Add("Lavadoras");
        //                dgvEstatusMensualRendimiento.Rows.Add("Lavadoras");
        //                dgvEstatusMensualCalidad.Rows.Add("Lavadoras");
        //                CargarArea("Lavadoras", i);
        //                break;
        //            case 3:
        //                dgvEstatusMensual.Rows.Add("Ensamble Final");
        //                dgvEstatusMensualDisponibilidad.Rows.Add("Ensamble Final");
        //                dgvEstatusMensualRendimiento.Rows.Add("Ensamble Final");
        //                dgvEstatusMensualCalidad.Rows.Add("Ensamble Final");
        //                CargarArea("Ensamble Final", i);
        //                break;
        //            case 4:
        //                dgvEstatusMensual.Rows.Add("Soldadoras");
        //                dgvEstatusMensualDisponibilidad.Rows.Add("Soldadoras");
        //                dgvEstatusMensualRendimiento.Rows.Add("Soldadoras");
        //                dgvEstatusMensualCalidad.Rows.Add("Soldadoras");
        //                CargarArea("Soldadoras", i);
        //                break;
        //            case 5:
        //                dgvEstatusMensual.Rows.Add("Robot SLs");
        //                dgvEstatusMensualDisponibilidad.Rows.Add("Robot SLs");
        //                dgvEstatusMensualRendimiento.Rows.Add("Robot SLs");
        //                dgvEstatusMensualCalidad.Rows.Add("Robot SLs");
        //                CargarArea("Robot SLs", i);
        //                break;
        //        }
        //        dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, i, diasMes);
        //        dgvEstatusMensualDisponibilidad = CalcularPromedioPorArea(dgvEstatusMensualDisponibilidad, i, diasMes);
        //        dgvEstatusMensualRendimiento = CalcularPromedioPorArea(dgvEstatusMensualRendimiento, i, diasMes);
        //        dgvEstatusMensualCalidad = CalcularPromedioPorArea(dgvEstatusMensualCalidad, i, diasMes);
        //    }
        //    dgvEstatusMensual.Rows.Add("Planta");
        //    dgvEstatusMensualDisponibilidad.Rows.Add("Planta");
        //    dgvEstatusMensualRendimiento.Rows.Add("Planta");
        //    dgvEstatusMensualCalidad.Rows.Add("Planta");
        //    for (int j = 0; j <= diasMes; j++)
        //    {
        //        dgvEstatusMensual = CalcularPromedioPorPlanta(dgvEstatusMensual, j);
        //        dgvEstatusMensualDisponibilidad = CalcularPromedioPorPlanta(dgvEstatusMensualDisponibilidad, j);
        //        dgvEstatusMensualRendimiento = CalcularPromedioPorPlanta(dgvEstatusMensualRendimiento, j);
        //        dgvEstatusMensualCalidad = CalcularPromedioPorPlanta(dgvEstatusMensualCalidad, j);
        //    }
        //}

        //public void CargarArea(string area, int i)
        //{
        //    MensualBLL objectMensualBLL = RecuperarInformacion();
        //    int mes = Convert.ToInt32(objectMensualBLL.Fecha);
        //    int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
        //    float OEEDia = 0, DisponibilidadDia = 0, RendimientoDia = 0, CalidadDia = 0;
        //    int contadorDias = 1;
        //    DateTime Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
        //    for (int j = 0; j < diasMes; j++)
        //    {
        //        dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT fecha, turno, area, machine, target, piecesProduced, " +
        //        "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
        //        "OEE FROM OEEPerMachine WHERE fecha='" + Fecha.ToString("dd/MM/yyyy") + "' AND area='" + area + "'").Tables[0];
        //        if (dgvFiltro.Rows.Count > 0)
        //        {
        //            ConsultarEstatusProduccionArea(dgvFiltro);
        //            OEEDia = (float)Math.Round(CalcularOEEDia(dgvEstatusProduccionPorArea, 9), 2);
        //            DisponibilidadDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 6);
        //            RendimientoDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 7);
        //            CalidadDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 8);
        //            #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
        //            if (Double.IsNaN(OEEDia))
        //            {
        //                OEEDia = 0;
        //            }
        //            if (Double.IsNaN(DisponibilidadDia))
        //            {
        //                DisponibilidadDia = 0;
        //            }
        //            if (Double.IsNaN(RendimientoDia))
        //            {
        //                RendimientoDia = 0;
        //            }
        //            if (Double.IsNaN(CalidadDia))
        //            {
        //                CalidadDia = 0;
        //            }
        //            #endregion

        //        }
        //        dgvEstatusMensual.Rows[i].Cells[j + 1].Value = OEEDia * 100;
        //        dgvEstatusMensual.Rows[i].Cells[j + 1].Style.ForeColor = Color.White;
        //        dgvEstatusMensual.Rows[i].Cells[j + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
        //        #region DATAGRIDVIEWS PARA CALCULO DE INDICADORES DE DISPONIBILIDAD, RENDIMIENTO Y CALIDAD
        //        dgvEstatusMensualDisponibilidad.Rows[i].Cells[j + 1].Value = DisponibilidadDia * 100;
        //        dgvEstatusMensualRendimiento.Rows[i].Cells[j + 1].Value = RendimientoDia * 100;
        //        dgvEstatusMensualCalidad.Rows[i].Cells[j + 1].Value = CalidadDia * 100;
        //        #endregion
        //        if (contadorDias < diasMes)
        //        {
        //            contadorDias++;
        //            Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
        //            dgvEstatusProduccionPorArea.DataSource = null;
        //            dgvEstatusProduccionPorArea.Rows.Clear();
        //            dgvFiltro.DataSource = null;
        //            dgvFiltro.Rows.Clear();
        //        }
        //        OEEDia = 0;
        //        DisponibilidadDia = 0;
        //        RendimientoDia = 0;
        //        CalidadDia = 0;
        //    }

        //}

        #endregion

        #region APARTADO DE OPERACIONES TECNICAS

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

        public static Outlook.Account GetAccountForEmailAddress(Outlook.Application application, string smtpAddress)
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
    }
}
