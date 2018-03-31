using System;
using System.Collections.Generic;

namespace FiiPrezent.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SecretCode { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}