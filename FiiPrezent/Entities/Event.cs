using System.Collections.Generic;

namespace FiiPrezent.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SecretCode { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}