using System;
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
        private readonly IUnitOfWork context;
        public EventService(IUnitOfWork context)
        {
            this.context = context;
        }

        public Event Get(int? id)
        {
            if(id == null)
                throw new ValidationException("Not specified id", "");
            var _event = context.Events.Find(id.Value);
            if(_event == null)
                throw new ValidationException("eventType not found", "");
            return _event;
        }
        public IEnumerable<Event> GetAll()
        {
            return (
                context.Events
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
                this.GetAll()
                .Where(t => t.Tickets.Count(c => c.Order == null) > 0)
                );
        }

        public void Dispose()
        {
            context.Dispose();
        }
        public bool Delete(Event entity)
        {
            try
            {
                context.Events.Remove(entity);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        public Event Add(Event entity)
        {
            return context.Events.Add(entity).Entity;
        }
        public Event Update(Event entity)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
