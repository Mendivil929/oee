using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OEE1.PL
{
    public partial class ConsultaTiempoMuerto : Form
    {
        public ConsultaTiempoMuerto(DataGridView objectDataGridView, string tipo)
        {
            InitializeComponent();
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                dgvDatosTiempoMuerto.Rows.Add();
                for (int j = 0; j < 2; j++)
                {
                    dgvDatosTiempoMuerto.Rows[i].Cells[j].Value = objectDataGridView.Rows[i].Cells[j + 1].Value;
                }
            }
            dgvDatosTiempoMuerto = UpdateSizeDgv(dgvDatosTiempoMuerto);
        }

        public ConsultaTiempoMuerto(DataGridView objectDataGridView)
        {
            InitializeComponent();
            for (int i = 0; i < objectDataGridView.Rows.Count; i++)
            {
                dgvDatosTiempoMuerto.Rows.Add();
                for (int j = 0; j < 2; j++)
                {
                    dgvDatosTiempoMuerto.Rows[i].Cells[j].Value = objectDataGridView.Rows[i].Cells[j].Value;
                }
            }
            dgvDatosTiempoMuerto = UpdateSizeDgv(dgvDatosTiempoMuerto);
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
            dgvDatosTiempoMuerto.Columns[0].Name = "Razon de Tiempo Muerto";
            dgvDatosTiempoMuerto.Columns[1].Name = "Minutos";
            ExportToExcel(dgvDatosTiempoMuerto);
        }

    }
}
