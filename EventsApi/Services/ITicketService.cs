using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Services
{
    public interface ITicketService
    {
        Task<List<EventSummaryDto>> GetTopFiveHighestSellingEventsAsync();
        Task<List<EventSummaryDto>> GetTopFiveHighestCountEventsAsync();
    }
}
