using LawEnforcementApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        public DbSet<LawEnforcement>? LawEnforcements { get; set; }
        public DbSet<Event>? Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
