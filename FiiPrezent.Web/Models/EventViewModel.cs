using System.Collections.Generic;
using System.Linq;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Web.Models
{
    public class EventViewModel
    {
        public EventViewModel(Event @event)
        {
            Id = @event.Id.ToString();
            Name = @event.Name;
            Description = @event.Description;

            Participants = @event.Participants?.Select(x => new ParticipantViewModel(x));
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<ParticipantViewModel> Participants { get; set; }
    }
}