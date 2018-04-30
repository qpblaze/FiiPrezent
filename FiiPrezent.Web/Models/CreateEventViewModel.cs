using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FiiPrezent.Web.Models
{
    public class CreateEventViewModel
    {
        [Required(ErrorMessage = "Please enter the name of the event.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the description.")]
        public string Description { get; set; }

        [DisplayName("Secret Code")]
        [Required(ErrorMessage = "Please enter the secret code.")]
        public string SecretCode { get; set; }

        [Required(ErrorMessage = "Please enter the location.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        [DisplayName("Image")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please enter the date.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}