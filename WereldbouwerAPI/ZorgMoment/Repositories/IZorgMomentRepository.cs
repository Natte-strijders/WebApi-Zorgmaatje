namespace ZorgmaatjeWebApi.ZorgMoment.Repositories
{
    public interface IZorgMomentRepository
    {
        Task<ZorgMoment> GetByIdAsync(int id);
        Task<ZorgMoment> GetZorgMomentByNameAndPatientIdAsync(string naam, string patientId);
        Task<IEnumerable<dynamic>> GetZorgMomentenByPatientIdSortedByVolgordeAsync(string patientId);
        Task<IEnumerable<ZorgMoment>> GetAllAsync();
        Task AddAsync(ZorgMoment zorgMoment);
        Task UpdateAsync(ZorgMoment zorgMoment);
        Task DeleteAsync(int id);
        Task<int> DeleteZorgMomentenByPatientIdAsync(string patientId);

    }
}
