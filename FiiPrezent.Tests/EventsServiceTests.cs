using Microsoft.AspNetCore.SignalR;
using FiiPrezent.Services;
using Shouldly;
using Xunit;
using Moq;
using FiiPrezent.Hubs;
using FiiPrezent.Interfaces;

namespace FiiPrezent.Tests
{
    public class EventsServiceTests
    {
        private readonly EventService _service;

        public EventsServiceTests()
        {
            var participantsUpdated = new Mock<IParticipantsUpdated>();
            var unitOfWork = new Mock<IUnitOfWork>();
            
            _service = new EventService(participantsUpdated.Object, unitOfWork.Object);
        }

        [Fact]
        public void RegisterParticipant_WithAnInvalidCode_ReturnsError()
        {
            var result = _service.RegisterParticipantAsync("bad code", "test participant");

            result.ShouldBeNull();
        }

        [Fact]
        public void RegisterParticipant_WithAValidCode_ReturnsSuccess()
        {
            var result = _service.RegisterParticipantAsync("cometothedarksidewehavecookies", "test participant");

            result.ShouldNotBeNull();
        }

        [Fact]
        public void RegisterParticipant_WithAValidCode_AddsParticipantToEvent()
        {
            var result = _service.RegisterParticipantAsync("cometothedarksidewehavecookies", "Tudor");

           // result.Participants.ShouldContain("Tudor");
        }
    }
}
