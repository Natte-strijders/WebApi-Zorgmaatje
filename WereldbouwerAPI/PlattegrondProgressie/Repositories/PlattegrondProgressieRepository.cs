//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Dapper;
//using Microsoft.Data.SqlClient;
//using ZorgmaatjeWebApi.PlattegrondProgressie.Repositories;

//namespace ZorgmaatjeWebApi.PlattegrondProgressie.Repositories
//{
//    public class PlattegrondProgressieRepository : IPlattegrondProgressieRepository
//    {
//        private readonly string sqlConnectionString;

//        public PlattegrondProgressieRepository(string sqlConnectionString)
//        {
//            this.sqlConnectionString = sqlConnectionString;
//        }

//        public async Task<PlattegrondProgressie> GetPlattegrondProgressieByIdAsync(int id)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                return await sqlConnection.QuerySingleOrDefaultAsync<PlattegrondProgressie>("SELECT * FROM PlattegrondProgressie WHERE ID = @ID", new { ID = id });
//            }
//        }

//        public async Task AddPlattegrondProgressieAsync(PlattegrondProgressie plattegrondProgressie)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                await sqlConnection.ExecuteAsync("INSERT INTO PlattegrondProgressie (PatientID, Stap) VALUES (@PatientID, @Stap)", plattegrondProgressie);
//            }
//        }

//        public async Task UpdatePlattegrondProgressieAsync(PlattegrondProgressie plattegrondProgressie)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                await sqlConnection.ExecuteAsync("UPDATE PlattegrondProgressie SET PatientID = @PatientID, Stap = @Stap WHERE ID = @ID", plattegrondProgressie);
//            }
//        }
//    }
//}