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
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Office.Interop.Excel;
using OEE1.BLL;
using OEE1.DAL;

namespace OEE1.PL
{
    public partial class ReporteMensual : Form
    {
        consultaDAL objectConsultaDAL = new consultaDAL();
        public ReporteMensual()
        {
            InitializeComponent();
            cargarComboBoxes();
            dtpFecha.CustomFormat = "MMMM yyyy";
            dtpFecha.Format = DateTimePickerFormat.Custom;
        }
        /* -----CARGA DE COMBO BOXES----- */

        private void cargarComboBoxes()
        {
            /* Carga de datos cboArea -> Combo Box para especificar el area */
            cboArea.DataSource = objectConsultaDAL.MostrarDatos("nombreAreas").Tables[0];
            cboArea.DisplayMember = "area";
            cboArea.ValueMember = "area";

            /* Carga de datos cboMaquina -> Combo Box para maquinas */
            cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM AreaMaquinas WHERE Area='Fabricacion'").Tables[0];
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboArea.SelectedIndex)
            {
                case 0:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Fabricacion'").Tables[0];
                    break;
                case 1:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM " +
                        "AreaMaquinas WHERE Area='Dobladoras'").Tables[0];
                    break;
                case 2:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Lavadoras'").Tables[0];
                    break;
                case 3:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='EnsambleFinal'").Tables[0];
                    break;
                case 4:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Soldadoras'").Tables[0];
                    break;
                case 5:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Robot SLs'").Tables[0];
                    //if (cboMaquina.Text == "Todas")
                    //{
                    //    var FirstItem = cboMaquina.Items[0];
                    //    cboMaquina.Items[0] = cboMaquina.Items[cboMaquina.Items.Count - 1];
                    //    cboMaquina.Items[cboMaquina.Items.Count - 1] = FirstItem;
                    //}
                    break;
                case 6:
                    cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                        " FROM AreaMaquinas WHERE Area='Planta'").Tables[0];
                    break;
                default:
                    MessageBox.Show("No se ha hecho ninguna seleccion");
                    break;
            }
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";
        }

        private MensualBLL RecuperarInformacion()
        {
            MensualBLL objectMensualBLL = new MensualBLL();
            objectMensualBLL.Fecha = dtpFecha.Value.Month.ToString();
            objectMensualBLL.Area = cboArea.Text;
            objectMensualBLL.Maquina = cboMaquina.Text;
            return objectMensualBLL;
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

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            int mes = Convert.ToInt32(objectMensualBLL.Fecha);
            string area = objectMensualBLL.Area;
            string maquina = objectMensualBLL.Maquina;
            int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
            float DisponibilidadTotal, RendimientoTotal, CalidadTotal, OEETotal = 0;

            //Indicamos en dgvEstatusMensual cuantos dias tiene el mes
            dgvEstatusMensual = EstablecerDiasDelMes(dgvEstatusMensual, diasMes);
            dgvEstatusMensualDisponibilidad = EstablecerDiasDelMes(dgvEstatusMensualDisponibilidad, diasMes);
            dgvEstatusMensualRendimiento = EstablecerDiasDelMes(dgvEstatusMensualRendimiento, diasMes);
            dgvEstatusMensualCalidad = EstablecerDiasDelMes(dgvEstatusMensualCalidad, diasMes);
            //Realizamos consulta
            if (area == "Planta")
            {
                ConsultaPorPlanta(diasMes);
                float.TryParse(dgvEstatusMensual.Rows[6].Cells[diasMes + 1].Value.ToString(), out OEETotal);
                dgvEstatusMensual.Rows[6].Cells[diasMes + 1].Style.ForeColor = Color.Black;
                float.TryParse(dgvEstatusMensualDisponibilidad.Rows[6].Cells[diasMes + 1].Value.ToString(), out DisponibilidadTotal);
                float.TryParse(dgvEstatusMensualRendimiento.Rows[6].Cells[diasMes + 1].Value.ToString(), out RendimientoTotal);
                float.TryParse(dgvEstatusMensualCalidad.Rows[6].Cells[diasMes + 1].Value.ToString(), out CalidadTotal);
                CargarGraficasIndicadores(DisponibilidadTotal, RendimientoTotal, CalidadTotal, OEETotal);
                /*------GRAFICAS DE CONTRIBUIDORES PLANTA-------*/
                CargarContribuidoresPlanta();
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE mes=" + dtpFecha.Value.Month + "");
            }
            else if (area != "Planta" && maquina == "Todas")
            {
                int contadorMaquina = cboMaquina.Items.Count;
                ConsultaPorArea(diasMes, contadorMaquina);
                float.TryParse(dgvEstatusMensual.Rows[contadorMaquina - 1].Cells[diasMes + 1].Value.ToString(), out OEETotal);
                dgvEstatusMensual.Rows[contadorMaquina - 1].Cells[diasMes + 1].Style.ForeColor = Color.Black;
                float.TryParse(dgvEstatusMensualDisponibilidad.Rows[0].Cells[diasMes + 1].Value.ToString(), out DisponibilidadTotal);
                float.TryParse(dgvEstatusMensualRendimiento.Rows[0].Cells[diasMes + 1].Value.ToString(), out RendimientoTotal);
                float.TryParse(dgvEstatusMensualCalidad.Rows[0].Cells[diasMes + 1].Value.ToString(), out CalidadTotal);
                CargarGraficasIndicadores(DisponibilidadTotal, RendimientoTotal, CalidadTotal, OEETotal);
                /*------GRAFICAS DE CONTRIBUIDORES AREA-------*/
                CargarContribuidoresArea();
                cboMaquina.SelectedIndex = cboMaquina.Items.Count - 1;
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE mes=" + dtpFecha.Value.Month + " AND area='" + cboArea.Text + "'");
            }
            else if(area != "Planta" && maquina != "Todas")
            {
                ConsultaPorMaquina(diasMes);
                float.TryParse(dgvEstatusMensual.Rows[0].Cells[diasMes + 1].Value.ToString(), out OEETotal);
                dgvEstatusMensual.Rows[0].Cells[diasMes + 1].Style.ForeColor = Color.Black;
                float.TryParse(dgvEstatusMensualDisponibilidad.Rows[0].Cells[diasMes + 1].Value.ToString(), out DisponibilidadTotal);
                float.TryParse(dgvEstatusMensualRendimiento.Rows[0].Cells[diasMes + 1].Value.ToString(), out RendimientoTotal);
                float.TryParse(dgvEstatusMensualCalidad.Rows[0].Cells[diasMes + 1].Value.ToString(), out CalidadTotal);
                CargarGraficasIndicadores(DisponibilidadTotal, RendimientoTotal, CalidadTotal, OEETotal);
                /*------GRAFICAS DE CONTRIBUIDORES MAQUINA-------*/
                CargarContribuidoresMaquina();
                CalcularPiezasMeta("SELECT target, piecesProduced FROM NewOEEPerMachine WHERE mes=" + dtpFecha.Value.Month + " " +
                    "AND area='" + cboArea.Text + "' AND machine='" + cboMaquina.Text + "'");
            }
            else
            {
                MessageBox.Show("Registros no disponibles");
            }
            //Establecemos colores de referencia
            if(dgvEstatusMensual.Rows.Count > 0)
                dgvEstatusMensual = EstablecerColorCeldas(dgvEstatusMensual, diasMes);

            //Configuracion de botones True
            btnCapturaPantalla.Enabled = true;
            btnDescargaExcel.Enabled = true;
            btnGeneraPdf.Enabled = true;
            btnPresentacion.Enabled = false;
            //Configuracion de botones False
            btnCalcular.Enabled = false;
            dtpFecha.Enabled = false;
            cboArea.Enabled = false;
            cboMaquina.Enabled = false;
            //Personalizamos DataGrid
            dgvEstatusMensual.DefaultCellStyle.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 6.25f, FontStyle.Regular);
            dgvEstatusMensual = UpdateSizeDgv(dgvEstatusMensual);
            dgvEstatusMensual.BackgroundColor = SystemColors.Control;
            ////Enviar correo automatico
            //CorreoAutomatico correo = new CorreoAutomatico();
            //correo.enviarCorreo();
        }

        private void ConsultaPorPlanta(int diasMes)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        dgvEstatusMensual.Rows.Add("Fabricacion");
                        dgvEstatusMensualDisponibilidad.Rows.Add("Fabricacion");
                        dgvEstatusMensualRendimiento.Rows.Add("Fabricacion");
                        dgvEstatusMensualCalidad.Rows.Add("Fabricacion");
                        CargarArea("Fabricacion", i);
                        break;
                    case 1:
                        dgvEstatusMensual.Rows.Add("Dobladoras");
                        dgvEstatusMensualDisponibilidad.Rows.Add("Dobladoras");
                        dgvEstatusMensualRendimiento.Rows.Add("Dobladoras");
                        dgvEstatusMensualCalidad.Rows.Add("Dobladoras");
                        CargarArea("Dobladoras", i);
                        break;
                    case 2:
                        dgvEstatusMensual.Rows.Add("Lavadoras");
                        dgvEstatusMensualDisponibilidad.Rows.Add("Lavadoras");
                        dgvEstatusMensualRendimiento.Rows.Add("Lavadoras");
                        dgvEstatusMensualCalidad.Rows.Add("Lavadoras");
                        CargarArea("Lavadoras", i);
                        break;
                    case 3:
                        dgvEstatusMensual.Rows.Add("Ensamble Final");
                        dgvEstatusMensualDisponibilidad.Rows.Add("Ensamble Final");
                        dgvEstatusMensualRendimiento.Rows.Add("Ensamble Final");
                        dgvEstatusMensualCalidad.Rows.Add("Ensamble Final");
                        CargarArea("Ensamble Final", i);
                        break;
                    case 4:
                        dgvEstatusMensual.Rows.Add("Soldadoras");
                        dgvEstatusMensualDisponibilidad.Rows.Add("Soldadoras");
                        dgvEstatusMensualRendimiento.Rows.Add("Soldadoras");
                        dgvEstatusMensualCalidad.Rows.Add("Soldadoras");
                        CargarArea("Soldadoras", i);
                        break;
                    case 5:
                        dgvEstatusMensual.Rows.Add("Robot SLs");
                        dgvEstatusMensualDisponibilidad.Rows.Add("Robot SLs");
                        dgvEstatusMensualRendimiento.Rows.Add("Robot SLs");
                        dgvEstatusMensualCalidad.Rows.Add("Robot SLs");
                        CargarArea("Robot SLs", i);
                    break;
                }
                dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, i, diasMes);
                dgvEstatusMensualDisponibilidad = CalcularPromedioPorArea(dgvEstatusMensualDisponibilidad, i, diasMes);
                dgvEstatusMensualRendimiento = CalcularPromedioPorArea(dgvEstatusMensualRendimiento, i, diasMes);
                dgvEstatusMensualCalidad = CalcularPromedioPorArea(dgvEstatusMensualCalidad, i, diasMes);
            }
            dgvEstatusMensual.Rows.Add("Planta");
            dgvEstatusMensualDisponibilidad.Rows.Add("Planta");
            dgvEstatusMensualRendimiento.Rows.Add("Planta");
            dgvEstatusMensualCalidad.Rows.Add("Planta");
            for (int j = 0; j <= diasMes; j++)
            {
                dgvEstatusMensual = CalcularPromedioPorPlanta(dgvEstatusMensual, j);
                dgvEstatusMensualDisponibilidad = CalcularPromedioPorPlanta(dgvEstatusMensualDisponibilidad, j);
                dgvEstatusMensualRendimiento = CalcularPromedioPorPlanta(dgvEstatusMensualRendimiento, j);
                dgvEstatusMensualCalidad = CalcularPromedioPorPlanta(dgvEstatusMensualCalidad, j);
            }
        }

        private void ConsultaPorArea(int diasMes, int contadorMaquina)
        {
            //MessageBox.Show(contadorMaquina.ToString());
            for (int i = 0; i < contadorMaquina - 1; i++)
            {
                cboMaquina.SelectedIndex = i;
                //MessageBox.Show(cboMaquina.Text);
                dgvEstatusMensual.Rows.Add(cboMaquina.Text);
                CargarMaquina(i);

            }
            dgvEstatusMensual.Rows.Add(cboArea.Text);
            dgvEstatusMensualDisponibilidad.Rows.Add(cboArea.Text);
            dgvEstatusMensualRendimiento.Rows.Add(cboArea.Text);
            dgvEstatusMensualCalidad.Rows.Add(cboArea.Text);
            CargarAreaMaquinas(cboArea.Text, contadorMaquina);
            dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, contadorMaquina - 1, diasMes);
            dgvEstatusMensualDisponibilidad = CalcularPromedioPorArea(dgvEstatusMensualDisponibilidad, 0, diasMes);
            dgvEstatusMensualRendimiento = CalcularPromedioPorArea(dgvEstatusMensualRendimiento, 0, diasMes);
            dgvEstatusMensualCalidad = CalcularPromedioPorArea(dgvEstatusMensualCalidad, 0, diasMes);
        }

        private void ConsultaPorMaquina(int diasMes)
        {
            dgvEstatusMensual.Rows.Add(cboMaquina.Text);
            dgvEstatusMensualDisponibilidad.Rows.Add(cboMaquina.Text);
            dgvEstatusMensualRendimiento.Rows.Add(cboMaquina.Text);
            dgvEstatusMensualCalidad.Rows.Add(cboMaquina.Text);
            CargarMaquina();
            dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, 0, diasMes);
            dgvEstatusMensualDisponibilidad = CalcularPromedioPorArea(dgvEstatusMensualDisponibilidad, 0, diasMes);
            dgvEstatusMensualRendimiento = CalcularPromedioPorArea(dgvEstatusMensualRendimiento, 0, diasMes);
            dgvEstatusMensualCalidad = CalcularPromedioPorArea(dgvEstatusMensualCalidad, 0, diasMes);
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


        #region CARGA DE AREAS (GENERAL Y DESGLOSE DE MAQUINAS RESPECTIVAMENTE)
        public void CargarArea(string area, int i)
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            int mes = Convert.ToInt32(objectMensualBLL.Fecha);
            int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
            float OEEDia = 0, DisponibilidadDia = 0, RendimientoDia = 0, CalidadDia = 0;
            int contadorDias = 1;
            DateTime Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
            for (int j = 0; j < diasMes; j++)
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT fecha, turno, area, machine, target, piecesProduced, " +
                "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
                "OEE FROM NewOEEPerMachine WHERE fecha='" + Fecha.ToString("dd/MM/yyyy") + "' AND area='" + area + "'").Tables[0];
                if (dgvFiltro.Rows.Count > 0)
                {
                    ConsultarEstatusProduccionArea(dgvFiltro);
                    OEEDia = (float)Math.Round(CalcularOEEDia(dgvEstatusProduccionPorArea, 9), 2);
                    DisponibilidadDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 6);
                    RendimientoDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 7);
                    CalidadDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 8);       
                    #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
                    if (Double.IsNaN(OEEDia))
                    {
                        OEEDia = 0;
                    }
                    if (Double.IsNaN(DisponibilidadDia))
                    {
                        DisponibilidadDia = 0;
                    }
                    if (Double.IsNaN(RendimientoDia))
                    {
                        RendimientoDia = 0;
                    }
                    if (Double.IsNaN(CalidadDia))
                    {
                        CalidadDia = 0;
                    }
                    #endregion

                }
                dgvEstatusMensual.Rows[i].Cells[j + 1].Value = OEEDia * 100;
                dgvEstatusMensual.Rows[i].Cells[j + 1].Style.ForeColor = Color.White;
                dgvEstatusMensual.Rows[i].Cells[j + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
                #region DATAGRIDVIEWS PARA CALCULO DE INDICADORES DE DISPONIBILIDAD, RENDIMIENTO Y CALIDAD
                dgvEstatusMensualDisponibilidad.Rows[i].Cells[j + 1].Value = DisponibilidadDia * 100;
                dgvEstatusMensualRendimiento.Rows[i].Cells[j + 1].Value = RendimientoDia * 100;
                dgvEstatusMensualCalidad.Rows[i].Cells[j + 1].Value = CalidadDia * 100;
                #endregion
                if (contadorDias < diasMes)
                {
                    contadorDias++;
                    Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
                    dgvEstatusProduccionPorArea.DataSource = null;
                    dgvEstatusProduccionPorArea.Rows.Clear();
                    dgvFiltro.DataSource = null;
                    dgvFiltro.Rows.Clear();
                }
                OEEDia = 0;
                DisponibilidadDia = 0;
                RendimientoDia = 0;
                CalidadDia = 0;
            }
            
        }

        public void CargarAreaMaquinas(string area, int i)
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            int mes = Convert.ToInt32(objectMensualBLL.Fecha);
            int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
            float OEEDia = 0, DisponibilidadDia = 0, RendimientoDia = 0, CalidadDia = 0;
            int contadorDias = 1;
            DateTime Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
            for (int j = 0; j < diasMes; j++)
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT fecha, turno, area, machine, target, piecesProduced, " +
                "downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, " +
                "OEE FROM NewOEEPerMachine WHERE fecha='" + Fecha.ToString("dd/MM/yyyy") + "' AND area='" + area + "'").Tables[0];
                if (dgvFiltro.Rows.Count > 0)
                {
                    ConsultarEstatusProduccionArea(dgvFiltro);
                    OEEDia = (float)Math.Round(CalcularOEEDia(dgvEstatusProduccionPorArea, 9), 2);
                    DisponibilidadDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 6);
                    RendimientoDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 7);
                    CalidadDia = CalcularOEEDia(dgvEstatusProduccionPorArea, 8);
                    #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
                    if (Double.IsNaN(OEEDia))
                    {
                        OEEDia = 0;
                    }
                    if (Double.IsNaN(DisponibilidadDia))
                    {
                        DisponibilidadDia = 0;
                    }
                    if (Double.IsNaN(RendimientoDia))
                    {
                        RendimientoDia = 0;
                    }
                    if (Double.IsNaN(CalidadDia))
                    {
                        CalidadDia = 0;
                    }
                    #endregion

                }
                dgvEstatusMensual.Rows[i - 1].Cells[j + 1].Value = OEEDia * 100;
                dgvEstatusMensual.Rows[i - 1].Cells[j + 1].Style.ForeColor = Color.White;
                dgvEstatusMensual.Rows[i - 1].Cells[j + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
                #region DATAGRIDVIEWS PARA CALCULO DE INDICADORES DE DISPONIBILIDAD, RENDIMIENTO Y CALIDAD
                dgvEstatusMensualDisponibilidad.Rows[0].Cells[j + 1].Value = DisponibilidadDia * 100;
                dgvEstatusMensualRendimiento.Rows[0].Cells[j + 1].Value = RendimientoDia * 100;
                dgvEstatusMensualCalidad.Rows[0].Cells[j + 1].Value = CalidadDia * 100;
                #endregion
                if (contadorDias < diasMes)
                {
                    contadorDias++;
                    Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
                    dgvEstatusProduccionPorArea.DataSource = null;
                    dgvEstatusProduccionPorArea.Rows.Clear();
                    dgvFiltro.DataSource = null;
                    dgvFiltro.Rows.Clear();
                }
                OEEDia = 0;
                DisponibilidadDia = 0;
                RendimientoDia = 0;
                CalidadDia = 0;
            }

        }
        #endregion

        #region Carga de Maquinas (1 sobrecarga)
        public void CargarMaquina()
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            string area = objectMensualBLL.Area;
            string maquina = objectMensualBLL.Maquina;
            int mes = Convert.ToInt32(objectMensualBLL.Fecha);
            int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
            float OEE = 0, Disponibilidad = 0, Rendimiento = 0, Calidad = 0;
            int contadorDias = 1;
            DateTime Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
            for(int i = 0; i < diasMes; i++)
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT startTime, endTime, partNumber, " +
                "target, piecesProduced, variation, downTime, scrapPieces, Disponibilidad, " +
                "Rendimiento, Calidad, OEE FROM NewOEEPerMachine WHERE fecha='"+ Fecha.ToString("dd/MM/yyyy") +"' AND area='" + area + "' " +
                "AND machine='" + maquina + "'").Tables[0];
                
                    Disponibilidad = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 8), 2) * 100;
                    Rendimiento = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 9), 2) * 100;
                    Calidad = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 10), 2) * 100;
                    OEE = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 11), 2) * 100;
                    #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
                    if (Double.IsNaN(OEE))
                    {
                        OEE = 0;
                    }
                    if (Double.IsNaN(Disponibilidad))
                    {
                        Disponibilidad = 0;
                    }
                    if (Double.IsNaN(Rendimiento))
                    {
                        Rendimiento = 0;
                    }
                    if (Double.IsNaN(Calidad))
                    {
                        Calidad = 0;
                    }
                
                #endregion
                dgvEstatusMensual.Rows[0].Cells[i + 1].Value = OEE;
                dgvEstatusMensual.Rows[0].Cells[i + 1].Style.ForeColor = Color.White;
                dgvEstatusMensual.Rows[0].Cells[i + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
                #region DATAGRIDVIEWS PARA CALCULO DE INDICADORES DE DISPONIBILIDAD, RENDIMIENTO Y CALIDAD
                dgvEstatusMensualDisponibilidad.Rows[0].Cells[i + 1].Value = Disponibilidad;
                dgvEstatusMensualRendimiento.Rows[0].Cells[i + 1].Value = Rendimiento;
                dgvEstatusMensualCalidad.Rows[0].Cells[i + 1].Value = Calidad;
                #endregion
                if (contadorDias < diasMes)
                {
                    contadorDias++;
                    Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
                    dgvFiltro.DataSource = null;
                    dgvFiltro.Rows.Clear();
                }
            }
        }

        public void CargarMaquina(int contador)
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            string area = objectMensualBLL.Area;
            string maquina = objectMensualBLL.Maquina;
            int mes = Convert.ToInt32(objectMensualBLL.Fecha);
            int diasMes = DateTime.DaysInMonth(dtpFecha.Value.Year, mes);
            float OEE = 0, Disponibilidad = 0, Rendimiento = 0, Calidad = 0;
            int contadorDias = 1;
            DateTime Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
            for (int i = 0; i < diasMes; i++)
            {
                dgvFiltro.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT startTime, endTime, partNumber, " +
                "target, piecesProduced, variation, downTime, scrapPieces, Disponibilidad, " +
                "Rendimiento, Calidad, OEE FROM NewOEEPerMachine WHERE fecha='" + Fecha.ToString("dd/MM/yyyy") + "' AND area='" + area + "' " +
                "AND machine='" + maquina + "'").Tables[0];

                Disponibilidad = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 8), 2) * 100;
                Rendimiento = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 9), 2) * 100;
                Calidad = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 10), 2) * 100;
                OEE = (float)Math.Round(objectConsultaDAL.CalcularIndicadorMesMaquina(dgvFiltro, 11), 2) * 100;
                #region CONDICIONALES PARA SABER SI LOS INDICADORES POR DIA ESTAN VACÍOS
                if (Double.IsNaN(OEE))
                {
                    OEE = 0;
                }
                if (Double.IsNaN(Disponibilidad))
                {
                    Disponibilidad = 0;
                }
                if (Double.IsNaN(Rendimiento))
                {
                    Rendimiento = 0;
                }
                if (Double.IsNaN(Calidad))
                {
                    Calidad = 0;
                }

                #endregion
                dgvEstatusMensual.Rows[contador].Cells[i + 1].Value = OEE;
                dgvEstatusMensual.Rows[contador].Cells[i + 1].Style.ForeColor = Color.White;
                dgvEstatusMensual.Rows[contador].Cells[i + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
                //#region DATAGRIDVIEWS PARA CALCULO DE INDICADORES DE DISPONIBILIDAD, RENDIMIENTO Y CALIDAD
                //dgvEstatusMensualDisponibilidad.Rows[0].Cells[i + 1].Value = Disponibilidad;
                //dgvEstatusMensualRendimiento.Rows[0].Cells[i + 1].Value = Rendimiento;
                //dgvEstatusMensualCalidad.Rows[0].Cells[i + 1].Value = Calidad;
                //#endregion
                if (contadorDias < diasMes)
                {
                    contadorDias++;
                    Fecha = new DateTime(dtpFecha.Value.Year, mes, contadorDias);
                    dgvFiltro.DataSource = null;
                    dgvFiltro.Rows.Clear();
                }
            }
            dgvEstatusMensual = CalcularPromedioPorArea(dgvEstatusMensual, contador, diasMes);

        }
        #endregion

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
            if(restador == dias)
                restador = dias - 1;
            promOEE = (float)Math.Round((sumaOEE / (dias - restador)), 2);
            objectDataGridView.Rows[i].Cells[dias + 1].Value = promOEE;
            objectDataGridView.Rows[i].Cells[dias + 1].Style.ForeColor = Color.White;
            objectDataGridView.Rows[i].Cells[dias + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);
            return objectDataGridView;
        }

        public DataGridView CalcularPromedioPorPlanta(DataGridView objectDataGridView, int j)
        {
            float sumaPlanta = 0;
            float contadorPlanta = 0;
            float promPlanta = 0;
            int restador = 0;
            for(int i = 0; i < 6; i++)
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
            objectDataGridView.Rows[6].Cells[j + 1].Style.ForeColor = Color.White;
            objectDataGridView.Rows[6].Cells[j + 1].Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 7.25f, FontStyle.Bold);

            return objectDataGridView;
        }

        public DataGridView EstablecerColorCeldas(DataGridView objectDataGridView, int dias)
        {
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                for (int j = 1; j <= dias + 1; j++)
                {
                    if (Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) >= 75)
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }
                    else if (Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) < 75 && Convert.ToInt32(objectDataGridView.Rows[i].Cells[j].Value) >= 70)
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        objectDataGridView.Rows[i].Cells[j].Style.ForeColor= Color.Black;
                    }
                    else
                    {
                        objectDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                }
            }
            return objectDataGridView;
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
            if(Rendimiento > 100)
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
                return Color.Green;
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

        #region CARGA DE CONTRIBUIDORES (Por Planta, por Area y por Maquina)
        public void CargarContribuidoresPlanta()
        {
            int mes = dtpFecha.Value.Month;
            int anual = dtpFecha.Value.Year;

            //REGISTRO DE SCRAP
            dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT nombreScrap, numPiezas FROM NewRegistroScrap " +
            "WHERE mes=" + mes + " AND anual=" + anual + "").Tables[0];
            dgvRegistroScrap.Columns[0].HeaderText = "Nombre de Scrap";
            dgvRegistroScrap.Columns[1].HeaderText = "Numero de piezas";
            //Sumamos los elementos repetidos del DataGridView
            dgvRegistroScrap = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroScrap);
            //Ordenamos de mayor a menor (Orden descendente)
            DataGridViewColumn numPiezas = dgvRegistroScrap.Columns[1];
            dgvRegistroScrap.Sort(numPiezas, ListSortDirection.Descending);
            dgvRegistroScrap = UpdateSizeDgv(dgvRegistroScrap);
            chartMuestreo1 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroScrap, chartMuestreo1);

            //REGISTRO DE TIEMPO MUERTO
            dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT razonTiempoMuerto, minutosTiempoMuerto " +
            "FROM NewRegistroTiempoMuerto WHERE mes=" + mes + " AND anual=" + anual + "").Tables[0];
            dgvRegistroTiempoMuerto.Columns[0].HeaderText = "Razon de Tiempo Muerto";
            dgvRegistroTiempoMuerto.Columns[1].HeaderText = "Minutos";
            //Sumamos los elementos repetidos del DataGridView
            dgvRegistroTiempoMuerto = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroTiempoMuerto);
            //Ordenamos de mayor a menor (Orden descendente)
            DataGridViewColumn minutos = dgvRegistroTiempoMuerto.Columns[1];
            dgvRegistroTiempoMuerto.Sort(minutos, ListSortDirection.Descending);
            dgvRegistroTiempoMuerto = UpdateSizeDgv(dgvRegistroTiempoMuerto);
            chartMuestreo2 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroTiempoMuerto, chartMuestreo2);
        }

        public void CargarContribuidoresArea()
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            int mes = dtpFecha.Value.Month;
            int anual = dtpFecha.Value.Year;
            string area = objectMensualBLL.Area;
            //REGISTRO DE SCRAP
            dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT nombreScrap, numPiezas FROM NewRegistroScrap " +
            "WHERE mes=" + mes + " AND anual=" + anual + " AND area='" + area + "'").Tables[0];
            dgvRegistroScrap.Columns[0].HeaderText = "Nombre de Scrap";
            dgvRegistroScrap.Columns[1].HeaderText = "Numero de piezas";
            //Sumamos los elementos repetidos del DataGridView
            dgvRegistroScrap = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroScrap);
            //Ordenamos de mayor a menor (Orden descendente)
            DataGridViewColumn numPiezas = dgvRegistroScrap.Columns[1];
            dgvRegistroScrap.Sort(numPiezas, ListSortDirection.Descending);
            dgvRegistroScrap = UpdateSizeDgv(dgvRegistroScrap);
            chartMuestreo1 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroScrap, chartMuestreo1);

            //REGISTRO DE TIEMPO MUERTO
            dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT razonTiempoMuerto, minutosTiempoMuerto " +
            "FROM NewRegistroTiempoMuerto WHERE mes=" + mes + " AND anual=" + anual + " AND area='" + area + "'").Tables[0];
            dgvRegistroTiempoMuerto.Columns[0].HeaderText = "Razon de Tiempo Muerto";
            dgvRegistroTiempoMuerto.Columns[1].HeaderText = "Minutos";
            //Sumamos los elementos repetidos del DataGridView
            dgvRegistroTiempoMuerto = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroTiempoMuerto);
            //Ordenamos de mayor a menor (Orden descendente)
            DataGridViewColumn minutos = dgvRegistroTiempoMuerto.Columns[1];
            dgvRegistroTiempoMuerto.Sort(minutos, ListSortDirection.Descending);
            dgvRegistroTiempoMuerto = UpdateSizeDgv(dgvRegistroTiempoMuerto);
            chartMuestreo2 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroTiempoMuerto, chartMuestreo2);
        }

        public void CargarContribuidoresMaquina()
        {
            MensualBLL objectMensualBLL = RecuperarInformacion();
            int mes = dtpFecha.Value.Month;
            int anual = dtpFecha.Value.Year;
            string area = objectMensualBLL.Area;
            string maquina = objectMensualBLL.Maquina;
            //REGISTRO DE SCRAP
            dgvRegistroScrap.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT nombreScrap, numPiezas FROM NewRegistroScrap " +
            "WHERE mes=" + mes + " AND anual=" + anual + " AND area='" + area + "' AND machine='" + maquina + "'").Tables[0];
            dgvRegistroScrap.Columns[0].HeaderText = "Nombre de Scrap";
            dgvRegistroScrap.Columns[1].HeaderText = "Numero de piezas";
            //Sumamos los elementos repetidos del DataGridView
            dgvRegistroScrap = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroScrap);
            //Ordenamos de mayor a menor (Orden descendente)
            DataGridViewColumn numPiezas = dgvRegistroScrap.Columns[1];
            dgvRegistroScrap.Sort(numPiezas, ListSortDirection.Descending);
            dgvRegistroScrap = UpdateSizeDgv(dgvRegistroScrap);
            chartMuestreo1 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroScrap, chartMuestreo1);

            //REGISTRO DE TIEMPO MUERTO
            dgvRegistroTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT razonTiempoMuerto, minutosTiempoMuerto " +
            "FROM NewRegistroTiempoMuerto WHERE mes=" + mes + " AND anual=" + anual + " AND area='" + area + "' AND machine='" + maquina + "'").Tables[0];
            dgvRegistroTiempoMuerto.Columns[0].HeaderText = "Razon de Tiempo Muerto";
            dgvRegistroTiempoMuerto.Columns[1].HeaderText = "Minutos";
            //Sumamos los elementos repetidos del DataGridView
            dgvRegistroTiempoMuerto = objectConsultaDAL.SumarElementosRepetidos(dgvRegistroTiempoMuerto);
            //Ordenamos de mayor a menor (Orden descendente)
            DataGridViewColumn minutos = dgvRegistroTiempoMuerto.Columns[1];
            dgvRegistroTiempoMuerto.Sort(minutos, ListSortDirection.Descending);
            dgvRegistroTiempoMuerto = UpdateSizeDgv(dgvRegistroTiempoMuerto);
            chartMuestreo2 = objectConsultaDAL.CargarChartContribuidor(dgvRegistroTiempoMuerto, chartMuestreo2);
        }
        #endregion

        private void btnDescargaExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvEstatusMensual);
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

        private void btnCapturaPantalla_Click(object sender, EventArgs e)
        {
            CapturaPantalla capturaPantalla = new CapturaPantalla();
            capturaPantalla.Show();
        }

        private void btnGeneraPdf_Click(object sender, EventArgs e)
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
            guardar.FileName = "Reporte Mensual_" + cboArea.Text + " - " + cboMaquina.Text + "_" + dtpFecha.Value.ToString("MM-yyyy") + ".pdf";
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                    PdfWriter wri = PdfWriter.GetInstance(doc, stream);
                    //Open document to write
                    doc.Open();
                    //Write Title
                    Paragraph paragrahp = new Paragraph("REPORTE MENSUAL - " + cboArea.Text + " - " + cboMaquina.Text);
                    //Add paragraph to doc
                    doc.Add(paragrahp);
                    doc.Add(new Paragraph("\n"));

                    #region CREACION DE TABLA PARA DATAGRIDVIEW DE CONSULTA
                    //Create and display the table for DataGridView
                    PdfPTable tblReport = new PdfPTable(dgvEstatusMensual.ColumnCount);
                    tblReport.WidthPercentage = 95;
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    //Insertion Table Headers
                    /*PdfPCell clMachine = new PdfPCell(new Phrase("Machine", _standardFont));
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
                    clOEE.BorderWidthBottom = 0.75f; */
                    for(int i = 0; i < dgvEstatusMensual.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dgvEstatusMensual.Columns[i].Name, _standardFont));
                        cell.BorderWidth = 0;
                        cell.BorderWidthBottom = 0.75f;
                        //Add cells to the table
                        tblReport.AddCell(cell);
                    }
                   
                    //Go through the DataGridView
                    for (int i = 0; i < dgvEstatusMensual.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvEstatusMensual.ColumnCount; j++)
                        {
                            string cadenaCelda = dgvEstatusMensual.Rows[i].Cells[j].Value.ToString();
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

                    #region TABLA TOTAL DE CONTRIBUIDORES

                    #endregion
                    //Close the document to finalize the actions
                    doc.Close();
                    //Close stream (memory)
                    stream.Close();
                }

            }
        }

        private void dgvEstatusMensual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (Char)Keys.Enter)
            {
                int i = dgvEstatusMensual.CurrentRow.Index;
                MessageBox.Show(i.ToString());
            }
        }

        private void CalcularPiezasMeta(string instruccion)
        {
            DataSet ds = objectConsultaDAL.MostrarDatosCommand(instruccion);
            System.Data.DataTable dt = new System.Data.DataTable();
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
