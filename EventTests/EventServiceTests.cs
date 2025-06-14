using EventsApi.Models;
using EventsApi.Services;
using Moq;
using NUnit.Framework;

namespace EventTests
{
    [TestFixture]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _mockEventRepository;
        private EventService _eventService;
        private List<Event> _testEvents;

        [SetUp]
        public void Setup()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _eventService = new EventService(_mockEventRepository.Object);

            // Setup test data
            _testEvents = new List<Event>
            {
                new Event
                {
                    Id = "1",
                    Name = "Test Event 1",
                    Location = "Location 1",
                    StartsOn = DateTime.Now.AddDays(1),
                    EndsOn = DateTime.Now.AddDays(2)
                },
                new Event
                {
                    Id = "2",
                    Name = "Test Event 2",
                    Location = "Location 2",
                    StartsOn = DateTime.Now.AddDays(3),
                    EndsOn = DateTime.Now.AddDays(4)
                }
            };
        }

        [Test]
        public async Task GetEventsAsync_ReturnsEvents()
        {
            _mockEventRepository.Setup(repo => repo.GetEventsAsync())
                .ReturnsAsync(_testEvents);

            var result = await _eventService.GetEventsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            _mockEventRepository.Verify(repo => repo.GetEventsAsync(), Times.Once);
        }

        [Test]
        public async Task GetEventsAsync_HandlesException()
        {
            _mockEventRepository.Setup(repo => repo.GetEventsAsync())
                .ThrowsAsync(new Exception("Database error"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _eventService.GetEventsAsync());
            Assert.That(ex.Message, Is.EqualTo("Database error"));
        }
    }
}