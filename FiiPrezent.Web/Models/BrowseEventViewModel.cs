using System;
using System.ComponentModel.DataAnnotations;

namespace FiiPrezent.Web.Models
{
    public class BrowseEventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd, dd MMMM yyyy}")]
        public DateTime Date { get; set; }
    }
}