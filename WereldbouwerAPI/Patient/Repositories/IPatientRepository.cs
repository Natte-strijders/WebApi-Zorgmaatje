using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgmaatjeWebApi.Patient.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientById(int id);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(int id);
    }
}

