using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINegocio.DAO
{
    abstract class BaseDAO
    {
        internal DatabaseProviderFactory factory = new DatabaseProviderFactory();

        internal const string _BeneficiosProvider = "csFI";
    }
}
