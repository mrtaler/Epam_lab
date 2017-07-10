using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Infrastructure;
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
            Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();
        }

      
        public IEnumerable<OrderStatus> GetAll()
        {
            return Context.OrderStatuses
                .Include(p=>p.Orders);
        }

        public OrderStatus Get(int? id)
        {
            return Context.OrderStatuses
                .Include(p => p.Orders)
                .SingleOrDefault(m => m.Id == id);
        }

        public OrderStatus Get(string name)
        {
            return Context.OrderStatuses
                .Include(p => p.Orders)
                .SingleOrDefault(m => m.StatusName == name);
        }
        public OrderStatus Add(OrderStatus entity)
        {
            if (!IsExists(entity.StatusName))
            {
                Context.OrderStatuses.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This OrderStatus {entity.StatusName} is" +
                                                 $" alredy exist", "alredy exist");
            }
            return entity;
        }
        public bool Delete(OrderStatus entity)
        {
            if (IsExists(entity.Id))
            {

                if (entity.Orders.Count != 0)
                {
                    throw new BllValidationException($"This OrderStatus {entity.StatusName} cannot delete" +
                                                     $" form DB because need cascade delete", "Need cascade");
                }
                else
                {
                    var ci = Context.OrderStatuses.Remove(entity);
                    Context.SaveChanges();
                    if (ci != null)
                    {
                        return true;
                    }
                }

            }
            else
            {
                throw new BllValidationException($"This City {entity.StatusName} cannot delete form DB because is not exist", "");
            }

            return false;
        }
        public OrderStatus Update(OrderStatus entity)
        {
            if (IsExists(entity.Id))
            {
                var updateEentitty = Get(entity.Id);
                updateEentitty.StatusName = entity.StatusName;
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This OrderStatus name:{entity.StatusName},Id:{entity.Id} cannot Update" +
                                                 $" in DB because is not exist", "");
            }
            return entity;
        }

        public bool IsExists(int id)
        {
            return Context.OrderStatuses.Any(e => e.Id == id);
        }

        public bool IsExists(string name)
        {
            return Context.OrderStatuses.Any(e => e.StatusName == name);
        }
    }
}
