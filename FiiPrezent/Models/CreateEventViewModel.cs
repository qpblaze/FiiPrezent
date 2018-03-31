using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiiPrezent.Models
{
    public class CreateEventViewModel
    {
        [Required(ErrorMessage = "Please enter the name of the event.")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the description.")]
        [MaxLength(100)]
        public string Description { get; set; }

        [DisplayName("Secret Code")]
        [Required(ErrorMessage = "Please enter the secret code.")]
        public string SecretCode { get; set; }

        [Required(ErrorMessage = "Please enter the location.")] 
        [MaxLength(20)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter the date.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}