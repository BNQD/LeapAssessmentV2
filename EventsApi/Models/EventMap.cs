using EventsApi.Models;
using FluentNHibernate.Mapping;

namespace EventsApi.Models
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Table("Events"); // Table name
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Nullable();
            Map(x => x.StartsOn).Not.Nullable();
            Map(x => x.EndsOn).Not.Nullable();
            Map(x => x.Location).Not.Nullable();
        }
    }
}