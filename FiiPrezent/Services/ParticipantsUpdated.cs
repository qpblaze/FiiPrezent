using System;
using System.Collections.Generic;
using System.Linq;
using FiiPrezent.Entities;
using FiiPrezent.Hubs;
using FiiPrezent.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace FiiPrezent.Services
{
    class ParticipantsUpdated : IParticipantsUpdated
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
