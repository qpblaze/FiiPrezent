using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Core.Interfaces
{
    public interface IEventsService
    {
        Task<ResultStatus> CreateEventAsync(Event @event, string nameIdentifier);

        Task<ResultStatus> UpdateEventAsync(Event @event);

        Task<ResultStatus> DeleteEvent(Guid id);

        Task<Event> GetEventAsync(Guid id, bool include = true);

        Task<IEnumerable<Event>> ListAllEventsAsync();

        Task<bool> IsOwner(Guid eventId, string nameIdentifier);
    }
}