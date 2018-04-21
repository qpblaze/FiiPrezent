using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FiiPrezent.Core.Entities
{
    public class Participant : BaseEntity
    {
        public Guid AccountId { get; set; }
        public Guid EventId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }
    }
}