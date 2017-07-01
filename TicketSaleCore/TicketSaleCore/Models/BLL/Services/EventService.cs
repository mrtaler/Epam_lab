using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.DTO;
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
            get; set;
        }
        public EventService(IUnitOfWork context)
        {
            this.Context = context;
        }


        public EventsType GetEventsType(int? id)
        {
            if(id == null)
                throw new ValidationException("Не установлено id", "");
            var eventType = Context.EventsTypes.Find(id.Value);
            if(eventType == null)
                throw new ValidationException("eventType не найден", "");
            // применяем автомаппер для проекции Phone на PhoneDTO
           // Mapper.Initialize(cfg => cfg.CreateMap<EventsType, EventsTypeDTO>());
            return eventType;
                /*Mapper.Map<EventsType, EventsTypeDTO>(eventType);*/
        }
        public IEnumerable<Ticket> GetTickets(Event _event)
        {
            var Tickets = Context.Tickets;
            //  Mapper.Initialize(cfg => cfg.CreateMap<Ticket, TicketsDTO>());

            return Tickets.Where(p => p.Event.Id == _event.Id);
            //Mapper.Map<IEnumerable<Ticket>, List<TicketsDTO>>(Context.Tickets.Where(p => p.Event.Id == events.Id));
        }
        public IEnumerable<Event> GetEvents()
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<Event, EventsDTO>());
         //   return Mapper.Map<IEnumerable<Event>, List<EventsDTO>>(
            return (
                Context.Events
                .Include(p => p.Tickets)
                .ThenInclude(e => e.Order)
                .Include(p => p.Venue)
                .ThenInclude(c => c.City)
                .OrderBy(t => t.Date)
                
                );
            //.Where(t => t.Tickets.Count(c => c.Order == null) > 0)
            //.Where(t=>t.Date>=DateTime.UtcNow)





            //     );
        }
        public void MakeEvent(Event _event)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
