using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class EventsType
    {
        public int Id { get; set; }
        public string NameEventsType { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
