using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FiiPrezent.Core.Entities
{
    public class Event : BaseEntity
    {
        public Guid AccountId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string SecretCode { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}