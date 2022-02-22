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

        public async Task<IEnumerable<LawEnforcement>> GetAll() => await _context.LawEnforcements.Include(x => x.Events).ToListAsync();
        public async Task Add(LawEnforcement lawEnforcement)
        {
            await _context.LawEnforcements.AddAsync(lawEnforcement);
            await _context.SaveChangesAsync();
        }
        public async Task AddEventToEntiy(string eventId, int id)
        {
            var eventObj = new Event { Id = eventId };
            var entity = await _context.LawEnforcements.Include(x => x.Events).SingleOrDefaultAsync(x => x.Id == id);
            await _context.Events.AddAsync(eventObj);

            if (entity.Events == null)
                entity.Events = new List<Event>();

            entity.Events.Add(eventObj);
            _context.LawEnforcements.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
