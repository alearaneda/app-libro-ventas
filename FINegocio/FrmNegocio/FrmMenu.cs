using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINegocio.FrmNegocio
{
    public class FrmMenu
    {
        public int TotalVentas { get; set; }
        public int TotalGanancia { get; set; }
        public int TotalGastos { get; set; }
        public int TotalCompras { get; set; }
        public int SumaGastos { get; set; }

        public FrmMenu()
        {
            TotalVentas = 0;
            TotalGanancia = 0;
            TotalGastos = 0;
            TotalCompras = 0;
            SumaGastos = 0;
        }
    }
}
