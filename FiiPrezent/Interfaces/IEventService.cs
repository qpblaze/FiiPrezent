using System.Threading.Tasks;
using FiiPrezent.Entities;

namespace FiiPrezent.Interfaces
{
    public interface IEventService
    {
        Task<Event> RegisterParticipantAsync(string code, string name);
    }
}