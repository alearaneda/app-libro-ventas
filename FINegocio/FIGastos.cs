using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINegocio.DAO;
using FINegocio.FrmNegocio;

namespace FINegocio
{
    public class FIGastos
    {
        private GastoDAO _gDAO = new GastoDAO();

        public List<FrmGasto> ListaTodosGastos()
        {
            return _gDAO.ObtenerTodosGastos();
        }

        public List<FrmGasto> ListaTodosGastosInversion()
        {
            return _gDAO.ObtenerTodosGastosInversion();
        }

        public FrmGasto ObtieneGasto(Guid IdGasto)
        {
            return _gDAO.ObtenerGasto(IdGasto);
        }

        public Guid? RegistrarGasto(FrmGasto Gasto)
        {
            return _gDAO.RegistrarGasto(Gasto);
        }

        public bool ModificarGasto(FrmGasto Gasto)
        {
            return _gDAO.ModificaGasto(Gasto);
        }

        public bool EliminarGasto(Guid IdGasto)
        {
            return _gDAO.EliminaGasto(IdGasto);
        }

        public List<FrmGasto> ListaGastosPorDia(DateTime Dia)
        {
            return _gDAO.ObtenerGastosPorDia(Dia);
        }

        public List<FrmGasto> ListaGastosPorMes(DateTime Mes)
        {
            return _gDAO.ObtenerGastosPorMes(Mes);
        }
    }
}
