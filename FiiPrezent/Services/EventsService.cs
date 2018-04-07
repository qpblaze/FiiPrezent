using System;
using System.Collections.Generic;
using FiiPrezent.Entities;
using FiiPrezent.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FiiPrezent.Services
{
    public class EventsService : IEventsService
    {
        private readonly IParticipantsUpdated _participantsUpdated;
        private readonly IUnitOfWork _unitOfWork;

        public EventsService(
            IParticipantsUpdated participantsUpdated,
            IUnitOfWork unitOfWork)
        {
            _participantsUpdated = participantsUpdated;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultStatus> CreateEventAsync(Event @event)
        {
            if ((await _unitOfWork.Events.GetAsync(x => x.SecretCode == @event.SecretCode)).Any())
            {
                return new ResultStatus(nameof(@event.SecretCode), "This code is already in use.");
            }
            
            @event.Id = new Guid();

            await _unitOfWork.Events.AddAsync(@event);
            await _unitOfWork.CompletedAsync();
            
            return new ResultStatus();
        }

        public async Task<IEnumerable<Event>> ListAllEventsAsync()
        {
            return await _unitOfWork.Events.ListAllAsync();
        }

        public async Task<ResultStatus> GetEvent(Guid id)
        {
            var @event = await _unitOfWork.Events.GetByIdAsync(id);
            
            if(@event == null)
                return new ResultStatus(ErrorType.NotFound);

            @event.Participants = await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id);
            
            return new ResultStatus(@event);
        }

        public async Task<ResultStatus> DeleteEvent(Guid id)
        {
            var @event = await _unitOfWork.Events.GetByIdAsync(id);
            
            if(@event == null)
                return new ResultStatus(ErrorType.NotFound);

            _unitOfWork.Events.Delete(@event);
            await _unitOfWork.CompletedAsync();
            
            return new ResultStatus();
        }
    }
}