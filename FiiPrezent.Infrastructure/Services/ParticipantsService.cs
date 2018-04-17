using System;
using System.Linq;
using System.Threading.Tasks;
using FiiPrezent.Core;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;

namespace FiiPrezent.Infrastructure.Services
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
                return new ResultStatus(ResultStatusType.CodeAlreadyInUse, "SecretCode", "Wrong verification code.");

            participant.Id = Guid.NewGuid();
            participant.Event = @event;

            await _unitOfWork.Participants.AddAsync(participant);
            await _unitOfWork.CompletedAsync();

            var participants = (await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id)).ToList();

            _participantsUpdated.OnParticipantsUpdated(@event.Id, participants);

            return new ResultStatus(ResultStatusType.Ok, @event.Id);
        }
    }
}