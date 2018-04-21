using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiiPrezent.Core.Entities
{
    public class Account : BaseEntity
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }

        [JsonIgnore]
        public ICollection<Event> Events { get; set; }

        [JsonIgnore]
        public ICollection<Participant> Participants { get; set; }
    }
}