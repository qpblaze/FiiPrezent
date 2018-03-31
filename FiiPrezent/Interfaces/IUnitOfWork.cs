using System;
using System.Threading.Tasks;
using FiiPrezent.Entities;

namespace FiiPrezent.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Event> Events { get; }
        IRepository<Participant> Participants { get; }
        
        Task CompletedAsync();
    }
}