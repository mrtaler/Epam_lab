using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string EventTypeName { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
