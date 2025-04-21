using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEE1.DAL
{
    internal class conexionDAL
    {
        private string cadenaConexion = "server=PDNPD014; database=OEEDB; integrated security=false; user=AdminOEE; password=AdminOEE"; //Local Connection
        SqlConnection Conexion;

        public SqlConnection EstablecerConexion()
        {
            this.Conexion = new SqlConnection(this.cadenaConexion);
            return this.Conexion;
        }

        /*Método para ejecutar comandos*/
        public bool EjecutarComando(SqlCommand SQLcomando)
        {
            try
            {
                SqlCommand Comando = SQLcomando;
                Comando.Connection = EstablecerConexion();
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DataSet Consulta(SqlCommand SQLcomando)
        {
            DataSet DS = new DataSet();
            SqlDataAdapter Adaptador = new SqlDataAdapter();
            try
            {
                SqlCommand Comando = new SqlCommand();
                Comando = SQLcomando;
                Comando.Connection = EstablecerConexion();
                Adaptador.SelectCommand = Comando;
                Conexion.Open();
                Adaptador.Fill(DS);
                Conexion.Close();
                return DS;

            }
            catch
            {
                return DS;
            }
        }
    }
}
