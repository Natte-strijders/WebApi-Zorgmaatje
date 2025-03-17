using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZorgmaatjeWebApi
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string sqlConnectionString;

        public PatientRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            Patient patient1 = new Patient();
            return patient1;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            Patient patient1 = new Patient();
            Patient patient2 = new Patient();
            List<Patient> patients = new List<Patient>();
            return patients;
        }

        public async Task AddPatient(Patient patient)
        {
        }

        public async Task UpdatePatient(Patient patient)
        {
        }

        public async Task DeletePatient(int id)
        {
        }
    }
}
