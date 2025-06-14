using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Services
{
    public interface ITicketService
    {
        public Task<List<Event>> GetTopFiveHighestSellingEventsAsync();
        public Task<List<Event>> GetTopFiveHighestCountEventsAsync();
    }
}
