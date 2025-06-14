using EventsApi.Context;
using EventsApi.Models;
using EventsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventsService;
        private readonly ITicketService _ticketService;

        public EventsController(IEventService eventsService, ITicketService ticketService)
        {
            _eventsService = eventsService;
            _ticketService = ticketService;
        }

        [HttpGet("GetEvents")]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _eventsService.GetEventsAsync();
            return events;
        }
        [HttpGet("GetTopFiveHighestSellingEvents")]
        public async Task<IEnumerable<Event>> GetTopFiveHighestSellingEvents()
        {
            var events = await _ticketService.GetTopFiveHighestSellingEventsAsync();
            return events;
        }
        [HttpGet("GetTopFiveHighestCountEvents")]
        public async Task<IEnumerable<Event>> GetTopFiveHighestCountEvents()
        {
            var events = await _ticketService.GetTopFiveHighestCountEventsAsync();
            return events;
        }
    }
}
