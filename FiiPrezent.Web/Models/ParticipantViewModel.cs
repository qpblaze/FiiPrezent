using FiiPrezent.Core.Entities;

namespace FiiPrezent.Web.Models
{
    public class ParticipantViewModel
    {
        public ParticipantViewModel(Participant participant)
        {
            Name = participant.Account.Name;
            Email = participant.Account.Email;
            Picture = participant.Account.Picture;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }
}