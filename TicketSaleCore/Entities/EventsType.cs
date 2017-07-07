using System.Collections.Generic;

namespace Entities
{
    public class EventsType
    {
        public int Id { get; set; }
        public string NameEventsType { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
