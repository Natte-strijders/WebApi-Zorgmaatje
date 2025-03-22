namespace ZorgmaatjeWebApi.Arts.Repositories
{
    public interface IArtsRepository
    {
        Task<Arts> GetArtsByNaamAsync(string naam);
        Task<IEnumerable<Arts>> GetAllArtsAsync();
        Task AddArtsAsync(Arts arts);
        Task UpdateArtsAsync(Arts arts);
        Task DeleteArtsAsync(string naam);
    }
}
