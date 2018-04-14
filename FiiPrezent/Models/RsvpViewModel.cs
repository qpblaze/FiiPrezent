using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiiPrezent.Models
{
    public class RsvpViewModel
    {
        [Required(ErrorMessage = "Please enter the secret code.")]
        [DisplayName("Secret Code")]
        [DataType(DataType.Password)]
        public string Code { get; set; }
    }
}