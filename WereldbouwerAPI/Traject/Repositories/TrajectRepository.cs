using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi.Traject.Repositories
{
    public class TrajectRepository : ITrajectRepository
    {
        private readonly string sqlConnectionString;

        public TrajectRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

       
    }
}
