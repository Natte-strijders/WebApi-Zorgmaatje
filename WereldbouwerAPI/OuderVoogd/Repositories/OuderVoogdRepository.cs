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

        public async Task<OuderVoogd> GetOuderVoogdByIdAsync(string id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<OuderVoogd>("SELECT * FROM OuderVoogd WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<OuderVoogd>> GetAllOuderVoogdenAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<OuderVoogd>("SELECT * FROM OuderVoogd");
            }
        }

        public async Task AddOuderVoogdAsync(OuderVoogd ouderVoogd)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO OuderVoogd (Id, Voornaam, Achternaam) VALUES (@Id, @Voornaam, @Achternaam)", ouderVoogd);
            }
        }

        public async Task UpdateOuderVoogdAsync(OuderVoogd ouderVoogd)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE OuderVoogd SET Voornaam = @Voornaam, Achternaam = @Achternaam WHERE Id = @Id", ouderVoogd);
            }
        }

        public async Task DeleteOuderVoogdAsync(string id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM OuderVoogd WHERE Id = @Id", new { id });
            }
        }

    }
}
