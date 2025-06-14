using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Models
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEventsAsync();
        Task<List<EventSummaryDto>> GetTopFiveHighestSellingEventsAsync();
        Task<List<EventSummaryDto>> GetTopFiveHighestCountEventsAsync();
    }
}
