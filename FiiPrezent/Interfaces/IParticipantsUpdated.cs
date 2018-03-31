using System;

namespace FiiPrezent.Interfaces
{
    public interface IParticipantsUpdated
    {
        void OnParticipantsUpdated(Guid eventId, string[] newParticipants);
    }
}