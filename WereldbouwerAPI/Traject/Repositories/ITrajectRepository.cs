using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgmaatjeWebApi.Traject.Repositories
{
    public interface ITrajectRepository
    {
        Task<Traject> GetTrajectByNaamAsync(string naam);
        Task<IEnumerable<Traject>> GetAllTrajectsAsync();
        Task AddTrajectAsync(Traject traject);
        Task UpdateTrajectAsync(Traject traject);
        Task DeleteTrajectAsync(string naam);
    }
}

