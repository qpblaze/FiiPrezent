using FiiPrezent.Entities;
using System.Collections;
using System.Collections.Generic;
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
            Participants = @event.Participants;
        }

        public string Id { get; set; }
        public string Name { get; }
        public string Description { get; }
        public IEnumerable<Participant> Participants { get; set; }
    }
}