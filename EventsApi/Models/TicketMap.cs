using EventsApi.Models;
using FluentNHibernate.Mapping;

namespace EventsApi.Models
{
    public class TicketSaleMap : ClassMap<Ticket>
    {
        public TicketSaleMap()
        {
            Table("TicketSales");
            Map(x => x.Id).Not.Nullable();
            Map(x => x.EventId).Not.Nullable();
            Map(x => x.UserId).Not.Nullable();
            Map(x => x.PurchaseDate).Not.Nullable();
            Map(x => x.PriceInCents).Not.Nullable();
            References(x => x.EventId, "EventId")
                .Not.Nullable()
                .ForeignKey("FK_TicketSales_Events_EventId")
                .Cascade.Delete(); 
        }
    }
}