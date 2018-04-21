using System;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Event> Events { get; }
        IRepository<Participant> Participants { get; }
        IRepository<Account> Accounts { get; }

        Task CompletedAsync();
    }
}