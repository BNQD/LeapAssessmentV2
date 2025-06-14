using EventsApi.Context;
using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsApi.Services
{
    public class TicketService : ITicketService
    {
        private readonly IEventRepository _eventRepository;

        public TicketService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventSummaryDto>> GetTopFiveHighestSellingEventsAsync()
        {
            return await _eventRepository.GetTopFiveHighestSellingEventsAsync();
        }

        public async Task<List<EventSummaryDto>> GetTopFiveHighestCountEventsAsync()
        {
            return await _eventRepository.GetTopFiveHighestCountEventsAsync();
        }
    }
}
