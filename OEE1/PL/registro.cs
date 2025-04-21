using OEE1.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OEE1.PL;
using OEE1.BLL;
using System.Data.SqlClient;

namespace OEE1.PL
{
    public partial class registro : Form
    {
        //Variables de seleccion para DataGridView de Scrap y Tiempo Muerto
        //string nombreScrap = "", razonTiempoMuerto = "";
        //int piezasScrap = 0, minutosTiempoMuerto = 0;
        int indiceScrap = 0, indiceDowntime = 0;
        //Objeto de la clase 'consultaDAL'
        consultaDAL objectConsultaDAL = new consultaDAL();
        RegistroDAL objectRegistroDAL = new RegistroDAL();
        public registro()
        {
            InitializeComponent();
            cargarComboBoxes();
            //dtFecha.MinDate = dtFecha.Value.AddDays(-7);
            dtFecha.Value = DateTime.Now;
            dgvScrap = UpdateSizeDgv(dgvScrap);
            dgvTiempoMuerto = UpdateSizeDgv(dgvTiempoMuerto);
        }

        private void cargarComboBoxes()
        {
            /* Carga de datos cboTurno -> Combo Box para el cambio de turno */
            cboTurno.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT shiftChange FROM Turno WHERE shiftChange != 'Todos los turnos'").Tables[0];
            cboTurno.DisplayMember = "shiftChange";
            cboTurno.ValueMember = "shiftChange";

            /* Carga de datos cboArea -> Combo Box para especificar el area */
            cboArea.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Area FROM nombreAreas WHERE Area != 'Planta'").Tables[0];
            cboArea.DisplayMember = "area";
            cboArea.ValueMember = "area";

            /* Carga de datos cboNumeroParte -> Combo Box para el numero de parte */
            cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte").Tables[0];
            cboNumeroParte.DisplayMember = "parte";
            cboNumeroParte.ValueMember = "CT";
            /* Carga de datos cboHoraInicial -> Combo Box para el tiempo inicial */
            cboHoraInicial.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM " +
                "tiempoEntrada").Tables[0];
            cboHoraInicial.DisplayMember = "startTime";
            cboHoraInicial.ValueMember = "startTime";

            /* Carga de datos cboHoraFinal -> Combo Box para el tiempo final */
            cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                "tiempo WHERE [Start Time]='0:00:00'").Tables[0];
            cboHoraFinal.DisplayMember = "End Time";
            cboHoraFinal.ValueMember = "End Time";

            /* Carga de datos cboMaquina -> Combo Box para maquinas */
            cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina FROM " +
                "AreaMaquinas WHERE Area='Fabricacion' AND Maquina!='Todas'").Tables[0];
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";

            /* Carga de datos cboTiempoMuerto -> Combo Box para maquinas */
            cboTiempoMuertoPlaneado.DataSource = objectConsultaDAL.MostrarDatos("TiempoMuertoPlaneado").Tables[0];
            cboTiempoMuertoPlaneado.DisplayMember = "Razon";
            cboTiempoMuertoPlaneado.ValueMember = "Valor";

            /* Carga de datos cboRazonTiempoMuerto -> Combo Box para la razon de tiempo muerto */
            cboRazonTiempoMuerto.DataSource = objectConsultaDAL.MostrarDatos("ProblemasTiempoMuerto").Tables[0];
            cboRazonTiempoMuerto.DisplayMember = "Problema";
            cboRazonTiempoMuerto.ValueMember = "Problema";

            /* Carga de datos cboRazonTiempoMuerto -> Combo Box para la razon de tiempo muerto */
            cboRazonScrap.DataSource = objectConsultaDAL.MostrarDatos("ProblemasScrap").Tables[0];
            cboRazonScrap.DisplayMember = "Problema de Scrap";
            cboRazonScrap.ValueMember = "Problema de Scrap";
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            numUDPiezasProducidas.Enabled = true;
            btnSubirDatos.Enabled = true;
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
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE CT=1.125").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
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
                    //Definimos las lineas en donde el usuario escogerá el robot
                    numUDPiezasProducidas.Enabled = false;
                    btnSubirDatos.Enabled = false;
                    cboMaquina.DataSource = null;
                    cboMaquina.Items.Clear();
                    cboMaquina.Items.Add("Seleccione Linea");
                    cboMaquina.Items.Add("Linea 1");
                    cboMaquina.Items.Add("Linea 2");
                    cboMaquina.Items.Add("Linea 3");
                    cboMaquina.Items.Add("Linea 4");
                    cboMaquina.SelectedIndex = 0;
                    //cboMaquina.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT Maquina" +
                    //    " FROM AreaMaquinas WHERE Area='Robot SLs' AND Maquina!='Todas'").Tables[0];
                    break;
                default:
                    MessageBox.Show("No se ha hecho ninguna seleccion");
                    break;
            }
            cboMaquina.DisplayMember = "Maquina";
            cboMaquina.ValueMember = "Maquina";

            if(cboArea.Text == "Fabricacion" || cboArea.Text == "Dobladoras" || cboArea.Text == "Ensamble Final" || cboArea.Text == "Soldadoras")
            {
                cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte").Tables[0];
                cboNumeroParte.DisplayMember = "parte";
                cboNumeroParte.ValueMember = "CT";
            }
        }

        private void cboMaquina_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region CONFIGURACION DE LINEAS PARA ROBOTS
            if (cboMaquina.Text == "Linea 1")
            {
                numUDPiezasProducidas.Enabled = true;
                btnSubirDatos.Enabled = true;
                cboMaquina.DataSource = null;
                cboMaquina.Items.Clear();
                cboMaquina.Items.Add("CF8001 - SL1");
                cboMaquina.Items.Add("CF8003 - SL1");
                cboMaquina.SelectedIndex = 0;
            }
            else if (cboMaquina.Text == "Linea 2")
            {
                numUDPiezasProducidas.Enabled = true;
                btnSubirDatos.Enabled = true;
                cboMaquina.DataSource = null;
                cboMaquina.Items.Clear();
                cboMaquina.Items.Add("CF8010 - SL2");
                cboMaquina.Items.Add("CF8011 - SL2");
                cboMaquina.SelectedIndex = 0;
            }
            else if (cboMaquina.Text == "Linea 3")
            {
                numUDPiezasProducidas.Enabled = true;
                btnSubirDatos.Enabled = true;
                cboMaquina.DataSource = null;
                cboMaquina.Items.Clear();
                cboMaquina.Items.Add("CF8005 - SL3");
                cboMaquina.Items.Add("CF8007 - SL3");
                cboMaquina.SelectedIndex = 0;
            }
            else if (cboMaquina.Text == "Linea 4")
            {
                numUDPiezasProducidas.Enabled = true;
                btnSubirDatos.Enabled = true;
                cboMaquina.DataSource = null;
                cboMaquina.Items.Clear();
                cboMaquina.Items.Add("CF8013 - SL4");
                cboMaquina.Items.Add("CF8015 - SL4");
                cboMaquina.SelectedIndex = 0;
            }
            #endregion
            switch (cboMaquina.Text)
            {
                case "CF8001 - SL1":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A375-3815-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8003 - SL1":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A625-3787-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8010 - SL2":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A375-4434-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8011 - SL2":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A625-4380-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8007 - SL3":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A625-3816-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8005 - SL3":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A625-3849-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8015 - SL4":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A625-4240-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;

                case "CF8013 - SL4":
                    cboNumeroParte.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte WHERE parte='A625-4204-G850'").Tables[0];
                    cboNumeroParte.DisplayMember = "parte";
                    cboNumeroParte.ValueMember = "CT";
                    btnRatePerHour.Enabled = true;
                    break;
            }
        }

        private void cboHoraInicial_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboHoraInicial.Text)
            {
                case "00:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='0:00:00'").Tables[0];
                    break;
                case "00:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='0:30:00'").Tables[0];
                    break;
                case "01:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='1:00:00'").Tables[0];
                    break;
                case "01:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='1:30:00'").Tables[0];
                    break;
                case "02:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='2:00:00'").Tables[0];
                    break;
                case "02:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='2:30:00'").Tables[0];
                    break;
                case "03:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='3:00:00'").Tables[0];
                    break;
                case "03:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='3:30:00'").Tables[0];
                    break;
                case "04:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='4:00:00'").Tables[0];
                    break;
                case "04:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='4:30:00'").Tables[0];
                    break;
                case "05:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='5:00:00'").Tables[0];
                    break;
                case "05:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='5:30:00'").Tables[0];
                    break;
                case "06:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='6:00:00'").Tables[0];
                    break;
                case "06:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='6:30:00'").Tables[0];
                    break;
                case "07:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='7:00:00'").Tables[0];
                    break;
                case "07:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='7:30:00'").Tables[0];
                    cboHoraFinal.DisplayMember = "End Time";
                    cboHoraFinal.ValueMember = "End Time";
                    break;
                case "08:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='8:00:00'").Tables[0];
                    break;
                case "08:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='8:30:00'").Tables[0];
                    break;
                case "09:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='9:00:00'").Tables[0];
                    break;
                case "09:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='9:30:00'").Tables[0];
                    break;
                case "10:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='10:00:00'").Tables[0];
                    break;
                case "10:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='10:30:00'").Tables[0];
                    break;
                case "11:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='11:00:00'").Tables[0];
                    break;
                case "11:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='11:30:00'").Tables[0];
                    break;
                case "12:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='12:00:00'").Tables[0];
                    break;
                case "12:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='12:30:00'").Tables[0];
                    break;
                case "13:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='13:00:00'").Tables[0];
                    break;
                case "13:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='13:30:00'").Tables[0];
                    break;
                case "14:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='14:00:00'").Tables[0];
                    break;
                case "14:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='14:30:00'").Tables[0];
                    break;
                case "15:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='15:00:00'").Tables[0];
                    break;
                case "15:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='15:30:00'").Tables[0];
                    break;
                case "16:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='16:00:00'").Tables[0];
                    break;
                case "16:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='16:30:00'").Tables[0];
                    break;
                case "17:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='17:00:00'").Tables[0];
                    break;
                case "17:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='17:30:00'").Tables[0];
                    break;
                case "18:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='18:00:00'").Tables[0];
                    break;
                case "18:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='18:30:00'").Tables[0];
                    break;
                case "19:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='19:00:00'").Tables[0];
                    break;
                case "19:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='19:30:00'").Tables[0];
                    break;
                case "20:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='20:00:00'").Tables[0];
                    break;
                case "20:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='20:30:00'").Tables[0];
                    break;
                case "21:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='21:00:00'").Tables[0];
                    break;
                case "21:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='21:30:00'").Tables[0];
                    break;
                case "22:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='22:00:00'").Tables[0];
                    break;
                case "22:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='22:30:00'").Tables[0];
                    break;
                case "23:00:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='23:00:00'").Tables[0];
                    break;
                case "23:30:00":
                    cboHoraFinal.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='23:30:00'").Tables[0];
                    break;
            }
            cboHoraFinal.DisplayMember = "End Time";
            cboHoraFinal.ValueMember = "End Time";
        }

        private int DeterminarMinutos()
        {
            int minutos = 0;
            DateTime tiempoInicial = DateTime.Parse(cboHoraInicial.Text);
            DateTime tiempoFinal = DateTime.Parse(cboHoraFinal.Text);
            if(tiempoInicial.Minute == 30 || tiempoFinal.Minute == 30)
            {
                minutos = 30;
            }
            else
            {
                minutos = 60;
            }
            return minutos;
        }

        public DataGridView UpdateSizeDgv(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            //dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);

            return dgv;
        }

        private RegistroBLL RecuperarInformacion()
        {
            float CT;
            int tiempoTrabajo = DeterminarMinutos();
            int plannedDownTime = 0;
            int piezasProducidas = 0;

            RegistroBLL objectRegistroBLL = new RegistroBLL();
            objectRegistroBLL.Turno = cboTurno.Text;
            objectRegistroBLL.Area = cboArea.Text;
            objectRegistroBLL.Maquina = cboMaquina.Text;
            objectRegistroBLL.Fecha = dtFecha.Value.ToString("dd/MM/yyyy");
            objectRegistroBLL.Mes = dtFecha.Value.Month;
            objectRegistroBLL.Anual = dtFecha.Value.Year;
            objectRegistroBLL.HoraInicial = cboHoraInicial.Text;
            objectRegistroBLL.HoraFinal = cboHoraFinal.Text;
            objectRegistroBLL.NumeroParte = cboNumeroParte.Text;

            int.TryParse(numUDPiezasProducidas.Value.ToString(), out piezasProducidas);
            objectRegistroBLL.PiezasProducidas = piezasProducidas;

            int.TryParse(cboTiempoMuertoPlaneado.SelectedValue.ToString(), out plannedDownTime);
            objectRegistroBLL.TiempoMuertoPlaneado = plannedDownTime;

            //objectRegistroBLL.NombreScrap = cboRazonScrap.Text; //Pendiente DataGridView de razon de scrap

            objectRegistroBLL.ScrapPieces = objectConsultaDAL.CalcularPiezasScrap(dgvScrap);

            //objectRegistroBLL.RazonDownTime = cboRazonTiempoMuerto.Text; //Pendiente DataGridView de razon de tiempo muerto


            objectRegistroBLL.DownTime = objectConsultaDAL.CalcularMinTiempoMuerto(dgvTiempoMuerto);


            /* CALCULOS DE INDICADORES */
            
            /* DISPONIBILIDAD */
            float tiempoPlanificado = (tiempoTrabajo - objectRegistroBLL.TiempoMuertoPlaneado) * 60;
            float pausas = objectRegistroBLL.DownTime;
            float tiempoFuncionamiento = tiempoPlanificado - (pausas * 60); //1320
            objectRegistroBLL.Disponibilidad = tiempoFuncionamiento / tiempoPlanificado;
                //Calculo del target
                float.TryParse(cboNumeroParte.SelectedValue.ToString(), out CT);
                objectRegistroBLL.Target = tiempoPlanificado / CT;
            /* RENDIMIENTO */
            float totalPiezas = objectRegistroBLL.PiezasProducidas;
            objectRegistroBLL.Rendimiento = (CT * totalPiezas) / tiempoPlanificado;
            /* CALIDAD */
            float piezasCalidad = objectRegistroBLL.PiezasProducidas - objectRegistroBLL.ScrapPieces;
            objectRegistroBLL.Calidad = piezasCalidad / objectRegistroBLL.PiezasProducidas;
            /* OEE */
            float Disponibilidad = objectRegistroBLL.Disponibilidad;
            float Rendimiento = objectRegistroBLL.Rendimiento;
            float Calidad = objectRegistroBLL.Calidad;
            objectRegistroBLL.OEE = (Disponibilidad * Rendimiento * Calidad);

            objectRegistroBLL.Variation = objectRegistroBLL.PiezasProducidas - objectRegistroBLL.Target;

            return objectRegistroBLL;
        }

        private RegistroBLL RecuperarInformacionScrap()
        {
            RegistroBLL objectRegistroBLL = new RegistroBLL();
            int piezasScrap = 0;
            int.TryParse(numUDPiezasScrap.Value.ToString(), out piezasScrap);
            objectRegistroBLL.NombreScrap = cboRazonScrap.Text;
            objectRegistroBLL.ScrapPieces = piezasScrap;

            return objectRegistroBLL;
        }

        private RegistroBLL RecuperarInformacionTiempoMuerto()
        {
            RegistroBLL objectRegistroBLL = new RegistroBLL();
            int minTiempoMuerto = 0;
            int.TryParse(numericUDMinTiempoMuerto.Value.ToString(), out minTiempoMuerto);
            objectRegistroBLL.RazonDownTime = cboRazonTiempoMuerto.Text;
            objectRegistroBLL.DownTime = minTiempoMuerto;

            return objectRegistroBLL;
        }

        private void btnSubirDatos_Click(object sender, EventArgs e)
        {
            RegistroDAL objectRegistroDAL = new RegistroDAL();
            DialogResult resultado = MessageBox.Show("¿Seguro Que Los Datos Son Correctos?", "Verificación de información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //Definicion de variable para condicionales de subida de datos
            RegistroBLL objectRegistroBLL = RecuperarInformacion();
            if(resultado == DialogResult.Yes)
            {
                if(cboNumeroParte.Text == "-" || numUDPiezasProducidas.Value == 0)
                    MessageBox.Show("El número de parte ni el campo de piezas producidas pueden estar vacíos, por favor inserte o seleccione los valores correspondientes.");
                else if((float)numUDPiezasProducidas.Value > objectRegistroBLL.Target)
                {
                    MessageBox.Show("EL target que se espera es de: " + objectRegistroBLL.Target + "pz\nUsted ha indicado: "
                        + numUDPiezasProducidas.Value + "pz\n Si desea capturar más piezas que el target indicado por favor contacte con su supervisor");
                }
                else
                {
                    objectRegistroDAL.Registrar(RecuperarInformacion()); //OEEPerMachine Table
                    objectRegistroDAL.RegistrarScrap(dgvScrap, RecuperarInformacion()); //RegistroScrap Table
                    objectRegistroDAL.RegistrarTiempoMuerto(dgvTiempoMuerto, RecuperarInformacion()); //RegistroTiempoMuerto Table
                    #region Desactivar controles
                    cboTurno.Enabled = false;
                    cboArea.Enabled = false;
                    cboMaquina.Enabled = false;
                    dtFecha.Enabled = false;
                    cboHoraInicial.Enabled = false;
                    cboHoraFinal.Enabled = false;
                    cboNumeroParte.Enabled = false;
                    numUDPiezasProducidas.Enabled = false;
                    cboTiempoMuertoPlaneado.Enabled = false;
                    btnSubirDatos.Enabled = false;
                    cboRazonScrap.Enabled = false;
                    numUDPiezasScrap.Enabled = false;
                    cboRazonTiempoMuerto.Enabled = false;
                    numericUDMinTiempoMuerto.Enabled = false;
                    dgvScrap.Enabled = false;
                    btnAgregarScrap.Enabled = false;
                    btnEliminarScrap.Enabled = false;
                    dgvTiempoMuerto.Enabled = false;
                    btnAgregarTiempoMuerto.Enabled = false;
                    btnEliminarTiempoMuerto.Enabled = false;
                    #endregion
                    MessageBox.Show("Los Datos Han Sido Agregados Satisfactoriamente!!!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if(resultado == DialogResult.No)
            {
                MessageBox.Show("Por favor verifique los datos antes de subirlos");
            }
        }


        private void btnAgregarScrap_Click(object sender, EventArgs e)
        {
            RegistroBLL objectRegistroBLL = RecuperarInformacionScrap();
            string nombreScrap = objectRegistroBLL.NombreScrap;
            int scrapPieces = objectRegistroBLL.ScrapPieces;
            dgvScrap.Rows.Add(nombreScrap, scrapPieces);
            dgvScrap = UpdateSizeDgv(dgvScrap);
        }

        private void SeleccionarScrap(object sender, DataGridViewCellMouseEventArgs e)
        {
            indiceScrap = e.RowIndex;

            dgvScrap.ClearSelection();
            if(indiceScrap >= 0)
                btnEliminarScrap.Enabled = true;
            else
                btnEliminarScrap.Enabled = false;
        }
        
        private void btnEliminarScrap_Click(object sender, EventArgs e)
        {
            dgvScrap.Rows.RemoveAt(indiceScrap);
            dgvScrap = UpdateSizeDgv(dgvScrap);
            btnEliminarScrap.Enabled = false;
        }

        private void btnAgregarTiempoMuerto_Click(object sender, EventArgs e)
        {
            RegistroBLL objectRegistroBLL = RecuperarInformacionTiempoMuerto();
            string razonTiempoMuerto = objectRegistroBLL.RazonDownTime;
            int minutosTiempoMuerto = objectRegistroBLL.DownTime;
            dgvTiempoMuerto.Rows.Add(razonTiempoMuerto, minutosTiempoMuerto);
            dgvTiempoMuerto = UpdateSizeDgv(dgvTiempoMuerto);
        }

        private void SeleccionarTiempoMuerto(object sender, DataGridViewCellMouseEventArgs e)
        {
            indiceDowntime = e.RowIndex;

            dgvTiempoMuerto.ClearSelection();
            if(indiceDowntime >= 0)
                btnEliminarTiempoMuerto.Enabled = true;
            else
                btnEliminarTiempoMuerto.Enabled = false;
        }

        private void btnEliminarTiempoMuerto_Click(object sender, EventArgs e)
        {
            dgvTiempoMuerto.Rows.RemoveAt(indiceDowntime);
            dgvTiempoMuerto = UpdateSizeDgv(dgvTiempoMuerto);
            btnEliminarTiempoMuerto.Enabled = false;
        }

        private void cboNumeroParte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNumeroParte.SelectedIndex > 0)
            {
                btnRatePerHour.Enabled = true;
            }
            else
            {
                btnRatePerHour.Enabled = false;
            }
        }

        private void btnRatePerHour_Click(object sender, EventArgs e)
        {
            float CT; float.TryParse(cboNumeroParte.SelectedValue.ToString(), out CT);
            int hourSeconds = 3600;
            float RatePerHour = hourSeconds / CT;
            float RatePerHalfHour = RatePerHour / 2;
            MessageBox.Show("Rate:\n " + RatePerHour + "pz/Hora\n" + RatePerHalfHour + "pz/30minutos");
        }

    }  
}       
        