﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.ViewModels
{
    public class TicketIndexViewModel
    {
      public  IEnumerable<Ticket> availableTicketsToSale { get; set; }
        public Event CurentEvent { get; set; }

        public TicketIndexViewModel(IUnitOfWork context,int? id )
        {
            if (id != null)
            {


                availableTicketsToSale = context.Tickets
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
                availableTicketsToSale = context.Tickets.Include(t => t.Event).Include(t => t.Seller);
                CurentEvent = null;
            }
        }

    }
}
