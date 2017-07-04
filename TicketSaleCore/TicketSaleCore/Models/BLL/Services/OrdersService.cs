using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class OrdersService : IOrdersService, IOrderStatusService
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

        public Order GetOrder(int? id)
        {
            return Context.Orders
                .Include(o => o.Buyer)
                .SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return Context.Orders.Include(o => o.Buyer)
                //  
                .Include(st => st.Status)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Event)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Seller);
        }
        public IEnumerable<Order> GetUserOrders(string id)
        {
            return this.GetOrders().Where(s => s.Buyer.Id.Equals(id));
        }




        public OrderStatus GetOrderStatus(int? id)
        {
           return this.GetOrderStatuses().SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<OrderStatus> GetOrderStatuses()
        {
            return Context.OrderStatuses;
        }
    }
}
