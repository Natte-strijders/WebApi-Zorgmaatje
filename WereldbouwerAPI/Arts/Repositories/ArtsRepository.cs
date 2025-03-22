using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi.Arts.Repositories
{
    public class ArtsRepository : IArtsRepository
    {
        private readonly string sqlConnectionString;

        public ArtsRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Arts> GetArtsByNaamAsync(string naam)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Arts>("SELECT * FROM Arts WHERE Naam = @Naam", new { naam });
            }
        }

        public async Task<IEnumerable<Arts>> GetAllArtsAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Arts>("SELECT * FROM Arts");
            }
        }

        public async Task AddArtsAsync(Arts arts)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var existingArts = await sqlConnection.QuerySingleOrDefaultAsync<Arts>("SELECT * FROM Arts WHERE Naam = @Naam", new { arts.naam });
                if (existingArts != null)
                {
                    throw new Exception($"Arts with Naam '{arts.naam}' already exists.");
                }

                arts.id = Guid.NewGuid().ToString(); // Genereer een unieke ID
                await sqlConnection.ExecuteAsync("INSERT INTO Arts (Id, Naam, Specialisatie) VALUES (@Id, @Naam, @Specialisatie)", arts);
            }
        }

        public async Task UpdateArtsAsync(Arts arts)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var existingArts = await sqlConnection.QuerySingleOrDefaultAsync<Arts>("SELECT * FROM Arts WHERE Id = @Id", new { arts.id });
                if (existingArts == null)
                {
                    throw new Exception($"Arts with Id '{arts.id}' does not exist.");
                }

                await sqlConnection.ExecuteAsync("UPDATE Arts SET Naam = @Naam, Specialisatie = @Specialisatie WHERE Id = @Id", arts);
            }
        }

        public async Task DeleteArtsAsync(string naam)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM Arts WHERE Naam = @Naam", new { naam });
            }
        }
    }
}
