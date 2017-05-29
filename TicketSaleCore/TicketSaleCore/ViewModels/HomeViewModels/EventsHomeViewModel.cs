using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models;

namespace TicketSaleCore.ViewModels.HomeViewModels
{
    public class EventsHomeViewModel : Event
    {
        public EventsHomeViewModel(Event ev)
            :base(ev)
        {

            AvailableTicket = ev.Tickets.Count(p => p.Order==null );
            
            //+ev.Tickets.Where(z => z.TicketsOrders != null).Count(z => z.TicketsOrders.Status.StatusName.Equals("Rejected"));
        }
        public int AvailableTicket { get; set; }
    }
}
