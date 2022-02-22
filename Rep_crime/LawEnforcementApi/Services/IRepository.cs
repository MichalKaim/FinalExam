using LawEnforcementApi.Model;

namespace LawEnforcementApi.Services
{
    public interface IRepository
    {
        Task Add(LawEnforcement lawEnforcement);
        Task AddEventToEntiy(string eventId, int id);
        Task<IEnumerable<LawEnforcement>> GetAll();
    }
}