using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINegocio.FrmNegocio
{
    public class FrmGasto
    {
        public Guid IdGasto { get; set; }
        public string DescripcionGasto { get; set; }
        public Nullable<DateTime> FechaInicioGasto { get; set; }
        public Nullable<DateTime> FechaTerminoGasto { get; set; }
        public int MontoGasto { get; set; }
        public bool GastoHabilitado { get; set; }
        public bool EsInversionGasto { get; set; }

        public int TotalGastoDia { get; set; }
        public int TotalGastoMes { get; set; }

        public FrmGasto()
        {
            TotalGastoDia = 0;
            TotalGastoMes = 0;
        }
    }
}
