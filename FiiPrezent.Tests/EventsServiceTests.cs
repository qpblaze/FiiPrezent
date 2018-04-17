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
    public class EventsServiceTests
    {
        public EventsServiceTests()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            _eventRepositoryMock = new Mock<IRepository<Event>>();

            unitOfWorkMock.Setup(x => x.Events).Returns(_eventRepositoryMock.Object);

            _eventsService = new EventsService(unitOfWorkMock.Object);
        }

        private readonly Mock<IRepository<Event>> _eventRepositoryMock;
        private readonly IEventsService _eventsService;

        [Fact]
        public async void CreateEventAsync_WhenCodeIsTaken_ReturnsError()
        {
            // TODO: nu merge cu expresii exacte (x => x.SecretCode == "code")
            _eventRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Event, bool>>>()))
                .ReturnsAsync(() => new List<Event>
                {
                    new Event()
                });

            var result = await _eventsService.CreateEventAsync(
                new Event
                {
                    SecretCode = "code"
                }
            );

            result.Type.ShouldBe(ResultStatusType.InvalidCode);
        }

        [Fact]
        public async void CreateEventAsync_WhenTheCodeIsValid_ReturnsNoError()
        {
            // TODO: nu merge cu expresii exacte (x => x.SecretCode == "code")
            _eventRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Event, bool>>>()))
                .ReturnsAsync(() => new List<Event>());

            var result = await _eventsService.CreateEventAsync(
                new Event
                {
                    SecretCode = "code"
                }
            );

            result.Type.ShouldBe(ResultStatusType.Ok);
        }
    }
}