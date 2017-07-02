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

        
    }
}
