using EventsApi.Models;
using EventsApi.Services;
using Moq;
using NUnit.Framework;

namespace EventTests
{
    [TestFixture]
    public class TicketServiceTests
    {
        private Mock<IEventRepository> _mockEventRepository;
        private TicketService _ticketService;
        private List<Event> _testEvents;

        [SetUp]
        public void Setup()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _ticketService = new TicketService(_mockEventRepository.Object);

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
        public async Task GetTopFiveHighestSellingEventsAsync_ReturnsEvents()
        {
            _mockEventRepository.Setup(repo => repo.GetTopFiveHighestSellingEventsAsync())
                .ReturnsAsync(_testEvents);

            var result = await _ticketService.GetTopFiveHighestSellingEventsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            _mockEventRepository.Verify(repo =>
                repo.GetTopFiveHighestSellingEventsAsync(), Times.Once);
        }

        [Test]
        public async Task GetTopFiveHighestCountEventsAsync_ReturnsEvents()
        {
            _mockEventRepository.Setup(repo => repo.GetTopFiveHighestCountEventsAsync())
                .ReturnsAsync(_testEvents);

            var result = await _ticketService.GetTopFiveHighestCountEventsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            _mockEventRepository.Verify(repo =>
                repo.GetTopFiveHighestCountEventsAsync(), Times.Once);
        }

        [Test]
        public async Task GetTopFiveHighestSellingEventsAsync_HandlesException()
        {
            _mockEventRepository.Setup(repo => repo.GetTopFiveHighestSellingEventsAsync())
                .ThrowsAsync(new Exception("Database error"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _ticketService.GetTopFiveHighestSellingEventsAsync());
            Assert.That(ex.Message, Is.EqualTo("Database error"));
        }

        [Test]
        public async Task GetTopFiveHighestCountEventsAsync_HandlesException()
        {
            _mockEventRepository.Setup(repo => repo.GetTopFiveHighestCountEventsAsync())
                .ThrowsAsync(new Exception("Database error"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _ticketService.GetTopFiveHighestCountEventsAsync());
            Assert.That(ex.Message, Is.EqualTo("Database error"));
        }
    }
}