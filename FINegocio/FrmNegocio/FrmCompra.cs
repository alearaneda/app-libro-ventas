using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINegocio.FrmNegocio
{
    public class FrmCompra
    {
        public Guid IdCompra { get; set; }
        public string DescripcionCompra { get; set; }
        public Nullable<DateTime> FechaInicioCompra { get; set; }
        public Nullable<DateTime> FechaTerminoCompra { get; set; }
        public int MontoCompra { get; set; }
        public bool CompraConFactura { get; set; }
        public Nullable<int> CompraFacturaInicio { get; set; }
        public Nullable<int> CompraFacturaTermino { get; set; }
        public string CompraRutFactura { get; set; } 
        public bool EsInversion { get; set; }
        public bool CompraHabilitada { get; set; }

        public int TotalDiaConFactura { get; set; }
        public int TotalDiaSinFactura { get; set; }
        public int TotalCompraDia { get; set; }
        public int TotalMesConFactura { get; set; }
        public int TotalMesSinFactura { get; set; }
        public int TotalCompraMes { get; set; }
        public string RangoFacturas { get; set; }

        public FrmCompra()
        {
            TotalDiaConFactura = 0;
            TotalDiaSinFactura = 0;
            TotalCompraDia = 0;
            TotalMesConFactura = 0;
            TotalMesSinFactura = 0;
            TotalCompraMes = 0;
        }
    }
}
