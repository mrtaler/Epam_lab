using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.ViewModels
{
    public class TicketIndexViewModel
    {
      public  IEnumerable<Ticket> AvailableTicketsToSale { get; set; }
        public Event CurentEvent { get; set; }

        public TicketIndexViewModel(IUnitOfWork context,int? id )
        {
            if (id != null)
            {


                AvailableTicketsToSale = context.Tickets
                    .Where(p => p.EventId == id)
                    .Where(p => p.Order == null)
                    .Include(t => t.Event)
                    .Include(t => t.Seller);

                CurentEvent = context.Events
                    .Include(p => p.Venue)
                    .ThenInclude(z => z.City)
                    .First(p => p.Id == id);
            }
            else
            {
                AvailableTicketsToSale = context.Tickets.Include(t => t.Event).Include(t => t.Seller);
                CurentEvent = null;
            }
        }

    }
}
