using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi.TrajectZorgMoment.Repositories
{
    public class TrajectZorgMomentRepository : ITrajectZorgMomentRepository
    {
        private readonly string sqlConnectionString;

        public TrajectZorgMomentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<TrajectZorgMoment> GetByIdAsync(TrajectZorgMomentKey key)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<TrajectZorgMoment>(
                    "SELECT * FROM Traject_ZorgMoment WHERE TrajectId = @TrajectId AND ZorgMomentId = @ZorgMomentId",
                    key);
            }
        }

        public async Task<IEnumerable<TrajectZorgMoment>> GetAllAsync()
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                return await connection.QueryAsync<TrajectZorgMoment>("SELECT * FROM Traject_ZorgMoment");
            }
        }

        public async Task AddAsync(TrajectZorgMoment trajectZorgMoment)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Traject_ZorgMoment (TrajectId, ZorgMomentId, Volgorde) VALUES (@TrajectId, @ZorgMomentId, @Volgorde)",
                    trajectZorgMoment);
            }
        }

        public async Task UpdateAsync(TrajectZorgMoment trajectZorgMoment)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.ExecuteAsync(
                    "UPDATE Traject_ZorgMoment SET Volgorde = @Volgorde WHERE TrajectId = @TrajectId AND ZorgMomentId = @ZorgMomentId",
                    trajectZorgMoment);
            }
        }

        public async Task DeleteAsync(TrajectZorgMomentKey key)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.ExecuteAsync(
                    "DELETE FROM Traject_ZorgMoment WHERE TrajectId = @TrajectId AND ZorgMomentId = @ZorgMomentId",
                    key);
            }
        }
    }
}

