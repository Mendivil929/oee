using Microsoft.Office.Interop.Excel;
using OEE1.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OEE1.PL
{
    public partial class PantallaModificacion : Form
    {
        consultaDAL objectConsultaDAL = new consultaDAL();
        conexionDAL connection;
        string Fecha, Turno, Area, Maquina;
        int ID = 0, Target = 0;
        float TiempoMuertoPlaneado = 0;

        public PantallaModificacion()
        {
            InitializeComponent();
            CargarCampos();
        }
        public PantallaModificacion(int registerNumber, string fecha, string turno, string area, string maquina, string horaInicial, string horaFinal, string numeroParte, int target, int piezasProducidas, int DowntimeMinutes, int ScrapPieces)
        {
            InitializeComponent();
            CargarCampos();
            connection = new conexionDAL();
            ID = registerNumber;
            Fecha = fecha;
            Turno = turno;
            Area = area;
            Maquina = maquina;
            Target = target;
            //DEFINE LAS VARIABLES CONSULTORAS PARA PODER HACER EL UPDATE
            cboStartTime.Text = horaInicial;
            cboEndTime.Text = horaFinal;
            cboPartNumber.Text = numeroParte;
            numUDPiecesProduced.Value = piezasProducidas;
            numUDDowntime.Value = DowntimeMinutes;
            numUDScrapPieces.Value = ScrapPieces; 

            //CALCULAMOS EL TIEMPO MUERTO PLANEADO DE LA FILA
            int tiempoTrabajo = DeterminarMinutos();
            float CT = 0; float.TryParse(cboPartNumber.SelectedValue.ToString(), out CT);
            TiempoMuertoPlaneado = tiempoTrabajo - ((Target * CT) / 60);
        }

        public void CargarCampos()
        {
            cboStartTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM tiempoEntrada").Tables[0];
            cboStartTime.DisplayMember = "startTime";
            cboStartTime.ValueMember = "startTime";

            //cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
            //    "tiempo").Tables[0];
            //cboEndTime.DisplayMember = "End Time";
            //cboEndTime.ValueMember = "End Time";

            cboPartNumber.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM NumerosDeParte").Tables[0];
            cboPartNumber.DisplayMember = "parte";
            cboPartNumber.ValueMember = "CT";
        }

        private void cboStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboStartTime.Text)
            {
                case "00:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='0:00:00'").Tables[0];
                    break;
                case "00:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='0:30:00'").Tables[0];
                    break;
                case "01:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='1:00:00'").Tables[0];
                    break;
                case "01:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='1:30:00'").Tables[0];
                    break;
                case "02:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='2:00:00'").Tables[0];
                    break;
                case "02:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='2:30:00'").Tables[0];
                    break;
                case "03:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='3:00:00'").Tables[0];
                    break;
                case "03:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='3:30:00'").Tables[0];
                    break;
                case "04:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='4:00:00'").Tables[0];
                    break;
                case "04:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='4:30:00'").Tables[0];
                    break;
                case "05:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='5:00:00'").Tables[0];
                    break;
                case "05:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='5:30:00'").Tables[0];
                    break;
                case "06:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='6:00:00'").Tables[0];
                    break;
                case "06:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='6:30:00'").Tables[0];
                    break;
                case "07:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='7:00:00'").Tables[0];
                    break;
                case "07:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='7:30:00'").Tables[0];
                    cboEndTime.DisplayMember = "End Time";
                    cboEndTime.ValueMember = "End Time";
                    break;
                case "08:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='8:00:00'").Tables[0];
                    break;
                case "08:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='8:30:00'").Tables[0];
                    break;
                case "09:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='9:00:00'").Tables[0];
                    break;
                case "09:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='9:30:00'").Tables[0];
                    break;
                case "10:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='10:00:00'").Tables[0];
                    break;
                case "10:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='10:30:00'").Tables[0];
                    break;
                case "11:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='11:00:00'").Tables[0];
                    break;
                case "11:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='11:30:00'").Tables[0];
                    break;
                case "12:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='12:00:00'").Tables[0];
                    break;
                case "12:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='12:30:00'").Tables[0];
                    break;
                case "13:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='13:00:00'").Tables[0];
                    break;
                case "13:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='13:30:00'").Tables[0];
                    break;
                case "14:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='14:00:00'").Tables[0];
                    break;
                case "14:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='14:30:00'").Tables[0];
                    break;
                case "15:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='15:00:00'").Tables[0];
                    break;
                case "15:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='15:30:00'").Tables[0];
                    break;
                case "16:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='16:00:00'").Tables[0];
                    break;
                case "16:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='16:30:00'").Tables[0];
                    break;
                case "17:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='17:00:00'").Tables[0];
                    break;
                case "17:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='17:30:00'").Tables[0];
                    break;
                case "18:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='18:00:00'").Tables[0];
                    break;
                case "18:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='18:30:00'").Tables[0];
                    break;
                case "19:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='19:00:00'").Tables[0];
                    break;
                case "19:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='19:30:00'").Tables[0];
                    break;
                case "20:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='20:00:00'").Tables[0];
                    break;
                case "20:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='20:30:00'").Tables[0];
                    break;
                case "21:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='21:00:00'").Tables[0];
                    break;
                case "21:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='21:30:00'").Tables[0];
                    break;
                case "22:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='22:00:00'").Tables[0];
                    break;
                case "22:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='22:30:00'").Tables[0];
                    break;
                case "23:00:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='23:00:00'").Tables[0];
                    break;
                case "23:30:00":
                    cboEndTime.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT [End Time] FROM " +
                    "tiempo WHERE [Start Time]='23:30:00'").Tables[0];
                    break;
            }
            cboEndTime.DisplayMember = "End Time";
            cboEndTime.ValueMember = "End Time";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //DEFINICION DE VARIABLES
            DateTime date = DateTime.ParseExact(Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            int mes = date.Month;
            int anual = date.Year;
            string horaInicial = cboStartTime.Text;
            string horaFinal = cboEndTime.Text;
            string numeroDeParte = cboPartNumber.Text;
            int piezasProducidas = Convert.ToInt32(numUDPiecesProduced.Value);
            int DowntimeMinutes = Convert.ToInt32(numUDDowntime.Value);
            int piezasScrap = Convert.ToInt32(numUDScrapPieces.Value);
            //Calculando nuevamente CT, target, piezasCalidad
            int tiempoTrabajo = DeterminarMinutos();
            float CT = 0; float.TryParse(cboPartNumber.SelectedValue.ToString(), out CT);
            float tiempoPlanificado = (tiempoTrabajo - TiempoMuertoPlaneado) * 60;
            float tiempoFuncionamiento = tiempoPlanificado - (DowntimeMinutes * 60);
            float Target = tiempoPlanificado / CT;
            int piezasCalidad = piezasProducidas - piezasScrap;
            //Calculando Disponibilidad, Rendimiento, Calidad, OEE
            float Disponibilidad = tiempoFuncionamiento / tiempoPlanificado;
            float Rendimiento = (CT * piezasProducidas) / tiempoPlanificado;
            float Calidad = (float)piezasCalidad / (float)piezasProducidas;
            float OEE = (Disponibilidad * Rendimiento * Calidad);

            float variation = piezasProducidas - Target;
            //UPDATE COMMAND
            SqlCommand updateCommand = new SqlCommand("UPDATE NewOEEPerMachine SET startTime='" + horaInicial + "', endTime='" + horaFinal + "', partNumber='" + numeroDeParte + "', " +
                "target=" + Target + ", piecesProduced=" + piezasProducidas + ", variation=" + variation + ", downTime=" + DowntimeMinutes + ", " +
                "scrapPieces=" + piezasScrap + ", Disponibilidad=" + Disponibilidad + ", Rendimiento=" + Rendimiento + ", Calidad=" + Calidad + ", " +
                "OEE=" + OEE + " WHERE ID=" + ID + "");
            if (connection.EjecutarComando(updateCommand))
                MessageBox.Show("Los datos han sido actualizados correctamente!!");
            else
                MessageBox.Show("Hubo un problema al actualizar los datos, intentelo de nuevo...");
            this.Close();
        }

        private int DeterminarMinutos()
        {
            int minutos = 0;
            DateTime tiempoInicial = DateTime.Parse(cboStartTime.Text);
            DateTime tiempoFinal = DateTime.Parse(cboEndTime.Text);
            if (tiempoInicial.Minute == 30 || tiempoFinal.Minute == 30)
            {
                minutos = 30;
            }
            else
            {
                minutos = 60;
            }
            return minutos;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
