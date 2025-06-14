using EventsApi.Context;
using EventsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventRepository : IEventRepository
{
    private readonly EventsContext _context;

    public EventRepository(EventsContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetEventsAsync()
    {
        return await _context.Events.ToListAsync();
    }
    public async Task<List<Event>> GetTopFiveHighestSellingEventsAsync()
    {
        var query = _context.TicketSales
            .GroupBy(x => x.EventId)
            .Select(e => new
            {
                EventId = e.Key,
                TotalSales = e.Sum(x => x.PriceInCents)
            })
            .OrderByDescending(x => x.TotalSales)
            .Take(5);

        var eventIds = await query.Select(x => x.EventId).ToListAsync();
        return await _context.Events
            .Where(e => eventIds.Contains(e.Id))
            .ToListAsync();
    }

    public async Task<List<Event>> GetTopFiveHighestCountEventsAsync()
    {
        var query = _context.TicketSales
            .GroupBy(x => x.EventId)
            .Select(e => new
            {
                EventId = e.Key,
                TicketCount = e.Count()
            })
            .OrderByDescending(x => x.TicketCount)
            .Take(5);

        var eventIds = await query.Select(x => x.EventId).ToListAsync();
        return await _context.Events
            .Where(e => eventIds.Contains(e.Id))
            .ToListAsync();
    }
}