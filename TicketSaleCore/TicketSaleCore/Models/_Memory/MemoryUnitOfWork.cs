using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Memory
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        DbSet<Order> orders;
        DbSet<City> citys;
        DbSet<Event> events;
        DbSet<EventsType> eventsTypes;
        DbSet<OrderStatus> orderStatuses;
        DbSet<Ticket> tickets;
        DbSet<TicketsOrder> ticketsOrders;
        DbSet<Venue> venues;
        DbSet<AppRole> appRoles;
        DbSet<AppUser> appUsers;



        public MemoryUnitOfWork()
        {
            orders = new MemoryGenericRepository<Order>();
            citys = new MemoryGenericRepository<City>();
            events = new MemoryGenericRepository<Event>();
            eventsTypes = new MemoryGenericRepository<EventsType>();
            orderStatuses = new MemoryGenericRepository<OrderStatus>();
            tickets = new MemoryGenericRepository<Ticket>();
            ticketsOrders = new MemoryGenericRepository<TicketsOrder>();
            venues = new MemoryGenericRepository<Venue>();
            appRoles = new MemoryGenericRepository<AppRole>();
            appUsers = new MemoryGenericRepository<AppUser>();




        }

        public DbSet<City> Citys => citys ?? (citys = new MemoryGenericRepository<City>());

        public DbSet<Event> Events => events ?? (events = new MemoryGenericRepository<Event>());

        public DbSet<EventsType> EventsTypes => eventsTypes ?? (eventsTypes = new MemoryGenericRepository<EventsType>());

        public DbSet<Order> Orders => orders ?? (orders = new MemoryGenericRepository<Order>());

        public DbSet<OrderStatus> OrderStatuses => orderStatuses ?? (orderStatuses = new MemoryGenericRepository<OrderStatus>());

        public DbSet<Ticket> Tickets => tickets ?? (tickets = new MemoryGenericRepository<Ticket>());

        public DbSet<TicketsOrder> TicketsOrders => ticketsOrders ?? (ticketsOrders = new MemoryGenericRepository<TicketsOrder>());

        public DbSet<Venue> Venues => venues ?? (venues = new MemoryGenericRepository<Venue>());

        public DbSet<AppRole> AppRoles => appRoles ?? (appRoles = new MemoryGenericRepository<AppRole>());

        public DbSet<AppUser> AppUsers => appUsers ?? (appUsers = new MemoryGenericRepository<AppUser>());

        public void SaveChanges()
        {
        }

    }
}
