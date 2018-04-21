using System;
using System.ComponentModel.DataAnnotations;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Web.Models
{
    public class BrowseEventViewModel
    {
        public BrowseEventViewModel(Event @event)
        {
            Id = @event.Id.ToString();
            AccountId = @event.Account.NameIdentifier;

            Name = @event.Name;
            Description = @event.Description;
            Location = @event.Location;
            ImagePath = @event.ImagePath;
            Date = @event.Date;
        }

        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}