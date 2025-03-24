using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgmaatjeWebApi.TrajectZorgMoment.Repositories
{
    public interface ITrajectZorgMomentRepository
    {
        Task<TrajectZorgMoment> GetByIdAsync(int id);
        Task<IEnumerable<TrajectZorgMoment>> GetAllAsync();
        Task AddAsync(TrajectZorgMoment trajectZorgMoment);
        Task UpdateAsync(TrajectZorgMoment trajectZorgMoment);
        Task DeleteAsync(int id);
    }
}