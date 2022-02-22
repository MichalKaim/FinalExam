using LawEnforcementApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
/*            var splitStringConverter = new ValueConverter<IList<string>, string>(v => string.Join(";", v), v => v.Split(new[] { ';' }));
            modelBuilder.Entity<LawEnforcement>().Property(nameof(LawEnforcement.Events)).HasConversion(splitStringConverter);*/
        }
    }
}
