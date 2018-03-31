﻿using FiiPrezent.Controllers;
using FiiPrezent.Entities;
using FiiPrezent.Services;
using System.Linq;

namespace FiiPrezent.Models
{
    public class EventViewModel
    {
        public EventViewModel(Event @event)
        {
            Id = @event.Id.ToString();
            Name = @event.Name;
            Description = @event.Description;
            Participants = @event.Participants.Select(x => x.Name).ToArray();
        }

        public string Id { get; set; }
        public string Name { get; }
        public string Description { get; }
        public string[] Participants { get; }
    }
}