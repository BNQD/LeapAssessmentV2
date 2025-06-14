namespace EventsApi.Models
{
    public class EventSummaryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TicketCount { get; set; }
        public int TotalSales { get; set; }
    }
}