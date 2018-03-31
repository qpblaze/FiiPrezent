using System.ComponentModel.DataAnnotations;

namespace FiiPrezent.Models
{
    public class CreateEventViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SecretCode { get; set; }
    }
}