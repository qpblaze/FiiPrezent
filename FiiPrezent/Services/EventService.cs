using FiiPrezent.Entities;
using FiiPrezent.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FiiPrezent.Services
{
    public class EventService : IEventService
    {
        private readonly IParticipantsUpdated _participantsUpdated;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(
            IParticipantsUpdated participantsUpdated,
            IUnitOfWork unitOfWork)
        {
            _participantsUpdated = participantsUpdated;
            _unitOfWork = unitOfWork;
        }

        private async Task<Event> GetByVerificationCode(string code)
        {
            return (await _unitOfWork.Events.GetAsync(e => e.SecretCode == code)).SingleOrDefault();
        }

        public async Task<Event> RegisterParticipantAsync(string code, string name)
        {
            var @event = await GetByVerificationCode(code);

            if (@event == null)
            {
                return null;
            }

            var participant = new Participant
            {
                Name = name,
                Event = @event
            };

            await _unitOfWork.Participants.AddAsync(participant);
            await _unitOfWork.CompletedAsync();

            var participants = (await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id)).Select(x => x.Name).ToArray();

            _participantsUpdated.OnParticipantsUpdated(@event.Id, participants);

            return @event;
        }
    }
}