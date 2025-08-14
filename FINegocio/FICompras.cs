using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINegocio.DAO;
using FINegocio.FrmNegocio;

namespace FINegocio
{
    public class FICompras
    {
        private CompraDAO _cDAO = new CompraDAO();

        public List<FrmCompra> ListaTodasCompras()
        {
            return _cDAO.ObtenerTodasCompras();
        }

        public List<FrmCompra> ListaTodasComprasInversion()
        {
            return _cDAO.ObtenerTodasComprasInversion();
        }

        public FrmCompra ObtieneCompra(Guid IdCompra)
        {
            return _cDAO.ObtenerCompra(IdCompra);
        }

        public Guid? RegistrarCompra(FrmCompra Compra)
        {
            return _cDAO.RegistrarCompra(Compra);
        }

        public bool ModificarCompra(FrmCompra Compra)
        {
            return _cDAO.ModificaCompra(Compra);
        }

        public bool EliminarCompra(Guid IdCompra)
        {
            return _cDAO.EliminaCompra(IdCompra);
        }

        public List<FrmCompra> ListaComprasPorDia(DateTime Dia)
        {
            return _cDAO.ObtenerComprasPorDia(Dia);
        }

        public List<FrmCompra> ListaComprasPorMes(DateTime Mes)
        {
            return _cDAO.ObtenerComprasPorMes(Mes);
        }
    }
}
