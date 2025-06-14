using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PriceInCents { get; set; }

    }
}
