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
    internal class VentaDAO : BaseDAO
    {
        internal List<FrmVenta> ObtenerTodasVentas()
        {
            List<FrmVenta> lista = new List<FrmVenta>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneTodoVenta");

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmVenta frm = new FrmVenta();
                    frm.IdVenta = (Guid)dataReader["IdVenta"];
                    frm.DescripcionVenta = (String)dataReader["DescripcionVenta"];
                    frm.MontoVenta = (int)dataReader["MontoVenta"];
                    frm.FechaInicioVenta = (DateTime)dataReader["FechaInicioVenta"];
                    frm.FechaTerminoVenta = (DateTime)dataReader["FechaTerminoVenta"];
                    frm.NumeroBoletaInicio = (int)dataReader["VentaBoletaInicio"];
                    frm.NumeroBoletaTermino = (int)dataReader["VentaBoletaTermino"];
                    frm.VentaConBoleta = (Boolean)dataReader["VentaConBoleta"];
                    frm.VentaHabilitada = (Boolean)dataReader["VentaHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }

        internal List<FrmVenta> ObtenerVentasPorDia(DateTime DIA)
        {
            List<FrmVenta> lista = new List<FrmVenta>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneVentaPorDia");
            db.AddInParameter(oDBCommand, "FechaVenta", DbType.DateTime, DIA);

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmVenta frm = new FrmVenta();
                    frm.IdVenta = (Guid)dataReader["IdVenta"];
                    frm.DescripcionVenta = (String)dataReader["DescripcionVenta"];
                    frm.MontoVenta = (int)dataReader["MontoVenta"];
                    frm.FechaInicioVenta = (DateTime)dataReader["FechaInicioVenta"];
                    frm.FechaTerminoVenta = (DateTime)dataReader["FechaTerminoVenta"];
                    frm.NumeroBoletaInicio = (int)dataReader["VentaBoletaInicio"];
                    frm.NumeroBoletaTermino = (int)dataReader["VentaBoletaTermino"];
                    frm.VentaConBoleta = (Boolean)dataReader["VentaConBoleta"];
                    frm.VentaHabilitada = (Boolean)dataReader["VentaHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }


        internal List<FrmVenta> ObtenerVentasPorMes(DateTime MES)
        {
            List<FrmVenta> lista = new List<FrmVenta>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneVentaPorMes");
            db.AddInParameter(oDBCommand, "FechaVenta", DbType.DateTime, MES);

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmVenta frm = new FrmVenta();
                    frm.IdVenta = (Guid)dataReader["IdVenta"];
                    frm.DescripcionVenta = (String)dataReader["DescripcionVenta"];
                    frm.MontoVenta = (int)dataReader["MontoVenta"];
                    frm.FechaInicioVenta = (DateTime)dataReader["FechaInicioVenta"];
                    frm.FechaTerminoVenta = (DateTime)dataReader["FechaTerminoVenta"];
                    frm.NumeroBoletaInicio = (int)dataReader["VentaBoletaInicio"];
                    frm.NumeroBoletaTermino = (int)dataReader["VentaBoletaTermino"];
                    frm.VentaConBoleta = (Boolean)dataReader["VentaConBoleta"];
                    frm.VentaHabilitada = (Boolean)dataReader["VentaHabilitada"];

                    lista.Add(frm);
                }
            }
            return lista;
        }


        internal FrmVenta ObtenerVenta(Guid IdVenta)
        {
            FrmVenta frm = null;
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneVentaPorId");
            db.AddInParameter(oDBCommand, "idVenta", DbType.Guid, IdVenta);
            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                if (dataReader.Read())
                {
                    frm = new FrmVenta();
                    frm.IdVenta = (Guid)dataReader["IdVenta"];
                    frm.DescripcionVenta = (String)dataReader["DescripcionVenta"];
                    frm.MontoVenta = (int)dataReader["MontoVenta"];
                    frm.FechaInicioVenta = (DateTime)dataReader["FechaInicioVenta"];
                    frm.FechaTerminoVenta = (DateTime)dataReader["FechaTerminoVenta"];
                    frm.NumeroBoletaInicio = (int)dataReader["VentaBoletaInicio"];
                    frm.NumeroBoletaTermino = (int)dataReader["VentaBoletaTermino"];
                    frm.VentaConBoleta = (Boolean)dataReader["VentaConBoleta"];
                    frm.VentaHabilitada = (Boolean)dataReader["VentaHabilitada"];
                }
            }
            return frm;
        }


        internal Guid? RegistrarVenta(FrmVenta Venta)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_creaVenta");
            db.AddInParameter(oDbCommand, "DescripcionVenta", DbType.String, Venta.DescripcionVenta);
            db.AddInParameter(oDbCommand, "FechaInicioVenta", DbType.Date, Venta.FechaInicioVenta);
            db.AddInParameter(oDbCommand, "FechaTerminoVenta", DbType.Date, Venta.FechaTerminoVenta);
            db.AddInParameter(oDbCommand, "MontoVenta", DbType.Int32, Venta.MontoVenta);
            db.AddInParameter(oDbCommand, "VentaConBoleta", DbType.Boolean, Venta.VentaConBoleta);
            db.AddInParameter(oDbCommand, "VentaBoletaInicio", DbType.Int32, Venta.NumeroBoletaInicio);
            db.AddInParameter(oDbCommand, "VentaBoletaTermino", DbType.Int32, Venta.NumeroBoletaTermino);
            db.AddOutParameter(oDbCommand, "NewIdVenta", DbType.Guid, 0);

            if (db.ExecuteNonQuery(oDbCommand) == 0)
                return null;
            else
                return (Guid?)db.GetParameterValue(oDbCommand, "NewIdVenta");
        }


        internal bool ModificaVenta(FrmVenta Venta)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_modificarVenta");
            db.AddInParameter(oDbCommand, "DescripcionVenta", DbType.String, Venta.DescripcionVenta);
            db.AddInParameter(oDbCommand, "FechaInicioVenta", DbType.Date, Venta.FechaInicioVenta);
            db.AddInParameter(oDbCommand, "FechaTerminoVenta", DbType.Date, Venta.FechaTerminoVenta);
            db.AddInParameter(oDbCommand, "MontoVenta", DbType.Int32, Venta.MontoVenta);
            db.AddInParameter(oDbCommand, "VentaConBoleta", DbType.Boolean, Venta.VentaConBoleta);
            db.AddInParameter(oDbCommand, "VentaBoletaInicio", DbType.Int32, Venta.NumeroBoletaInicio);
            db.AddInParameter(oDbCommand, "VentaBoletaTermino", DbType.Int32, Venta.NumeroBoletaTermino);
            db.AddInParameter(oDbCommand, "IdVenta", DbType.Guid, Venta.IdVenta);

            db.ExecuteNonQuery(oDbCommand);
            return true;
        }


        internal bool EliminaVenta(Guid Idventa)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_eliminarVenta");
            db.AddInParameter(oDbCommand, "IdVenta", DbType.Guid, Idventa);

            db.ExecuteNonQuery(oDbCommand);
            return true;
        }
    }
}
