using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgmaatjeWebApi.TrajectZorgMoment.Repositories
{
    public interface ITrajectZorgMomentRepository
    {
        Task<TrajectZorgMoment> GetByIdAsync(TrajectZorgMomentKey key);
        Task<IEnumerable<TrajectZorgMoment>> GetAllAsync();
        Task AddAsync(TrajectZorgMoment trajectZorgMoment);
        Task UpdateAsync(TrajectZorgMoment trajectZorgMoment);
        Task DeleteAsync(TrajectZorgMomentKey key);
        Task<int> DeleteTrajectZorgMomentenByPatientIdAsync(string patientId);
    }
}