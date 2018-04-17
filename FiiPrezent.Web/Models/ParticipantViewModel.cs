using FiiPrezent.Core.Entities;

namespace FiiPrezent.Web.Models
{
    public class ParticipantViewModel
    {
        public ParticipantViewModel(Participant participant)
        {
            Name = participant.Name;
            Email = participant.Email;
            ImagePath = participant.ImagePath;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
    }
}