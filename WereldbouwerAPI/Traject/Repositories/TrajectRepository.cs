using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<Traject> GetTrajectByNaamAsync(string naam)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Traject>("SELECT * FROM Traject WHERE Naam = @Naam", new { naam });
            }
        }

        public async Task<IEnumerable<Traject>> GetAllTrajectsAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Traject>("SELECT * FROM Traject");
            }
        }

        public async Task AddTrajectAsync(Traject traject)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var existingTraject = await sqlConnection.QuerySingleOrDefaultAsync<Traject>("SELECT * FROM Traject WHERE Naam = @Naam", new { traject.naam });
                if (existingTraject != null)
                {
                    throw new Exception($"Traject with Naam '{traject.naam}' already exists.");
                }
                await sqlConnection.ExecuteAsync("INSERT INTO Traject (Naam) VALUES (@Naam)", traject);
            }
        }

        public async Task UpdateTrajectAsync(Traject traject)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE Traject SET Naam = @Naam WHERE Id = @Id", traject);
            }
        }

        public async Task DeleteTrajectAsync(string naam)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM Traject WHERE Naam = @Naam", new { naam });
            }
        }

    }
}
