using System;
using System.Collections.Generic;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Core.Interfaces
{
    public interface IParticipantsUpdated
    {
        void OnParticipantsUpdated(Guid eventId, List<Participant> participants);
    }
}