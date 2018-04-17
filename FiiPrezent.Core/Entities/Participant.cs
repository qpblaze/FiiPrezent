using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FiiPrezent.Core.Entities
{
    public class Participant : BaseEntity
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public Guid EventId { get; set; }

        [IgnoreDataMember]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
    }
}