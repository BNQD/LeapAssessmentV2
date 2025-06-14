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

    public async Task<List<EventSummaryDto>> GetTopFiveHighestSellingEventsAsync()
    {
        return await _context.Events
            .Join(
                _context.TicketSales.GroupBy(x => x.EventId)
                    .Select(g => new
                    {
                        EventId = g.Key,
                        TotalSales = g.Sum(x => x.PriceInCents),
                        TicketCount = g.Count()
                    }),
                e => e.Id,
                s => s.EventId,
                (e, s) => new EventSummaryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    TotalSales = s.TotalSales,
                    TicketCount = s.TicketCount
                })
            .OrderByDescending(x => x.TotalSales)
            .Take(5)
            .ToListAsync();
    }

    public async Task<List<EventSummaryDto>> GetTopFiveHighestCountEventsAsync()
    {
        return await _context.Events
            .Join(
                _context.TicketSales.GroupBy(x => x.EventId)
                    .Select(g => new
                    {
                        EventId = g.Key,
                        TotalSales = g.Sum(x => x.PriceInCents),
                        TicketCount = g.Count()
                    }),
                e => e.Id,
                s => s.EventId,
                (e, s) => new EventSummaryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    TotalSales = s.TotalSales,
                    TicketCount = s.TicketCount
                })
            .OrderByDescending(x => x.TicketCount)
            .Take(5)
            .ToListAsync();
    }
}