using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Data.SqlClient;
using OEE1.DAL;
using OEE1.PL;

namespace OEE1
{
    public partial class WindowsLogin : Form
    {
        conexionDAL objectConecction;
        public WindowsLogin()
        {
            objectConecction = new conexionDAL();
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection comando = new SqlConnection();
            comando = objectConecction.EstablecerConexion();
            comando.Open();
            string nameUser = user.Text;
            string passw = pass.Text;
            string cadena = "select users, passwords from Logeo where users='"+ nameUser +"' and passwords='"+ passw + "'";
            SqlCommand query = new SqlCommand(cadena, comando);
            SqlDataReader registro = query.ExecuteReader();
            if(registro.Read())
            {
                MessageBox.Show("Bievenido a Hanon Apps!!");
                openMenuOEE();
            }
            else
            {
                MessageBox.Show("Acceso denegado");
            }
        }

        private void openMenuOEE()
        {
            MenuOEE menuOEE = new MenuOEE();
            menuOEE.Show();
            this.Close();
        }

    }
}