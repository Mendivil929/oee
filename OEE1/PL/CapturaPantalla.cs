using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OEE1.PL
{
    public partial class CapturaPantalla : Form
    {
        Point posicionVentana;
        bool mover = false;
        public CapturaPantalla()
        {
            InitializeComponent();
            this.Opacity = 0.2;
            this.TransparencyKey = Color.Turquoise;
            this.ControlBox = false;
            this.Text = "";
        }

        private void CapturaPantalla_Load(object sender, EventArgs e)
        {

        }

        private void CapturaPantalla_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.C)) { CapturarPantalla(); }
            if ((e.KeyCode == Keys.M)) { this.WindowState = FormWindowState.Minimized; }
            if ((e.KeyCode == Keys.L)) { this.WindowState = FormWindowState.Maximized; }
            if ((e.KeyCode == Keys.Escape)) { this.Close(); }
        }

        public void CapturarPantalla()
        {
            try
            {
                this.Hide();
                Bitmap CapturaBitMap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
                Rectangle CapturaRectangulo = Screen.AllScreens[0].Bounds;
                Graphics CapturaGrafico = Graphics.FromImage(CapturaBitMap);
                CapturaGrafico.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, CapturaRectangulo.Size);
                Clipboard.SetImage(CapturaBitMap);
                MessageBox.Show("Región capturada");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CapturaPantalla_MouseDown(object sender, MouseEventArgs e)
        {
            posicionVentana = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mover = true;
        }

        private void CapturaPantalla_MouseUp(object sender, MouseEventArgs e)
        {
            mover = false;
        }

        private void CapturaPantalla_MouseMove(object sender, MouseEventArgs e)
        {
            if (mover)
            {
                this.Location = new Point(Cursor.Position.X - posicionVentana.X, Cursor.Position.Y - posicionVentana.Y);
            }
        }
    }
}
