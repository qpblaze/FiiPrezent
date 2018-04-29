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
        private readonly IAccountsService _accountsService;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipantsService(
            IParticipantsUpdated participantsUpdated,
            IUnitOfWork unitOfWork, 
            IAccountsService accountsService)
        {
            _participantsUpdated = participantsUpdated;
            _unitOfWork = unitOfWork;
            _accountsService = accountsService;
        }

        public async Task<ResultStatus> RegisterParticipantAsync(string code, string nameIdentifier)
        {
            var @event = (await _unitOfWork.Events.GetAsync(e => e.SecretCode == code)).SingleOrDefault();

            if (@event == null)
                return new ResultStatus(ResultStatusType.AlreadyExists, "Code", "Wrong verification code.");

            if((await _unitOfWork.Participants.GetAsync(x => x.Account.NameIdentifier == nameIdentifier && x.EventId == @event.Id)).Any())
                return new ResultStatus(ResultStatusType.AlreadyGoing, "Code", "You are already going to this event.");

            Participant participant = new Participant
            {
                Id = Guid.NewGuid(),
                EventId = @event.Id,
                AccountId = (await _accountsService.GetAccountByNameIdentifier(nameIdentifier)).Id
            };

            await _unitOfWork.Participants.AddAsync(participant);
            await _unitOfWork.CompletedAsync();

            var participants = (await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id)).ToList();

            _participantsUpdated.OnParticipantsUpdated(@event.Id, participants);

            return new ResultStatus(ResultStatusType.Ok, @event.Id);
        }
    }
}