using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartsOn { get; set; }
        public DateTime EndsOn { get; set; }
        public string Location { get; set; }
    }
}
