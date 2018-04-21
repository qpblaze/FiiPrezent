using FiiPrezent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiiPrezent.Infrastructure.Data
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
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Events)
                .WithOne(e => e.Account)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Account)
                .WithMany(a => a.Participants)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}