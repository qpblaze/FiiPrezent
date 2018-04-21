using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FiiPrezent.Core;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Infrastructure.Services;
using Moq;
using Shouldly;
using Xunit;

namespace FiiPrezent.Tests
{
    public class ParticipantsServiceTests
    {
        private readonly Mock<IRepository<Event>> _eventRepositoryMock;
        private readonly Mock<IRepository<Participant>> _participantRepositoryMock;
        private readonly IParticipantsService _participantsService;

        public ParticipantsServiceTests()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var participantsUpdatedMock = new Mock<IParticipantsUpdated>();

            _eventRepositoryMock = new Mock<IRepository<Event>>();
            _participantRepositoryMock = new Mock<IRepository<Participant>>();

            unitOfWorkMock.Setup(x => x.Events).Returns(_eventRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.Participants).Returns(_participantRepositoryMock.Object);

            _participantsService = new ParticipantsService(participantsUpdatedMock.Object, unitOfWorkMock.Object);
        }

        [Fact]
        public async void RegisterParticipantAsync_WhenCodeIsInvalid_ReturnsError()
        {
            // TODO: nu merge cu expresii exacte (x => x.SecretCode == "code")
            _eventRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Event, bool>>>()))
                .ReturnsAsync(() => new List<Event>
                {
                    new Event()
                    {
                        SecretCode = "code"
                    }
                });

            _participantRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Participant, bool>>>()))
                .ReturnsAsync(() => new List<Participant>());

            var result = await _participantsService.RegisterParticipantAsync("code", "");

            result.Type.ShouldBe(ResultStatusType.AlreadyExists);
        }

        [Fact]
        public async void RegisterParticipantAsync_WhenTheCodeIsValid_ReturnsNoErrorAndTheIdOfTheNewEvent()
        {
            // TODO: nu merge cu expresii exacte (x => x.SecretCode == "code")
            _eventRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Event, bool>>>()))
                .ReturnsAsync(() => new List<Event>());

            var participant = new Participant();

            var result = await _participantsService.RegisterParticipantAsync("code", "");

            result.Type.ShouldBe(ResultStatusType.Ok);
        }
    }
}