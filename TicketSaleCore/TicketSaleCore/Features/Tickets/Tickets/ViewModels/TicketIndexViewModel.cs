using System.Collections.Generic;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Features.Tickets.ViewModels
{
    public class TicketIndexViewModel
    {
      public  IEnumerable<Ticket> AvailableTicketsToSale { get; set; }
        public Event CurentEvent { get; set; }

        
    }
}
