using System.Threading.Tasks;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Infrastructure.Data;

namespace FiiPrezent.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Event> Events { get; }
        public IRepository<Participant> Participants { get; }
        public IRepository<Account> Accounts { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IRepository<Event> events, 
            IRepository<Participant> participants, 
            IRepository<Account> accounts)

        {
            _context = context;
            Events = events;
            Participants = participants;
            Accounts = accounts;
        }

        public async Task CompletedAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}