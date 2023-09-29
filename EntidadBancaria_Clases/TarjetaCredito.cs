using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadBancaria_Clases
{
    public class TarjetaCredito
    {
        public int id { get; set; } 

        public string numeroTarjeta { get; set; } 

        public double limiteCredito { get; set; }   
        public double saldoDisponible { get; set; } 
        public string estado { get; set; }  

    }
}
