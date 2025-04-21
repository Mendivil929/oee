using OEE1.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OEE1.PL
{
    public partial class ConsultaScrap : Form
    {
        consultaDAL objectConsultaDAL = new consultaDAL();
        conexionDAL conn;
        public ConsultaScrap(DataGridView objectDataGridView, string tipo)
        {
            InitializeComponent();
            conn = new conexionDAL();
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                dgvDatosScrap.Rows.Add();
                for (int j = 0; j < 2; j++)
                {
                    dgvDatosScrap.Rows[i].Cells[j].Value = objectDataGridView.Rows[i].Cells[j + 1].Value;
                }
            }
            dgvDatosScrap = UpdateSizeDgv(dgvDatosScrap);
        }

        public ConsultaScrap(DataGridView objectDataGridView)
        {
            InitializeComponent();
            conn = new conexionDAL();
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                dgvDatosScrap.Rows.Add();
                for (int j = 0; j < 2; j++)
                {
                    dgvDatosScrap.Rows[i].Cells[j].Value = objectDataGridView.Rows[i].Cells[j].Value;
                }
            }
            dgvDatosScrap = UpdateSizeDgv(dgvDatosScrap);
        }

        public DataGridView UpdateSizeDgv(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count;
            //var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(Width, totalHeight);
            dgv.BackgroundColor = SystemColors.Control;

            return dgv;
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dgvDatosScrap.Columns[0].Name = "Nombre de Scrap";
            dgvDatosScrap.Columns[1].Name = "Piezas";
            ExportToExcel(dgvDatosScrap);
        }

        private void resolver_lavadoras_Click(object sender, EventArgs e)
        {
            string partNumber = "", horaInicial = "", horaFinal = "";
            int target = 3200, variation = 0, piecesProduced = 0, plannedDownTime = 0, tiempoDeTrabajo = 0, tiempoPlanificado = 0;
            float disponibilidad = 1, rendimiento = 0, calidad = 1, OEE = 0;
            dgvLavadoras.DataSource = objectConsultaDAL.MostrarDatosCommand("SELECT * FROM OEEPerMachine WHERE turno='2do Turno' AND  area='Lavadoras'").Tables[0];
            for (int i = 0; i < dgvLavadoras.Rows.Count; i++)
            {
                if (dgvLavadoras.Rows[i].Cells[5].Value.ToString() == "Lavadora Durr")
                    partNumber = "Tubos - Lavadoras Durr";
                else if (dgvLavadoras.Rows[i].Cells[5].Value.ToString() == "Tecson#1")
                    partNumber = "Tubos - Lavadoras Tecson";
                else if(dgvLavadoras.Rows[i].Cells[5].Value.ToString() == "Tecson#2")
                    partNumber = "Tubos - Lavadoras Tecson 2";
                piecesProduced = Convert.ToInt32(dgvLavadoras.Rows[i].Cells[10].Value);
                variation = piecesProduced - target;
                //Calculando Rendimiento
                horaInicial = dgvLavadoras.Rows[i].Cells[6].Value.ToString();
                horaFinal = dgvLavadoras.Rows[i].Cells[7].Value.ToString();
                tiempoDeTrabajo = DeterminarMinutos(horaInicial, horaFinal);
                plannedDownTime = Convert.ToInt32(dgvLavadoras.Rows[i].Cells[12].Value);
                tiempoPlanificado = (tiempoDeTrabajo - plannedDownTime) * 60;
                rendimiento = (1.125f * piecesProduced) / tiempoPlanificado;
                OEE = (disponibilidad * rendimiento * calidad);
                //SECCION DE ACTUALIZACION
                string fecha = dgvLavadoras.Rows[i].Cells[0].Value.ToString();
                string numeroParte = dgvLavadoras.Rows[i].Cells[8].Value.ToString();
                int meta = Convert.ToInt32(dgvLavadoras.Rows[i].Cells[9].Value);
                int Variacion = Convert.ToInt32(dgvLavadoras.Rows[i].Cells[11].Value);
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "UPDATE OEEPerMachine SET partNumber = '" + partNumber + "', target = " + target + ", variation = " + variation + ", Rendimiento = " + rendimiento + ", OEE = " + OEE + " " +
                    "WHERE fecha='" + fecha + "' AND turno='2do Turno' AND area='Lavadoras' AND startTime='" + horaInicial + "' AND endTime='" + horaFinal + "' AND partNumber='" + numeroParte + "' " +
                    "AND target=" + meta + " AND piecesProduced=" + piecesProduced + " AND variation=" + Variacion + "";
                conn.EjecutarComando(comando);
                resolver_lavadoras.Enabled = false;
            }
        }

        private int DeterminarMinutos(string horaInicial, string horaFinal)
        {
            int minutos = 0;
            DateTime tiempoInicial = DateTime.Parse(horaInicial);
            DateTime tiempoFinal = DateTime.Parse(horaFinal);
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
    }
}