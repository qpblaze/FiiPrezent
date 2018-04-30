using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FiiPrezent.Core;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace FiiPrezent.Infrastructure.Services
{
    public class EventsService : IEventsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountsService _accountsService;
        private readonly IFileManager _fileManager;

        public EventsService(
            IUnitOfWork unitOfWork,
            IAccountsService accountsService,
            IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _accountsService = accountsService;
            _fileManager = fileManager;
        }

        public async Task<ResultStatus> CreateEventAsync(Event @event, string nameIdentifier)
        {
            if ((await _unitOfWork.Events.GetAsync(x => x.SecretCode == @event.SecretCode)).Any())
                return new ResultStatus(ResultStatusType.InvalidCode, nameof(@event.SecretCode), "This code is already in use.");

            @event.Id = new Guid();
            @event.ImagePath = await _fileManager.UploadAsync(@event.Image);
            @event.Account = await _accountsService.GetAccountByNameIdentifier(nameIdentifier);

            await _unitOfWork.Events.AddAsync(@event);
            await _unitOfWork.CompletedAsync();

            return new ResultStatus();
        }

        public async Task<IEnumerable<Event>> ListAllEventsAsync()
        {
            return await _unitOfWork.Events.ListAllAsync(x => x.Account);
        }

        public async Task<Event> GetEventAsync(Guid id, bool include = true)
        {
            var @event = await _unitOfWork.Events.GetByIdAsync(id);

            if (!include)
                return @event;

            @event.Participants = await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id);
            @event.Account = await _accountsService.GetAccountById(@event.AccountId);

            return @event;
        }

        public async Task<ResultStatus> DeleteEvent(Guid id)
        {
            var @event = await _unitOfWork.Events.GetByIdAsync(id);

            if (@event == null)
                return new ResultStatus(ResultStatusType.NotFound);

            foreach (var participant in (await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id)))
            {
                _unitOfWork.Participants.Delete(participant);
            }

            _fileManager.Delete(@event.ImagePath);
            _unitOfWork.Events.Delete(@event);

            await _unitOfWork.CompletedAsync();

            return new ResultStatus();
        }

        public async Task<ResultStatus> UpdateEventAsync(Event @event)
        {
            var oldEvent = await _unitOfWork.Events.GetByIdAsync(@event.Id);

            oldEvent.Name = @event.Name;
            oldEvent.Description = @event.Description;
            oldEvent.Location = @event.Location;
            oldEvent.SecretCode = @event.SecretCode;
            oldEvent.Date = @event.Date;

            if (@event.Image != null)
            {
                _fileManager.Delete(oldEvent.ImagePath);
                oldEvent.ImagePath = await _fileManager.UploadAsync(@event.Image);
            }

            _unitOfWork.Events.Update(oldEvent);

            await _unitOfWork.CompletedAsync();

            return new ResultStatus();
        }
    }
}