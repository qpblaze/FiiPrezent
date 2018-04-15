using System;
using System.Linq;
using System.Threading.Tasks;
using FiiPrezent.Entities;
using FiiPrezent.Interfaces;

namespace FiiPrezent.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IParticipantsUpdated _participantsUpdated;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipantsService(
            IParticipantsUpdated participantsUpdated,
            IUnitOfWork unitOfWork)
        {
            _participantsUpdated = participantsUpdated;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultStatus> RegisterParticipantAsync(string code, Participant participant)
        {
            var @event = (await _unitOfWork.Events.GetAsync(e => e.SecretCode == code)).SingleOrDefault();

            if (@event == null)
                return new ResultStatus("SecretCode", "Wrong verification code.");

            participant.Id = Guid.NewGuid();
            participant.Event = @event;

            await _unitOfWork.Participants.AddAsync(participant);
            await _unitOfWork.CompletedAsync();

            var participants = (await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id)).ToList();

            _participantsUpdated.OnParticipantsUpdated(@event.Id, participants);

            return new ResultStatus(@event.Id);
        }
    }
}