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
    internal class GastoDAO: BaseDAO
    {
        internal List<FrmGasto> ObtenerTodosGastos()
        {
            List<FrmGasto> lista = new List<FrmGasto>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneTodosGastos");

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmGasto frm = new FrmGasto();
                    frm.IdGasto = (Guid)dataReader["IdGasto"];
                    frm.DescripcionGasto = (String)dataReader["DescripcionGasto"];
                    frm.MontoGasto = (int)dataReader["MontoGasto"];
                    frm.FechaInicioGasto = (DateTime)dataReader["FechaInicioGasto"];
                    frm.FechaTerminoGasto = (DateTime)dataReader["FechaTerminoGasto"];
                    frm.GastoHabilitado = (Boolean)dataReader["GastoHabilitado"];
                    frm.EsInversionGasto = (Boolean)dataReader["EsInversion"];

                    lista.Add(frm);
                }
            }
            return lista;
        }

        internal List<FrmGasto> ObtenerTodosGastosInversion()
        {
            List<FrmGasto> lista = new List<FrmGasto>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneTodosGastosInversion");

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmGasto frm = new FrmGasto();
                    frm.IdGasto = (Guid)dataReader["IdGasto"];
                    frm.DescripcionGasto = (String)dataReader["DescripcionGasto"];
                    frm.MontoGasto = (int)dataReader["MontoGasto"];
                    frm.FechaInicioGasto = (DateTime)dataReader["FechaInicioGasto"];
                    frm.FechaTerminoGasto = (DateTime)dataReader["FechaTerminoGasto"];
                    frm.GastoHabilitado = (Boolean)dataReader["GastoHabilitado"];
                    frm.EsInversionGasto = (Boolean)dataReader["EsInversion"];

                    lista.Add(frm);
                }
            }
            return lista;
        }

        internal List<FrmGasto> ObtenerGastosPorDia(DateTime DIA)
        {
            List<FrmGasto> lista = new List<FrmGasto>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneGastoPorDia");
            db.AddInParameter(oDBCommand, "FechaGasto", DbType.DateTime, DIA);

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmGasto frm = new FrmGasto();
                    frm.IdGasto = (Guid)dataReader["IdGasto"];
                    frm.DescripcionGasto = (String)dataReader["DescripcionGasto"];
                    frm.MontoGasto = (int)dataReader["MontoGasto"];
                    frm.FechaInicioGasto = (DateTime)dataReader["FechaInicioGasto"];
                    frm.FechaTerminoGasto = (DateTime)dataReader["FechaTerminoGasto"];
                    frm.GastoHabilitado = (Boolean)dataReader["GastoHabilitado"];
                    frm.EsInversionGasto = (Boolean)dataReader["EsInversion"];

                    lista.Add(frm);
                }
            }
            return lista;
        }


        internal List<FrmGasto> ObtenerGastosPorMes(DateTime MES)
        {
            List<FrmGasto> lista = new List<FrmGasto>();
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneGastoPorMes");
            db.AddInParameter(oDBCommand, "FechaGasto", DbType.DateTime, MES);

            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                while (dataReader.Read())
                {
                    FrmGasto frm = new FrmGasto();
                    frm.IdGasto = (Guid)dataReader["IdGasto"];
                    frm.DescripcionGasto = (String)dataReader["DescripcionGasto"];
                    frm.MontoGasto = (int)dataReader["MontoGasto"];
                    frm.FechaInicioGasto = (DateTime)dataReader["FechaInicioGasto"];
                    frm.FechaTerminoGasto = (DateTime)dataReader["FechaTerminoGasto"];
                    frm.GastoHabilitado = (Boolean)dataReader["GastoHabilitado"];
                    frm.EsInversionGasto = (Boolean)dataReader["EsInversion"];

                    lista.Add(frm);
                }
            }
            return lista;
        }


        internal FrmGasto ObtenerGasto(Guid IdGasto)
        {
            FrmGasto frm = null;
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDBCommand = db.GetStoredProcCommand("sp_fi_obtieneGastoPorId");
            db.AddInParameter(oDBCommand, "idGasto", DbType.Guid, IdGasto);
            using (IDataReader dataReader = db.ExecuteReader(oDBCommand))
            {
                if (dataReader.Read())
                {
                    frm = new FrmGasto();
                    frm.IdGasto = (Guid)dataReader["IdGasto"];
                    frm.DescripcionGasto = (String)dataReader["DescripcionGasto"];
                    frm.MontoGasto = (int)dataReader["MontoGasto"];
                    frm.FechaInicioGasto = (DateTime)dataReader["FechaInicioGasto"];
                    frm.FechaTerminoGasto = (DateTime)dataReader["FechaTerminoGasto"];
                    frm.GastoHabilitado = (Boolean)dataReader["GastoHabilitado"];
                    frm.EsInversionGasto = (Boolean)dataReader["EsInversion"];
                }
            }
            return frm;
        }


        internal Guid? RegistrarGasto(FrmGasto Gasto)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_creaGasto");
            db.AddInParameter(oDbCommand, "DescripcionGasto", DbType.String, Gasto.DescripcionGasto);
            db.AddInParameter(oDbCommand, "FechaInicioGasto", DbType.Date, Gasto.FechaInicioGasto);
            db.AddInParameter(oDbCommand, "FechaTerminoGasto", DbType.Date, Gasto.FechaTerminoGasto);
            db.AddInParameter(oDbCommand, "MontoGasto", DbType.Int32, Gasto.MontoGasto);
            db.AddInParameter(oDbCommand, "EsInversion", DbType.Boolean, Gasto.EsInversionGasto);
            db.AddOutParameter(oDbCommand, "NewIdGasto", DbType.Guid, 0);

            if (db.ExecuteNonQuery(oDbCommand) == 0)
                return null;
            else
                return (Guid?)db.GetParameterValue(oDbCommand, "NewIdGasto");
        }


        internal bool ModificaGasto(FrmGasto Gasto)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_modificarGasto");
            db.AddInParameter(oDbCommand, "DescripcionGasto", DbType.String, Gasto.DescripcionGasto);
            db.AddInParameter(oDbCommand, "FechaInicioGasto", DbType.Date, Gasto.FechaInicioGasto);
            db.AddInParameter(oDbCommand, "FechaTerminoGasto", DbType.Date, Gasto.FechaTerminoGasto);
            db.AddInParameter(oDbCommand, "MontoGasto", DbType.Int32, Gasto.MontoGasto);
            db.AddInParameter(oDbCommand, "IdGasto", DbType.Guid, Gasto.IdGasto);

            db.ExecuteNonQuery(oDbCommand);
            return true;
        }


        internal bool EliminaGasto(Guid IdGasto)
        {
            Database db = factory.Create(_BeneficiosProvider);
            DbCommand oDbCommand = db.GetStoredProcCommand("sp_fi_eliminarGasto");
            db.AddInParameter(oDbCommand, "IdGasto", DbType.Guid, IdGasto);

            db.ExecuteNonQuery(oDbCommand);
            return true;
        }
    }
}
