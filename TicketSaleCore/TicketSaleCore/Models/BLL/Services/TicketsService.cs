using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Models.BLL.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnitOfWork context;
        public TicketsService(
            IUnitOfWork context)
        {
            this.context = context;
        }
        public IEnumerable<Ticket> GetAllTicket()
        {
            var ticket = context.Tickets;
            return ticket;
        }
        public TicketIndexViewModel GetTicketByEvent(int? _eventId)
        {
            TicketIndexViewModel ticketsVM = new TicketIndexViewModel();
            if(_eventId != null)
            {


                ticketsVM.AvailableTicketsToSale = context.Tickets
                    .Where(p => p.EventId == _eventId)
                    .Where(p => p.Order == null)
                    .Include(t => t.Event)
                    .Include(t => t.Seller);

                ticketsVM. CurentEvent = context.Events
                    .Include(p => p.Venue)
                    .ThenInclude(z => z.City)
                    .First(p => p.Id == _eventId);
            }
            else
            {
                ticketsVM. AvailableTicketsToSale = context.Tickets.Include(t => t.Event).Include(t => t.Seller);
                ticketsVM. CurentEvent = null;
            }
            return ticketsVM;
        }
        public Ticket GetTicket(int? ticketId)
        {
            var ticket = context.Tickets
               .Include(t => t.Event)
               .Include(t => t.Seller)
               .SingleOrDefault(m => m.Id == ticketId);
            return ticket;
        }
        public bool TicketExists(int id)
        {
            return context.Tickets.Any(e => e.Id == id);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
