using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private IUnitOfWork Context
        {
            get;
        }
        public OrderStatusService(IUnitOfWork context)
        {
            this.Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public OrderStatus Get(int? id)
        {
            return this.GetAll().SingleOrDefault(m => m.Id == id);
        }
        public IEnumerable<OrderStatus> GetAll()
        {
            return Context.OrderStatuses;
        }

        public OrderStatus Add(OrderStatus entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(OrderStatus entity)
        {
            throw new NotImplementedException();
        }
        public OrderStatus Update(OrderStatus entity)
        {
            throw new NotImplementedException();
        }
    }
}
