using FiiPrezent.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiiPrezent.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}