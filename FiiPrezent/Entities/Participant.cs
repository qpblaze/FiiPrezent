using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiiPrezent.Entities
{
    public class Participant : BaseEntity
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
    }
}