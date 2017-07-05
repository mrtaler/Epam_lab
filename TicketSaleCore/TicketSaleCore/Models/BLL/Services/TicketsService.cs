﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TicketSaleCore.Features.Tickets.Tickets.ViewModels;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnitOfWork context;
        public TicketsService(
            IUnitOfWork context
            )
        {
            this.context = context;
        }


        public IEnumerable<Ticket> GetAllTicket()
        {
            var ticket = context.Tickets;
            return ticket;
        }
        public TicketIndexViewModel GetTicketByEvent(int? eventId)
        {
            TicketIndexViewModel ticketsVm = new TicketIndexViewModel();
            if(eventId != null)
            {


                ticketsVm.AvailableTicketsToSale = context.Tickets
                    .Where(p => p.EventId == eventId)
                    .Where(p => p.Order == null)
                    .Include(t => t.Event)
                    .Include(t => t.Seller);

                ticketsVm. CurentEvent = context.Events
                    .Include(p => p.Venue)
                    .ThenInclude(z => z.City)
                    .First(p => p.Id == eventId);
            }
            else
            {
                ticketsVm. AvailableTicketsToSale = context.Tickets.Include(t => t.Event).Include(t => t.Seller);
                ticketsVm. CurentEvent = null;
            }
            return ticketsVm;
        }

        public IEnumerable<Ticket> GetAllUserTickets(string userId, TicketStatus param)
        {
            List<Ticket> returnTickets=null;
            switch (param)
            {
                case TicketStatus.SellingTickets:
                    returnTickets = new List<Ticket>(
                        context.Tickets
                            .Where(p => p.Seller.Id == userId)
                            .Where(z => z.Order == null)
                            .Include(p => p.Event)
                            .Include(p => p.Order)
                            .Include(p => p.Seller));
                    break;
                case TicketStatus.WaitingConfomition:

                    returnTickets = new List<Ticket>(
                        context.Tickets
                            .Include(p => p.Order)
                            .ThenInclude(p => p.Status)
                            .Include(z => z.Order.Buyer)
                            .Include(p => p.Seller)
                            .Include(p => p.Event)
                            .Where(p => p.Seller.Id == userId)
                            .Where(p => p.Order.Status.StatusName == "Waiting for conformation"));
                    break;

                case TicketStatus.Sold:
                    returnTickets = new List<Ticket>(
                        context.Tickets
                            .Include(p => p.Order)
                            .ThenInclude(p => p.Status)
                            .Include(z => z.Order.Buyer)
                            .Include(p => p.Seller)
                            .Include(p => p.Event)
                            .Where(p => p.Seller.Id == userId)
                            .Where(p => p.Order.Status.StatusName == "Confirmed"));
                    break;
            }
            return returnTickets;
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

    public enum TicketStatus
    {
        SellingTickets,
        WaitingConfomition,
        Sold
    }
}
