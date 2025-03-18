using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgmaatjeWebApi.Patient.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientByIdAsync(string id);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(string id);
    }
}

