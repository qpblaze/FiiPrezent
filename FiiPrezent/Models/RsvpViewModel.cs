using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiiPrezent.Models
{
    public class RsvpViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the secret code.")]
        [DisplayName("Secret Code")]
        public string Code { get; set; }
    }
}