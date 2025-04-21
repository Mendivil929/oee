using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OEE1.DAL
{
    internal class consultaDAL
    {
        conexionDAL connection;
        public consultaDAL()
        {
            connection = new conexionDAL();
        }

        #region OEE POR MAQUINA
        public DataSet MostrarDatos(string tabla)
        {
            SqlCommand comando = new SqlCommand("SELECT * FROM " + tabla + "");
            return connection.Consulta(comando);
        }
        public DataSet MostrarDatosCommand(string commandSent)
        {
            SqlCommand comando = new SqlCommand(commandSent);
            return connection.Consulta(comando);
        }
        public float CalcularIndicador(DataGridView objectDataGridView, int campoIndicador)
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

        public int CalcularPiezasScrap(DataGridView objectDataGridView)
        {
            int totalPiezasScrap = 0;
            int piezas = 0;
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                int.TryParse(objectDataGridView.Rows[i].Cells[1].Value.ToString(), out piezas);
                totalPiezasScrap += piezas;
            }
            return totalPiezasScrap;
        }

        public int CalcularMinTiempoMuerto(DataGridView objectDataGridView)
        {
            int totalMinTiempoMuerto = 0;
            int minutos = 0;
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                int.TryParse(objectDataGridView.Rows[i].Cells[1].Value.ToString(), out minutos);
                totalMinTiempoMuerto += minutos;
            }
            return totalMinTiempoMuerto;
        }


        #endregion

        public float CalcularIndicadorMesMaquina(DataGridView objectDataGridView, int campoIndicador)
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

        public DataGridView SumarElementosRepetidos (DataGridView objectDataGridView)
        {
            int valorBase = 0, valorFinal = 0;
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                for (int j = i + 1; j < objectDataGridView.Rows.Count; j++)
                {
                    if (objectDataGridView.Rows[j].Cells[0].Value.ToString() == objectDataGridView.Rows[i].Cells[0].Value.ToString())
                    {
                        valorBase = Convert.ToInt32(objectDataGridView.Rows[i].Cells[1].Value);
                        valorFinal = Convert.ToInt32(objectDataGridView.Rows[j].Cells[1].Value);
                        objectDataGridView.Rows[i].Cells[1].Value = valorBase + valorFinal;
                        objectDataGridView.Rows.RemoveAt(j);
                        j--;
                    }
                }
            }
            return objectDataGridView;
        }
        #region SUMAR ELEMENTOS REPETIDOS SOLO PARA oeePorMaquina
        public DataGridView SumarElementosRepetidosPorMaquina(DataGridView objectDataGridView)
        {
            int valorBase = 0, valorFinal = 0;
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                for (int j = i + 1; j < objectDataGridView.Rows.Count; j++)
                {
                    if (objectDataGridView.Rows[j].Cells[1].Value.ToString() == objectDataGridView.Rows[i].Cells[1].Value.ToString())
                    {
                        valorBase = Convert.ToInt32(objectDataGridView.Rows[i].Cells[2].Value);
                        valorFinal = Convert.ToInt32(objectDataGridView.Rows[j].Cells[2].Value);
                        objectDataGridView.Rows[i].Cells[2].Value = valorBase + valorFinal;
                        objectDataGridView.Rows.RemoveAt(j);
                        j--;
                    }
                }
            }
            return objectDataGridView;
        }
        #endregion

        public Chart CargarChartContribuidor(DataGridView objectDataGridView, Chart objectChart)
        {
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                if (i == 5)
                    break;
                objectChart.Series[0].Points.AddXY(objectDataGridView.Rows[i].Cells[0].Value.ToString(), objectDataGridView.Rows[i].Cells[1].Value);
            }
            return objectChart;
        }

        #region CARGAR EL RESPECTIVO CHART SOLO PARA oeePorMaquina
        public Chart CargarChartContribuidorPorMaquina(DataGridView objectDataGridView, Chart objectChart)
        {
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                if (i == 5)
                    break;
                objectChart.Series[0].Points.AddXY(objectDataGridView.Rows[i].Cells[1].Value.ToString(), objectDataGridView.Rows[i].Cells[2].Value);
            }
            return objectChart;
        }
        #endregion

        public Chart limpiarCharts(Chart objectChart)
        {
            foreach (var series in objectChart.Series)
            {
                series.Points.Clear();
            }

            return objectChart;
        }
    }

}