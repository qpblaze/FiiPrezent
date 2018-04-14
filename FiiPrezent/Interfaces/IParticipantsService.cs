using System.Threading.Tasks;
using FiiPrezent.Entities;
using FiiPrezent.Services;

namespace FiiPrezent.Interfaces
{
    public interface IParticipantsService
    {
        /// <summary>
        ///     Register a participant to an event.
        /// </summary>
        /// <param name="code">The secret code of the event.</param>
        /// <param name="participant">Some data about the participant.</param>
        /// <returns>An error message if the event with the
        ///     <param name="code"></param>
        ///     isn't found.
        /// </returns>
        Task<ResultStatus> RegisterParticipantAsync(string code, Participant participant);
    }
}