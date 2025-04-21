using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEE1.BLL
{
    internal class RegistroBLL
    {
        public string Fecha { get; set; }
        public int Mes {  get; set; }
        public int Anual { get; set; }
        public string Turno { get; set; }
        public string Area { get; set; }
        public string Maquina { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string NumeroParte { get; set; }
        public float Target {  get; set; }
        public int PiezasProducidas { get; set; }
        public float Variation {  get; set; }
        public string RazonDownTime { get; set; }
        public int DownTime { get; set; }
        public string NombreScrap { get; set; }
        public int ScrapPieces { get; set; }
        public int TiempoMuertoPlaneado { get; set; }

        /*Atributos de calculo*/
        public float Disponibilidad { get; set; }
        public float Rendimiento { get; set; }
        public float Calidad { get; set; }
        public float OEE { get; set; }

    }
}
