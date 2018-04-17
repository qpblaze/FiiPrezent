using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Core.Interfaces
{
    public interface IEventsService
    {
        /// <summary>
        ///     Adds an event to the storage.
        /// </summary>
        /// <param name="event">The event to be added.</param>
        /// <returns>Success = true if the event was created, a list of errors otherwise. </returns>
        Task<ResultStatus> CreateEventAsync(Event @event);

        /// <summary>
        ///     Gets all events from the storage.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Event>> ListAllEventsAsync();

        /// <summary>
        ///     Gets an event with all its participants.
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>The event with the
        ///     <param name="id">id</param>
        ///     and its participants.
        /// </returns>
        Task<ResultStatus> GetEvent(Guid id);

        /// <summary>
        ///     Deletes an event from the storage.
        /// </summary>
        /// <param name="id">The id of the event to be deleted. </param>
        /// <returns>Type.NotFound if the event isn't found.</returns>
        Task<ResultStatus> DeleteEvent(Guid id);
    }
}