using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi
{
    public class ZorgmaatjeRepository : IZorgmaatjeRepository
    {
        private readonly string sqlConnectionString;

        public ZorgmaatjeRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }


    }
}
