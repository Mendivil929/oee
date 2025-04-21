using System.Data;
using OEE1.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OEE1.DAL
{
    internal class RegistroDAL
    {
        conexionDAL connection;
        public RegistroDAL()
        {
            connection = new conexionDAL();
        }

        public SqlCommand solicitarParametrosConfiguracion(RegistroBLL objectRegistroBLL)
        {
            SqlCommand comandoUniversal = new SqlCommand();
            comandoUniversal.Parameters.Add("@fecha", SqlDbType.VarChar).Value = objectRegistroBLL.Fecha;
            comandoUniversal.Parameters.Add("@mes", SqlDbType.Int).Value = objectRegistroBLL.Mes;
            comandoUniversal.Parameters.Add("@anual", SqlDbType.Int).Value = objectRegistroBLL.Anual;
            comandoUniversal.Parameters.Add("@turno", SqlDbType.VarChar).Value = objectRegistroBLL.Turno;
            comandoUniversal.Parameters.Add("@area", SqlDbType.VarChar).Value = objectRegistroBLL.Area;
            comandoUniversal.Parameters.Add("@maquina", SqlDbType.VarChar).Value = objectRegistroBLL.Maquina;
            comandoUniversal.Parameters.Add("@horaInicial", SqlDbType.VarChar).Value = objectRegistroBLL.HoraInicial;
            comandoUniversal.Parameters.Add("@horaFinal", SqlDbType.VarChar).Value = objectRegistroBLL.HoraFinal;
            comandoUniversal.Parameters.Add("@numeroParte", SqlDbType.VarChar).Value = objectRegistroBLL.NumeroParte;
            comandoUniversal.Parameters.Add("@meta", SqlDbType.Int).Value = objectRegistroBLL.Target;
            comandoUniversal.Parameters.Add("@piezasProducidas", SqlDbType.Int).Value = objectRegistroBLL.PiezasProducidas;
            comandoUniversal.Parameters.Add("@variacion", SqlDbType.Int).Value = objectRegistroBLL.Variation;
            comandoUniversal.Parameters.Add("@tiempoMuertoPlaneado", SqlDbType.Int).Value = objectRegistroBLL.TiempoMuertoPlaneado;
            //comandoUniversal.Parameters.Add("@razonTiempoMuerto", SqlDbType.VarChar).Value = objectRegistroBLL.RazonDownTime;
            comandoUniversal.Parameters.Add("@minTiempoMuerto", SqlDbType.Int).Value = objectRegistroBLL.DownTime;
            //comandoUniversal.Parameters.Add("@nombreScrap", SqlDbType.VarChar).Value = objectRegistroBLL.NombreScrap;
            comandoUniversal.Parameters.Add("@piezasScrap", SqlDbType.Int).Value = objectRegistroBLL.ScrapPieces;
            comandoUniversal.Parameters.Add("@disponibilidad", SqlDbType.Float).Value = objectRegistroBLL.Disponibilidad;
            comandoUniversal.Parameters.Add("@rendimiento", SqlDbType.Float).Value = objectRegistroBLL.Rendimiento;
            comandoUniversal.Parameters.Add("@calidad", SqlDbType.Float).Value = objectRegistroBLL.Calidad;
            comandoUniversal.Parameters.Add("@oee", SqlDbType.Float).Value = objectRegistroBLL.OEE;

            return comandoUniversal;
        }

        //SOLICITUDES DE PARAMETROS PARA RegistroScrap y RegistroTiempoMuerto
        public SqlCommand solicitarParametrosRegistroScrap(RegistroBLL objectRegistroBLL)
        {
            SqlCommand comandoUniversal= new SqlCommand();
            comandoUniversal.Parameters.Add("@fecha", SqlDbType.VarChar).Value = objectRegistroBLL.Fecha;
            comandoUniversal.Parameters.Add("@mes", SqlDbType.Int).Value = objectRegistroBLL.Mes;
            comandoUniversal.Parameters.Add("@anual", SqlDbType.Int).Value = objectRegistroBLL.Anual;
            comandoUniversal.Parameters.Add("@turno", SqlDbType.VarChar).Value = objectRegistroBLL.Turno;
            comandoUniversal.Parameters.Add("@area", SqlDbType.VarChar).Value = objectRegistroBLL.Area;
            comandoUniversal.Parameters.Add("@maquina", SqlDbType.VarChar).Value = objectRegistroBLL.Maquina;
            comandoUniversal.Parameters.Add("@nombreScrap", SqlDbType.VarChar).Value = objectRegistroBLL.NombreScrap;
            comandoUniversal.Parameters.Add("@piezasScrap", SqlDbType.Int).Value = objectRegistroBLL.ScrapPieces;
            return comandoUniversal;
        }

        public SqlCommand solicitarParametrosRegistroTiempoMuerto(RegistroBLL objectRegistroBLL)
        {
            SqlCommand comandoUniversal = new SqlCommand();
            comandoUniversal.Parameters.Add("@fecha", SqlDbType.VarChar).Value = objectRegistroBLL.Fecha;
            comandoUniversal.Parameters.Add("@mes", SqlDbType.Int).Value = objectRegistroBLL.Mes;
            comandoUniversal.Parameters.Add("@anual", SqlDbType.Int).Value = objectRegistroBLL.Anual;
            comandoUniversal.Parameters.Add("@turno", SqlDbType.VarChar).Value = objectRegistroBLL.Turno;
            comandoUniversal.Parameters.Add("@area", SqlDbType.VarChar).Value = objectRegistroBLL.Area;
            comandoUniversal.Parameters.Add("@maquina", SqlDbType.VarChar).Value = objectRegistroBLL.Maquina;
            comandoUniversal.Parameters.Add("@razonTiempoMuerto", SqlDbType.VarChar).Value = objectRegistroBLL.RazonDownTime;
            comandoUniversal.Parameters.Add("@minTiempoMuerto", SqlDbType.Int).Value = objectRegistroBLL.DownTime;
            return comandoUniversal;
        }

        #region REGISTRO PRINCIPAL -> TABLA 'NewOEEPerMachine'
        public bool Registrar(RegistroBLL objectRegistroBLL)
        {
            SqlCommand comandoPorMaquina = solicitarParametrosConfiguracion(objectRegistroBLL);
            comandoPorMaquina.CommandText = "INSERT INTO NewOEEPerMachine " +
            "(fecha, mes, anual, turno, area, machine, startTime, endTime, partNumber, target, piecesProduced, variation, " +
            "plannedDownTime, downTime, scrapPieces, Disponibilidad, Rendimiento, Calidad, OEE) VALUES (@fecha, @mes, @anual, @turno, @area, @maquina, " +
            "@horaInicial, @horaFinal, @numeroParte, @meta, @piezasProducidas, @variacion, @tiempoMuertoPlaneado, @minTiempoMuerto, @piezasScrap, " +
            "@disponibilidad, @rendimiento, @calidad, @oee)";
            return connection.EjecutarComando(comandoPorMaquina);
        }

        #endregion

        #region REGISTRO DE SCRAP Y TIEMPO MUERTO -> TABLAS 'NewRegistroScrap' Y 'NewRegistroTiempoMuerto' 

        public bool RegistrarScrap(DataGridView objectDataGridView, RegistroBLL objectRegistroBLL)
        {
            #region ALMACENANDO LA LLAVE QUE OCUPA EL SCRAP     
            DataSet ds = new DataSet();
            consultaDAL objectConsultaDAL = new consultaDAL();
            ds = objectConsultaDAL.MostrarDatosCommand("SELECT MAX(ID) as LastID FROM NewOEEPerMachine");
            var dt = ds.Tables[0];
            int lastID = int.Parse(dt.Rows[0][0].ToString());
            #endregion
            SqlCommand comandoUniversal = new SqlCommand();
            int piezasScrap = 0;
            try
            {
                for (int i = 0; i < objectDataGridView.Rows.Count; i++)
                {
                    int.TryParse(objectDataGridView.Rows[i].Cells[1].Value.ToString(), out piezasScrap);
                    objectRegistroBLL.NombreScrap = objectDataGridView.Rows[i].Cells[0].Value.ToString();
                    objectRegistroBLL.ScrapPieces = piezasScrap;
                    comandoUniversal = solicitarParametrosRegistroScrap(objectRegistroBLL);
                    comandoUniversal.CommandText = "INSERT INTO NewRegistroScrap " +
                    "(llave, fecha, mes, anual, turno, area, machine, nombreScrap, numPiezas) VALUES (" + lastID + ", @fecha, @mes, @anual, @turno, @area, @maquina, " +
                    "@nombreScrap, @piezasScrap)";
                    connection.EjecutarComando(comandoUniversal);
                }
                return true;
            }
            catch { return false; }

        }
        public bool RegistrarTiempoMuerto(DataGridView objectDataGridView, RegistroBLL objectRegistroBLL)
        {
            #region ALMACENANDO LA LLAVE QUE OCUPA EL TIEMPO MUERTO
            DataSet ds = new DataSet();
            consultaDAL objectConsultaDAL = new consultaDAL();
            ds = objectConsultaDAL.MostrarDatosCommand("SELECT MAX(ID) as LastID FROM NewOEEPerMachine");
            var dt = ds.Tables[0];
            int lastID = int.Parse(dt.Rows[0][0].ToString());
            #endregion
            SqlCommand comandoUniversal = new SqlCommand();
            int minutos = 0;
            try
            {
                for (int i = 0; i < objectDataGridView.Rows.Count; i++)
                {
                    int.TryParse(objectDataGridView.Rows[i].Cells[1].Value.ToString(), out minutos);
                    objectRegistroBLL.RazonDownTime = objectDataGridView.Rows[i].Cells[0].Value.ToString();
                    objectRegistroBLL.DownTime = minutos;
                    comandoUniversal = solicitarParametrosRegistroTiempoMuerto(objectRegistroBLL);
                    comandoUniversal.CommandText = "INSERT INTO NewRegistroTiempoMuerto " +
                    "(llave, fecha, mes, anual, turno, area, machine, razonTiempoMuerto, minutosTiempoMuerto) VALUES (" + lastID + ", @fecha, @mes, @anual, @turno, " +
                    "@area, @maquina, @razonTiempoMuerto, @minTiempoMuerto)";
                    connection.EjecutarComando(comandoUniversal);
                }
                return true;
            }
            catch { return false; }

           
        }

        #endregion

        #region FUNCION PARA BORRAR REGISTROS -> SE USAN LAS 3 TABLAS
        public bool EliminarRegistros(int index)
        {
            SqlCommand comando1 = new SqlCommand("DELETE FROM NewOEEPerMachine WHERE ID=" + index + "");
            SqlCommand comando2 = new SqlCommand("DELETE FROM NewRegistroScrap WHERE llave=" + index + "");
            SqlCommand comando3 = new SqlCommand("DELETE FROM NewRegistroTiempoMuerto WHERE llave=" + index + "");
            try
            {
                connection.EjecutarComando(comando1);
                connection.EjecutarComando(comando2);
                connection.EjecutarComando(comando3);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}