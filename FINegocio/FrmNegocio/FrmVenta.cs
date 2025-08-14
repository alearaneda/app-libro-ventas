using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINegocio.FrmNegocio
{
    public class FrmVenta
    {
        public Guid IdVenta { get; set; }
        public string DescripcionVenta { get; set; }
        public Nullable<DateTime> FechaInicioVenta { get; set; }
        public Nullable<DateTime> FechaTerminoVenta { get; set; }
        public int MontoVenta { get; set; }
        public bool VentaConBoleta { get; set; }
        public Nullable<int> NumeroBoletaInicio { get; set; }
        public Nullable<int>NumeroBoletaTermino { get; set; }
        public bool VentaHabilitada { get; set; }
        
        public int TotalDiaConBoleta { get; set; }
        public int TotalDiaSinBoleta { get; set; }
        public int TotalDia { get; set; }
        public int TotalMesConBoleta { get; set; }
        public int TotalMesSinBoleta { get; set; }
        public int TotalMes { get; set; }
        public string RangoBoletas { get; set; }

        public FrmVenta()
        {
            TotalDiaConBoleta = 0;
            TotalDiaSinBoleta = 0;
            TotalDia = 0;
            TotalMesConBoleta = 0;
            TotalMesSinBoleta = 0;
            TotalMes = 0;
        }
    }
}
