using FiiPrezent.Entities;
using System.Collections.Generic;

namespace FiiPrezent.Models
{
    public class BroseEventsViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public FilterEventsViewModel Filter { get; set; }
    }
}