using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using FINegocio.FrmNegocio;

namespace FINegocio.DAO
{
    internal class CompraDAO: BaseDAO
    {
        internal List<FrmCompra> ObtenerTodasCompras()
        {
            List<FrmCompra> lista = new List<FrmCompra>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneTodasCompras");

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmCompra frm = new FrmCompra();
                    frm.IdCompra = (Guid)dataReader["IdCompra"];
                    frm.DescripcionCompra = (String)dataReader["DescripcionCompra"];
                    frm.MontoCompra = (int)dataReader["MontoCompra"];
                    frm.FechaInicioCompra = (DateTime)dataReader["FechaInicioCompra"];
                    frm.FechaTerminoCompra = !String.IsNullOrEmpty(dataReader["FechaTerminoCompra"].ToString()) ? (DateTime?)dataReader["FechaTerminoCompra"] : null;
                    frm.CompraFacturaInicio = (int)dataReader["CompraFacturaInicio"];
                    frm.CompraFacturaTermino = (int)dataReader["CompraFacturaTermino"];
                    frm.CompraConFactura = (Boolean)dataReader["CompraConFactura"];
                    frm.CompraRutFactura = (String)dataReader["CompraRutFactura"];
                    frm.EsInversion = (Boolean)dataReader["EsInversion"];
                    frm.CompraHabilitada = (Boolean)dataReader["CompraHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }

        internal List<FrmCompra> ObtenerTodasComprasInversion()
        {
            List<FrmCompra> lista = new List<FrmCompra>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneTodasComprasInversion");

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmCompra frm = new FrmCompra();
                    frm.IdCompra = (Guid)dataReader["IdCompra"];
                    frm.DescripcionCompra = (String)dataReader["DescripcionCompra"];
                    frm.MontoCompra = (int)dataReader["MontoCompra"];
                    frm.FechaInicioCompra = (DateTime)dataReader["FechaInicioCompra"];
                    frm.FechaTerminoCompra = !String.IsNullOrEmpty(dataReader["FechaTerminoCompra"].ToString()) ? (DateTime?)dataReader["FechaTerminoCompra"] : null;
                    frm.CompraFacturaInicio = (int)dataReader["CompraFacturaInicio"];
                    frm.CompraFacturaTermino = (int)dataReader["CompraFacturaTermino"];
                    frm.CompraConFactura = (Boolean)dataReader["CompraConFactura"];
                    frm.CompraRutFactura = (String)dataReader["CompraRutFactura"];
                    frm.EsInversion = (Boolean)dataReader["EsInversion"];
                    frm.CompraHabilitada = (Boolean)dataReader["CompraHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }

        internal List<FrmCompra> ObtenerComprasPorDia(DateTime DIA)
        {
            List<FrmCompra> lista = new List<FrmCompra>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneCompraPorDia");
            db.AddInParameter(oDBCommand, "FechaCompra", DbType.DateTime, DIA);

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmCompra frm = new FrmCompra();
                    frm.IdCompra = (Guid)dataReader["IdCompra"];
                    frm.DescripcionCompra = (String)dataReader["DescripcionCompra"];
                    frm.MontoCompra = (int)dataReader["MontoCompra"];
                    frm.FechaInicioCompra = (DateTime)dataReader["FechaInicioCompra"];
                    frm.FechaTerminoCompra = !String.IsNullOrEmpty(dataReader["FechaTerminoCompra"].ToString()) ? (DateTime?)dataReader["FechaTerminoCompra"] : null;
                    frm.CompraFacturaInicio = (int)dataReader["CompraFacturaInicio"];
                    frm.CompraFacturaTermino = (int)dataReader["CompraFacturaTermino"];
                    frm.CompraConFactura = (Boolean)dataReader["CompraConFactura"];
                    frm.CompraRutFactura = (String)dataReader["CompraRutFactura"];
                    frm.EsInversion = (Boolean)dataReader["EsInversion"];
                    frm.CompraHabilitada = (Boolean)dataReader["CompraHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }


        internal List<FrmCompra> ObtenerComprasPorMes(DateTime MES)
        {
            List<FrmCompra> lista = new List<FrmCompra>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneCompraPorMes");
            db.AddInParameter(oDBCommand, "FechaCompra", DbType.DateTime, MES);

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmCompra frm = new FrmCompra();
                    frm.IdCompra = (Guid)dataReader["IdCompra"];
                    frm.DescripcionCompra = (String)dataReader["DescripcionCompra"];
                    frm.MontoCompra = (int)dataReader["MontoCompra"];
                    frm.FechaInicioCompra = (DateTime)dataReader["FechaInicioCompra"];
                    frm.FechaTerminoCompra = !String.IsNullOrEmpty(dataReader["FechaTerminoCompra"].ToString()) ? (DateTime?)dataReader["FechaTerminoCompra"] : null;
                    frm.CompraFacturaInicio = (int)dataReader["CompraFacturaInicio"];
                    frm.CompraFacturaTermino = (int)dataReader["CompraFacturaTermino"];
                    frm.CompraConFactura = (Boolean)dataReader["CompraConFactura"];
                    frm.CompraRutFactura = (String)dataReader["CompraRutFactura"];
                    frm.EsInversion = (Boolean)dataReader["EsInversion"];
                    frm.CompraHabilitada = (Boolean)dataReader["CompraHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }


        internal FrmCompra ObtenerCompra(Guid IdCompra)
        {
            FrmCompra frm = null;
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneCompraPorId");
            db.AddInParameter(oDBCommand, "idCompra", DbType.Guid, IdCompra);
            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                if (dataReader.Read())
                {
                    frm = new FrmCompra();
                    frm.IdCompra = (Guid)dataReader["IdCompra"];
                    frm.DescripcionCompra = (String)dataReader["DescripcionCompra"];
                    frm.MontoCompra = (int)dataReader["MontoCompra"];
                    frm.FechaInicioCompra = (DateTime)dataReader["FechaInicioCompra"];
                    frm.FechaTerminoCompra = !String.IsNullOrEmpty(dataReader["FechaTerminoCompra"].ToString()) ? (DateTime?)dataReader["FechaTerminoCompra"] : null;
                    frm.CompraFacturaInicio = (int)dataReader["CompraFacturaInicio"];
                    frm.CompraFacturaTermino = (int)dataReader["CompraFacturaTermino"];
                    frm.CompraConFactura = (Boolean)dataReader["CompraConFactura"];
                    frm.CompraRutFactura = (String)dataReader["CompraRutFactura"];
                    frm.EsInversion = (Boolean)dataReader["EsInversion"];
                    frm.CompraHabilitada = (Boolean)dataReader["CompraHabilitada"];
                }
            }
            return frm;
        }


        internal Guid? RegistrarCompra(FrmCompra Compra)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_creaCompra");
            db.AddInParameter(oDbCommand, "DescripcionCompra", DbType.String, Compra.DescripcionCompra);
            db.AddInParameter(oDbCommand, "FechaInicioCompra", DbType.Date, Compra.FechaInicioCompra);
            db.AddInParameter(oDbCommand, "FechaTerminoCompra", DbType.Date, Compra.FechaTerminoCompra);
            db.AddInParameter(oDbCommand, "MontoCompra", DbType.Int32, Compra.MontoCompra);
            db.AddInParameter(oDbCommand, "CompraConFactura", DbType.Boolean, Compra.CompraConFactura);
            db.AddInParameter(oDbCommand, "CompraFacturaInicio", DbType.Int32, Compra.CompraFacturaInicio);
            db.AddInParameter(oDbCommand, "CompraFacturaTermino", DbType.Int32, Compra.CompraFacturaTermino);
            db.AddInParameter(oDbCommand, "CompraRutFactura", DbType.String, Compra.CompraRutFactura);
            db.AddInParameter(oDbCommand, "EsInversion", DbType.Boolean, Compra.EsInversion);
            db.AddOutParameter(oDbCommand, "NewIdCompra", DbType.Guid, 0);

            if (db.ExecuteNonQuery(oDbCommand) == 0)
                return null;
            else
                return (Guid?)db.GetParameterValue(oDbCommand, "NewIdCompra");
        }


        internal bool ModificaCompra(FrmCompra Compra)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_modificarCompra");
            db.AddInParameter(oDbCommand, "DescripcionCompra", DbType.String, Compra.DescripcionCompra);
            db.AddInParameter(oDbCommand, "FechaInicioCompra", DbType.Date, Compra.FechaInicioCompra);
            db.AddInParameter(oDbCommand, "FechaTerminoCompra", DbType.Date, Compra.FechaTerminoCompra);
            db.AddInParameter(oDbCommand, "MontoCompra", DbType.Int32, Compra.MontoCompra);
            db.AddInParameter(oDbCommand, "CompraConFactura", DbType.Boolean, Compra.CompraConFactura);
            db.AddInParameter(oDbCommand, "CompraFacturaInicio", DbType.Int32, Compra.CompraFacturaInicio);
            db.AddInParameter(oDbCommand, "CompraFacturaTermino", DbType.Int32, Compra.CompraFacturaTermino);
            db.AddInParameter(oDbCommand, "CompraRutFactura", DbType.String, Compra.CompraRutFactura);
            db.AddInParameter(oDbCommand, "IdCompra", DbType.Guid, Compra.IdCompra);

            db.ExecuteNonQuery(oDbCommand);
            return true;
        }


        internal bool EliminaCompra(Guid IdCompra)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_eliminarCompra");
            db.AddInParameter(oDbCommand, "IdCompra", DbType.Guid, IdCompra);

            db.ExecuteNonQuery(oDbCommand);
            return true;
        }
    }
}
