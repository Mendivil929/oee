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
    public partial class MenuOEE : Form
    {
        public MenuOEE()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle =  FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(childForm);
            panelContenedor.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
        }

        private void btnRegistroDatos_Click(object sender, EventArgs e)
        {
            openChildForm(new registro());
        }

        private void btnOEEMaquina_Click(object sender, EventArgs e)
        {
            openChildForm(new oeePorMaquina());
        }

        private void btnOEEArea_Click(object sender, EventArgs e)
        {
            openChildForm(new oeePorArea());
        }

        private void btnReporteMensual_Click(object sender, EventArgs e)
        {
            openChildForm(new ReporteMensual());
        }

        private void MenuOEE_Load(object sender, EventArgs e)
        {
            int ancho = Screen.PrimaryScreen.WorkingArea.Width;
            int alto = Screen.PrimaryScreen.WorkingArea.Height;
               
            this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            ApartadoCorreo apartadoCorreo = new ApartadoCorreo();
            apartadoCorreo.Show();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void MenuOEE_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Seguro Que Desea Cerrar el Programa?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                e.Cancel = false;
                if(activeForm != null)
                    activeForm.Close();
            }
            else if (resultado == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
