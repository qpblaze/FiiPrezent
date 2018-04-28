using System;
using System.Collections.Generic;
using AutoMapper;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Infrastructure.Hubs;
using FiiPrezent.Web.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace FiiPrezent.Web.Services
{
    public class ParticipantsUpdated : IParticipantsUpdated
    {
        private readonly IHubContext<ParticipantsHub> _hubContext;
        private readonly IMapper _mapper;

        public ParticipantsUpdated(IHubContext<ParticipantsHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public void OnParticipantsUpdated(Guid eventId, List<Participant> participants)
        {
            var group = _hubContext.Clients.Group(eventId.ToString());

            group.SendAsync("Update",
                JsonConvert.SerializeObject(
                    _mapper.Map<IEnumerable<Participant>, IEnumerable<ParticipantViewModel>>(participants)));
        }
    }
}