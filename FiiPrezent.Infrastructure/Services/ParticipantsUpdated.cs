using System;
using System.Collections.Generic;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace FiiPrezent.Infrastructure.Services
{
    public class ParticipantsUpdated : IParticipantsUpdated
    {
        private readonly IHubContext<ParticipantsHub> _hubContext;

        public ParticipantsUpdated(IHubContext<ParticipantsHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public void OnParticipantsUpdated(Guid eventId, List<Participant> newParticipants)
        {
            var group = _hubContext.Clients.Group(eventId.ToString());

            group.SendAsync("Update", JsonConvert.SerializeObject(newParticipants));
        }

    }
}
