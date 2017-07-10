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
    public class EventTypeService : IEventTypeService
    {
        private IUnitOfWork Context
        {
            get;
        }
        public EventTypeService(IUnitOfWork context)
        {
            this.Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();
        }
        public EventsType Get(int? id)
        {
            return
                Context.EventsTypes
                    .Include(p => p.Events)
                    .SingleOrDefault(m => m.Id == id);
        }
        public EventsType Get(string name)
        {
            return
                Context.EventsTypes
                    .Include(p => p.Events)
                    .SingleOrDefault(m => m.NameEventsType == name);
        }
        public IEnumerable<EventsType> GetAll()
        {
            return Context.EventsTypes.Include(p => p.Events);
        }


        public EventsType Add(EventsType entity)
        {
            if (!IsExists(entity.NameEventsType))
            {
                Context.EventsTypes.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This EventsType {entity.NameEventsType} is alredy exist", "alredy exist");
            }
            return entity;
        }

        public bool Delete(EventsType entity)
        {
            if (this.IsExists(entity.Id))
            {

                if (entity.Events.Count != 0)
                {
                    throw new BllValidationException($"This EventsType {entity.NameEventsType} cannot delete" +
                                                     $" form DB because need cascade delete", "Need cascade");
                }
                else
                {
                    var ci = Context.EventsTypes.Remove(entity);
                    Context.SaveChanges();
                    if (ci != null)
                    {
                        return true;
                    }
                }

            }
            else
            {
                throw new BllValidationException($"This EventsTypeCity {entity.NameEventsType} " +
                                                 $"cannot delete form DB because is not exist", "");
            }
            return false;
        }



        public bool IsExists(int id)
        {
            return Context.EventsTypes.Any(e => e.Id == id);
        }

        public bool IsExists(string name)
        {
            return Context.EventsTypes.Any(e => e.NameEventsType == name);
        }

        public EventsType Update(EventsType entity)
        {
            if (IsExists(entity.Id))
            {
                var updateEentitty = this.Get(entity.Id);
                updateEentitty.NameEventsType = entity.NameEventsType;
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This EventsType name:{entity.NameEventsType},Id:{entity.Id} cannot Update" +
                                                 $" in DB because is not exist", "");
            }
            return entity;
        }
    }
}
