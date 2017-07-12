using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        private IUnitOfWork Context
        {
            get;
        }
        public OrdersService(IUnitOfWork context)
        {
            this.Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }


        public Order Get(int? id)
        {
            return Context.Orders
                .Include(o => o.Buyer)
                .SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return Context.Orders.Include(o => o.Buyer)
                //  
                .Include(st => st.Status)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Event)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Seller);
        }
        public IEnumerable<Order> GetUserOrders(string id)
        {
            return this.GetAll().Where(s => s.Buyer.Id.Equals(id));
        }




        public bool Delete(Order entity)
        {
            if (IsExists(entity.Id))
            {
                //if (entity.OrderTickets.Count != 0)
                //{
                //    throw new BllValidationException($"This Order {entity.TrackNo} cannot delete" +
                //                                     $" form DB because need cascade delete", "Need cascade");
                //}
                //else
                //{
                //  context.Entry(item).State = EntityState.Deleted
                Context.Orders.Remove(entity).State = EntityState.Deleted;
                //   Remove(entity);
                return Convert.ToBoolean(Context.SaveChanges());
                //}
            }
            else
            {
                throw new BllValidationException($"This Venue {entity.TrackNo} cannot delete form DB because is not exist", "");
            }
        }
        public Order Update(Order entity)
        {
            if (IsExists(entity.Id))
            {
                // var updateEentitty = Get(entity.Id);

                //  updateEentitty = entity;
                Context.Orders.Update(entity).State = EntityState.Modified;
                //  Context.Context.Entry(entity).State = EntityState.Modified;
                //  updateEentitty.Name = entity.Name;
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($" cannot Update" +
                                                 $" in DB because is not exist", "");
            }
            return entity;
        }



        public Order Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public Order NewOrder(IEnumerable<Ticket> orderingTickets)
        {
            var newOrder = new Order();

            var ticketBySeller = new Dictionary<AppUser, List<Ticket>>();
            var sellers = orderingTickets.Select(p => p.Seller).GroupBy(p => p).ToList();
            var byer = sellers.First();
            var sel = sellers.Remove(byer);
            foreach (var seller in sellers)
            {
                ticketBySeller.Add(seller.Key, new List<Ticket>());
            }

            //ticketBySeller.Add("user1",new List<Ticket>());
            //ticketBySeller.Add("user2", new List<Ticket>());
            //ticketBySeller.Add("user3", new List<Ticket>());

            foreach (var ticket in orderingTickets)
            {
                var sellerT = ticket.Seller;
                if (ticket.Seller != byer)
                {
                    if (ticket.Order == null)
                    {
                        var sellerOr = ticketBySeller[sellerT];
                        sellerOr.Add(ticket);
                    }
                    else
                    {
                        var ss = ticket.Order.Status.StatusName;
                        if (ticket.Order.Status.StatusName != (nameof(TicketStatus.Sold)))
                        {
                            var sellerOr = ticketBySeller[sellerT];
                            sellerOr.Add(ticket);
                        }
                    }
                }
            }

            return newOrder;
        }



        public bool IsExists(int id)
        {
            return Context.Orders.Any(e => e.Id == id);
        }
        public Order Get(string name)
        {
            return Context.Orders
                .Include(o => o.Buyer)
                .SingleOrDefault(m => m.TrackNo == name);
        }
        public bool IsExists(string name)
        {
            return Context.Orders.Any(e => e.TrackNo == name);
        }
    }
}
