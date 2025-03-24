using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ZorgmaatjeWebApi.ZorgMoment.Repositories
{
    public class ZorgMomentRepository : IZorgMomentRepository
    {
        private readonly string sqlConnectionString;

        public ZorgMomentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<ZorgMoment> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<ZorgMoment>(
                    "SELECT * FROM ZorgMoment WHERE Id = @Id",
                    new { Id = id });
            }
        }

        public async Task<IEnumerable<ZorgMoment>> GetAllAsync()
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                return await connection.QueryAsync<ZorgMoment>("SELECT * FROM ZorgMoment");
            }
        }

        public async Task AddAsync(ZorgMoment zorgMoment)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.ExecuteAsync(
                    "INSERT INTO ZorgMoment (Naam, DatumTijd, PatientId) VALUES (@Naam, @DatumTijd, @PatientId)",
                    zorgMoment);
            }
        }

        public async Task UpdateAsync(ZorgMoment zorgMoment)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.ExecuteAsync(
                    "UPDATE ZorgMoment SET Naam = @Naam, DatumTijd = @DatumTijd, PatientId = @PatientId WHERE Id = @Id",
                    zorgMoment);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.ExecuteAsync(
                    "DELETE FROM ZorgMoment WHERE Id = @Id",
                    new { Id = id });
            }
        }
    }
}