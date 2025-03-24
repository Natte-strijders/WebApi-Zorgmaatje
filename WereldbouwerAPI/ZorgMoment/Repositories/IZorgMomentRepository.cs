namespace ZorgmaatjeWebApi.ZorgMoment.Repositories
{
    public interface IZorgMomentRepository
    {
        Task<ZorgMoment> GetByIdAsync(int id);
        Task<IEnumerable<ZorgMoment>> GetAllAsync();
        Task AddAsync(ZorgMoment zorgMoment);
        Task UpdateAsync(ZorgMoment zorgMoment);
        Task DeleteAsync(int id);

    }
}
