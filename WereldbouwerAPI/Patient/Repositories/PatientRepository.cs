using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi.Patient.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string sqlConnectionString;

        public PatientRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Patient> GetPatientByIdAsync(string id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Patient>("SELECT * FROM Patient WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Patient>("SELECT * FROM Patient");
            }
        }

        public async Task AddPatientAsync(Patient patient)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var existingPatient = await sqlConnection.QuerySingleOrDefaultAsync<Patient>("SELECT * FROM Patient WHERE Id = @Id", new { patient.id });
                if (existingPatient != null)
                {
                    throw new Exception($"Patient with Id {patient.id} already exists.");
                }
                await sqlConnection.ExecuteAsync("INSERT INTO Patient (ID, Voornaam, Achternaam, TrajectID, ArtsID) VALUES (@Id, @Voornaam, @Achternaam, @TrajectID, @ArtsID)", patient);
            }
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE Patient SET Voornaam = @Voornaam, Achternaam = @Achternaam, TrajectID = @TrajectID, ArtsID = @ArtsID WHERE Id = @Id", patient);
            }
        }

        public async Task DeletePatientAsync(string id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM Patient WHERE Id = @Id", new { id });
            }
        }
    }
}
