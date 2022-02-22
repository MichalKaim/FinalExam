using LawEnforcementApi.Data;
using LawEnforcementApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Services
{
    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LawEnforcement>> GetAll() => await _context.LawEnforcements.ToListAsync();
        public async Task Add(LawEnforcement lawEnforcement)
        {
            await _context.LawEnforcements.AddAsync(lawEnforcement);
            await _context.SaveChangesAsync();
        }
        public async Task AddEventToEntiy(string eventId, int id)
        {
            var entity = await _context.LawEnforcements.SingleOrDefaultAsync(x => x.Id == id);
            if (entity.Events == null)
            {
                entity.Events = new List<string>();
            }

            entity.Events.Add(eventId);
            _context.LawEnforcements.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
