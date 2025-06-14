using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.Services
{
    public interface IEventService
    {
        public Task<List<Event>> GetEventsAsync();
        
    }
}
