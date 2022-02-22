using CrimeApi.Models;

namespace CrimeApi.Services
{
    public interface IEventRepository
    {
        Task AddLawEnforcement(string eventId, int lawId);
        Task<string> Create(CrimeEvent crimeEvent);
        Task<IEnumerable<CrimeEvent>> GetAllAsync();
    }
}