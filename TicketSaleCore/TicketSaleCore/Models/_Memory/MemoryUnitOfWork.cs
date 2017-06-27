using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Memory
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        private DbSet<Order> orders;
        private DbSet<City> citys;
        private DbSet<Event> events;
        private DbSet<EventsType> eventsTypes;
        private DbSet<OrderStatus> orderStatuses;

        private DbSet<Ticket> tickets;
        //DbSet<TicketsOrder> ticketsOrders;
        private DbSet<Venue> venues;

        private DbSet<IdentityRole> appRoles;
        private DbSet<AppUser> appUsers;



        public MemoryUnitOfWork()
        {
            orders = new MemoryGenericRepository<Order>();
            citys = new MemoryGenericRepository<City>();
            events = new MemoryGenericRepository<Event>();
            eventsTypes = new MemoryGenericRepository<EventsType>();
            orderStatuses = new MemoryGenericRepository<OrderStatus>();
            tickets = new MemoryGenericRepository<Ticket>();
            //ticketsOrders = new MemoryGenericRepository<TicketsOrder>();
            venues = new MemoryGenericRepository<Venue>();
            appRoles = new MemoryGenericRepository<IdentityRole>();
            appUsers = new MemoryGenericRepository<AppUser>();




        }

        public DbSet<City> Citys => citys ?? (citys = new MemoryGenericRepository<City>());

        public DbSet<Event> Events => events ?? (events = new MemoryGenericRepository<Event>());

        public DbSet<EventsType> EventsTypes => eventsTypes ?? (eventsTypes = new MemoryGenericRepository<EventsType>());

        public DbSet<Order> Orders => orders ?? (orders = new MemoryGenericRepository<Order>());

        public DbSet<OrderStatus> OrderStatuses => orderStatuses ?? (orderStatuses = new MemoryGenericRepository<OrderStatus>());

        public DbSet<Ticket> Tickets => tickets ?? (tickets = new MemoryGenericRepository<Ticket>());

        //public DbSet<TicketsOrder> TicketsOrders => ticketsOrders ?? (ticketsOrders = new MemoryGenericRepository<TicketsOrder>());

        public DbSet<Venue> Venues => venues ?? (venues = new MemoryGenericRepository<Venue>());

        public DbSet<IdentityRole> AppRoles => appRoles ?? (appRoles = new MemoryGenericRepository<IdentityRole>());

        public DbSet<AppUser> AppUsers => appUsers ?? (appUsers = new MemoryGenericRepository<AppUser>());

        public int SaveChanges()
        {
            return 1;
        }

    }
}
