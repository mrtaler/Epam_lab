using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class EventService : IEventService
    {
        private IUnitOfWork Context
        {
            get;
        }
        public EventService(IUnitOfWork context)
        {
            this.Context = context;
        }

        public Event Get(int? id)
        {
            if (id == null)
                throw new BllValidationException("Not specified id", "");
            var _event = Context.Events.Find(id.Value);
            if (_event == null)
                throw new BllValidationException("eventType not found", "");
            return _event;
        }
        public IEnumerable<Event> GetAll()
        {
            return (
                Context.Events
                .Include(p => p.Tickets)
                .ThenInclude(e => e.Order)
                .Include(p => p.Venue)
                .ThenInclude(c => c.City)
                .OrderBy(t => t.Date)
                );
        }
        public IEnumerable<Event> GetAllEventWithTickets()
        {
            return (
                GetAll()
                .Where(t => t.Tickets.Count(c => c.Order == null) > 0)
                );
        }
        public void Dispose()
        {
            Context.Dispose();
        }
        public bool Delete(Event entity)
        {
            if (IsExists(entity.Id))
            {
                if (entity.Tickets.Count != 0)
                {
                    throw new BllValidationException($"This Event {entity.Name} cannot delete" +
                                                     $" form DB because need cascade delete", "Need cascade");
                }
                else
                {
                    var ci = Context.Events.Remove(entity);
                    Context.SaveChanges();
                    if (ci != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new BllValidationException($"This Event {entity.Name} cannot delete form DB because is not exist", "");
            }
            return false;
        }

        public Event Add(Event entity)
        {
            if (!IsExists(entity.Name))
            {
                Context.Events.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This Event {entity.Name} is alredy exist", "alredy exist");
            }
            return entity;

            //  return Context.Events.Add(entity).Entity;
        }
        public Event Update(Event entity)
        {
            if (IsExists(entity.Id))
            {
               // var updateEentitty = Get(entity.Id);

              //  updateEentitty = entity;
                Context.Events.Update(entity).State= EntityState.Modified;
                //  Context.Context.Entry(entity).State = EntityState.Modified;
                //  updateEentitty.Name = entity.Name;
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This Event name:{entity.Name},Id:{entity.Id} cannot Update" +
                                                  $" in DB because is not exist", "");
            }
            return entity;
        }

        public bool IsExists(int id)
        {
            return Context.Events.Any(e => e.Id == id);
        }

        public bool IsExists(string name)
        {
            return Context.Events.Any(e => e.Name == name);
        }

        public Event Get(string name)
        {
            return Context.Events
                .Include(p => p.Tickets)
                .ThenInclude(e => e.Order)
                .Include(p => p.Venue)
                .ThenInclude(c => c.City)
                .SingleOrDefault(m => m.Name == name);
        }
    }
}
