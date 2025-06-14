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
        private List<EventSummaryDto> _testEvents;

        [SetUp]
        public void Setup()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _ticketService = new TicketService(_mockEventRepository.Object);

            _testEvents = new List<EventSummaryDto>
            {
                new EventSummaryDto
                {
                    Id = "1",
                    Name = "Test Event 1",
                    TicketCount = 100,
                    TotalSales = 50000
                },
                new EventSummaryDto
                {
                    Id = "2",
                    Name = "Test Event 2",
                    TicketCount = 150,
                    TotalSales = 75000
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