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
                .HasKey(x => new { x.AccountId, x.EventId });

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Account)
                .WithMany(a => a.Participants)
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}