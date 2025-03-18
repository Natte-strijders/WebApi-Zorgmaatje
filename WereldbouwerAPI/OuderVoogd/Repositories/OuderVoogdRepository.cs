using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi.OuderVoogd.Repositories
{
    public class OuderVoogdRepository : IOuderVoogdRepository
    {
        private readonly string sqlConnectionString;

        public OuderVoogdRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

       
    }
}
