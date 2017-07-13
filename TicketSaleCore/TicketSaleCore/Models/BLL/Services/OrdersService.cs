namespace TicketSaleCore.Models.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Infrastructure;
    using Interfaces;
    using DAL.IRepository;
    using Entities;

    /// <summary>
    /// The orders service.
    /// </summary>
    public class OrdersService : IOrdersService
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        private  IUnitOfWork context;

        private  IOrderStatusService orderStatusService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersService"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public OrdersService(IUnitOfWork context,IOrderStatusService orderStatusService)
        {
            this.orderStatusService = orderStatusService;
            this.context = context;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }

        /// <summary>
        /// The get Order by unique identifier
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Order"/>.
        /// </returns>
        public Order Get(int? id)
        {
            return context.Orders
                .Include(o => o.Buyer)
                .SingleOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// The get all Orders id DB
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Order> GetAll()
        {
            return context.Orders.Include(o => o.Buyer)
                .Include(st => st.Status)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Event)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Seller);
        }

        /// <summary>
        /// The get user orders by buyer id
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The Order <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Order> GetUserOrders(string id)
        {

            return context.Orders.Include(o => o.Buyer)
                .Include(st => st.Status)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Event)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Seller)
                .Where(s => s.Buyer.Id.Equals(id));
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="BllValidationException">
        /// </exception>
        public bool Delete(Order entity)
        {
            if (IsExists(entity.Id))
            {
                // if (entity.OrderTickets.Count != 0)
                // {
                // throw new BllValidationException($"This Order {entity.TrackNo} cannot delete" +
                // $" form DB because need cascade delete", "Need cascade");
                // }
                // else
                // {
                // context.Entry(item).State = EntityState.Deleted
                context.Orders.Remove(entity).State = EntityState.Deleted;
                return Convert.ToBoolean(context.SaveChanges());
            }
            else
            {
                throw new BllValidationException($"This Venue {entity.TrackNo} cannot delete form DB because is not exist", string.Empty);
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Order"/>.
        /// </returns>
        /// <exception cref="BllValidationException">
        /// </exception>
        public Order Update(Order entity)
        {
            if (IsExists(entity.Id))
            {
                // var updateEntity = Get(entity.Id);

                //  updateEntity = entity;
                context.Orders.Update(entity).State = EntityState.Modified;

                //  Context.Context.Entry(entity).State = EntityState.Modified;
                //  updateEentitty.Name = entity.Name;
                context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($" cannot Update" + $" in DB because is not exist", string.Empty);
            }
            return entity;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Order"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Order Add(Order entity)
        {
            if (!IsExists(entity.TrackNo))
            {
                context.Orders.Add(entity).State=EntityState.Added;
                context.SaveChanges();
            }
            else
            {
                //throw new BllValidationException($"This City {entity.Name} is alredy exist", "alredy exist");
            }
            return entity;
        }

        public Order NewOrderWithTicket(IEnumerable<Ticket> orderingTickets)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> NewOrderWithTickets(IEnumerable<Ticket> orderingTickets)
        {
            List<Order> newOrders = new List<Order>(); // new order

            // dictionary for Seller and his tickets
            var ticketBySeller = new Dictionary<AppUser, List<Ticket>>();

            // get orderingTickets as List<Ticket>
            var orderingTicketsEnumerable =
                orderingTickets as IList<Ticket>
                ?? orderingTickets.ToList();

            // find all uniqe seller in Ticket list
            var sellers = orderingTicketsEnumerable.Select(p => p.Seller).Distinct().ToList();
            // GroupBy(p => p).ToList();

            var buyer = sellers.First(); // Test buyer

            var sel = sellers.Remove(buyer);

            // make seller ticket dictionary group by Seller 
            foreach (var seller in sellers)
            {
                ticketBySeller.Add(seller, new List<Ticket>());
            }

            foreach (var ticket in orderingTicketsEnumerable)
            {
                // find ticket seller
                var sellerT = ticket.Seller;

                //check current buyer cannnot sell and buy himself tickets
                if (!Equals(ticket.Seller, buyer))
                {
                    //check selling ticket
                    if (ticket.Order == null)
                    {
                        ticketBySeller[sellerT].Add(ticket); // add current ticket to equal seller
                    }
                    else
                    {
                        // ticket cannot be a sold
                        if (ticket.Order.Status.StatusName != ("Confirmed"/*nameof(TicketStatus.Sold)*/))
                        {
                            ticketBySeller[sellerT].Add(ticket);
                        }
                    }
                }
            }
            foreach (var ti in ticketBySeller)
            {
                var order = new Order
                {
                    Buyer = buyer,
                    Status = orderStatusService.Get("Waiting for conformation"/*nameof(TicketStatus.WaitingConfomition)*/),
                    OrderTickets = ti.Value,
                    TrackNo = $"Buyer :{buyer.FirstName}, " +
                              $"Seller:{ti.Key.FirstName}"
                };

                this.Add(order);
                newOrders.Add(order);   
            }
            return newOrders;
        }

        public bool IsExists(int id)
        {
            return context.Orders.Any(e => e.Id == id);
        }
        public Order Get(string name)
        {
            return context.Orders
                .Include(o => o.Buyer)
                .SingleOrDefault(m => m.TrackNo == name);
        }
        public bool IsExists(string name)
        {
            return context.Orders.Any(e => e.TrackNo == name);
        }
    }
}
