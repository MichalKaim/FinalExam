using CrimeApi.Models;

namespace CrimeApi.Services
{
    public interface IEventRepository
    {
        Task Create(CrimeEvent crimeEvent);
        Task<IEnumerable<CrimeEvent>> GetAllAsync();
    }
}