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
            throw new NotImplementedException();
        }
        public Order Add(Order entity)
        {
            throw new NotImplementedException();
        }
        public Order Update(Order entity)
        {
            throw new NotImplementedException();
        }

    }
}
