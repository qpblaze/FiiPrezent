using System;
using System.Collections.Generic;

namespace FiiPrezent.Web.Models
{
    public class EventViewModel
    {
        public string Id { get; set; }
        public string NameIdentifier { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<ParticipantViewModel> Participants { get; set; }
    }
}