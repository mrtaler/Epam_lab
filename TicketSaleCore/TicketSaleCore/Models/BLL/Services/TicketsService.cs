using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TicketSaleCore.Features.Tickets.Tickets.ViewModels;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;
using System;
using TicketSaleCore.Models.BLL.Infrastructure;
using static TicketSaleCore.Models.BLL.Infrastructure.TicketStatusEnum;

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

        public void Dispose()
        {
            context.Dispose();
        }

        public Ticket Get(string name)
        {
            var ticket = context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Seller)
                .SingleOrDefault(m => m.SellerNotes == name);
            return ticket;
        }
        public Ticket Get(int? ticketId)
        {
            var ticket = context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Seller)
                .SingleOrDefault(m => m.Id == ticketId);
            return ticket;
        }

        public IEnumerable<Ticket> GetAll()
        {
            var tickets = context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Seller);
            return tickets;
        }

        public TicketIndexViewModel GetTicketByEvent(int? eventId)
        {
            TicketIndexViewModel ticketsVm = new TicketIndexViewModel();
            if (eventId != null)
            {


                ticketsVm.AvailableTicketsToSale = context.Tickets
                    .Where(p => p.EventId == eventId)
                    .Where(p => p.Order == null)
                    .Include(t => t.Event)
                    .Include(t => t.Seller);

                ticketsVm.CurentEvent = context.Events
                    .Include(p => p.Venue)
                    .ThenInclude(z => z.City)
                    .First(p => p.Id == eventId);
            }
            else
            {
                ticketsVm.AvailableTicketsToSale = context.Tickets.Include(t => t.Event).Include(t => t.Seller);
                ticketsVm.CurentEvent = null;
            }
            return ticketsVm;
        }

        public IEnumerable<Ticket> GetAllUserTickets(string userId, TicketStatus param)
        {
            List<Ticket> returnTickets = null;
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


        public bool IsExists(int id)
        {
            return context.Tickets.Any(e => e.Id == id);
        }
        public bool IsExists(string name)
        {
            return context.Tickets.Any(e => e.SellerNotes == name);
        }



        public bool Delete(Ticket entity)
        {
            context.Tickets.Remove(entity).State = EntityState.Deleted;

            return Convert.ToBoolean(context.SaveChanges());
        }

        public Ticket Add(Ticket entity)
        {
            context.Tickets.Add(entity).State = EntityState.Added;
            context.SaveChanges();
            return entity;
        }
        public Ticket Update(Ticket entity)
        {
            if (IsExists(entity.Id))
            {
                // var updateEentitty = Get(entity.Id);

                //  updateEentitty = entity;
                context.Tickets.Update(entity).State = EntityState.Modified;
                //  Context.Context.Entry(entity).State = EntityState.Modified;
                //  updateEentitty.Name = entity.Name;
                context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This Ticket name:{entity.SellerNotes},Id:{entity.Id} cannot Update" +
                                                 $" in DB because is not exist", "");
            }
            return entity;
        }
    }


}
