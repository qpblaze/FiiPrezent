using System.Threading.Tasks;
using FiiPrezent.Data;
using FiiPrezent.Entities;
using FiiPrezent.Interfaces;

namespace FiiPrezent
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Event> Events { get; }
        public IRepository<Participant> Participants { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IRepository<Event> events, 
            IRepository<Participant> participants)
        {
            _context = context;
            Events = events;
            Participants = participants;
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