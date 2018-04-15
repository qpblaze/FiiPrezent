using FiiPrezent.Entities;
using System;
using System.Collections.Generic;

namespace FiiPrezent.Interfaces
{
    public interface IParticipantsUpdated
    {
        void OnParticipantsUpdated(Guid eventId, List<Participant> newParticipants);
    }
}