using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINegocio.DAO;
using FINegocio.FrmNegocio;

namespace FINegocio
{
    public class FIVentas
    {
        private VentaDAO _vDAO = new VentaDAO();

        public List<FrmVenta> ListaTodasVentas()
        {
            return _vDAO.ObtenerTodasVentas();
        }

        public FrmVenta ObtieneVenta(Guid IdVenta)
        {
            return _vDAO.ObtenerVenta(IdVenta);
        }

        public Guid? RegistrarVenta(FrmVenta Venta)
        {
            return _vDAO.RegistrarVenta(Venta);
        }

        public bool ModificarVenta(FrmVenta Venta)
        {
            return _vDAO.ModificaVenta(Venta);
        }

        public bool EliminarVenta(Guid IdVenta)
        {
            return _vDAO.EliminaVenta(IdVenta);
        }

        public List<FrmVenta> ListaVentasPorDia(DateTime Dia)
        {
            return _vDAO.ObtenerVentasPorDia(Dia);
        }

        public List<FrmVenta> ListaVentasPorMes(DateTime Mes)
        {
            return _vDAO.ObtenerVentasPorMes(Mes);
        }
    }
}
