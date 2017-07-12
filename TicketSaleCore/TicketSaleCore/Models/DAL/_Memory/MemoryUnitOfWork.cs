using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.DAL._Memory
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        private DbSet<Order> orders;
        private DbSet<City> citys;
        private DbSet<Event> events;
        private DbSet<EventsType> eventsTypes;
        private DbSet<OrderStatus> orderStatuses;
        private DbSet<Ticket> tickets;
        private DbSet<Venue> venues;

        private DbSet<AppUser> appUsers;

        #region Identity
        private DbSet<IdentityUserClaim<string>> userClaims;
        private DbSet<IdentityUserLogin<string>> userLogins;
        private DbSet<IdentityUserRole<string>> userRoles;
        private DbSet<IdentityUserToken<string>> userTokens;
        private DbSet<IdentityRole> roles;
        private DbSet<IdentityRoleClaim<string>> roleClaims;
        #endregion



        public MemoryUnitOfWork()
        {
            orders = new MemoryGenericRepository<Order>();
            citys = new MemoryGenericRepository<City>();
            events = new MemoryGenericRepository<Event>();
            eventsTypes = new MemoryGenericRepository<EventsType>();
            orderStatuses = new MemoryGenericRepository<OrderStatus>();
            tickets = new MemoryGenericRepository<Ticket>();
            venues = new MemoryGenericRepository<Venue>();

            appUsers = new MemoryGenericRepository<AppUser>();

            #region Identity

            userClaims = new MemoryGenericRepository<IdentityUserClaim<string>>();
            userLogins = new MemoryGenericRepository<IdentityUserLogin<string>>();
            userRoles = new MemoryGenericRepository<IdentityUserRole<string>>();
            userTokens = new MemoryGenericRepository<IdentityUserToken<string>>();
            roles = new MemoryGenericRepository<IdentityRole>();
            roleClaims = new MemoryGenericRepository<IdentityRoleClaim<string>>();

            #endregion

        }

        public DbSet<City> Citys => citys ?? (citys = new MemoryGenericRepository<City>());
        public DbSet<Event> Events => events ?? (events = new MemoryGenericRepository<Event>());
        public DbSet<EventsType> EventsTypes => eventsTypes ?? (eventsTypes = new MemoryGenericRepository<EventsType>());
        public DbSet<Order> Orders => orders ?? (orders = new MemoryGenericRepository<Order>());
        public DbSet<OrderStatus> OrderStatuses => orderStatuses ?? (orderStatuses = new MemoryGenericRepository<OrderStatus>());
        public DbSet<Ticket> Tickets => tickets ?? (tickets = new MemoryGenericRepository<Ticket>());
        public DbSet<Venue> Venues => venues ?? (venues = new MemoryGenericRepository<Venue>());

        public DbSet<AppUser> AppUsers => appUsers ?? (appUsers = new MemoryGenericRepository<AppUser>());
        #region Identity
        public DbSet<IdentityUserClaim<string>> UserClaims => userClaims ?? (userClaims = new MemoryGenericRepository<IdentityUserClaim<string>>());
        public DbSet<IdentityUserLogin<string>> UserLogins => userLogins ?? (userLogins = new MemoryGenericRepository<IdentityUserLogin<string>>());
        public DbSet<IdentityUserRole<string>> UserRoles => userRoles ?? (userRoles = new MemoryGenericRepository<IdentityUserRole<string>>());
        public DbSet<IdentityUserToken<string>> UserTokens => userTokens ?? (userTokens = new MemoryGenericRepository<IdentityUserToken<string>>());
        public DbSet<IdentityRole> Roles => roles ?? (roles = new MemoryGenericRepository<IdentityRole>());
        public DbSet<IdentityRoleClaim<string>> RoleClaims => roleClaims ?? (roleClaims = new MemoryGenericRepository<IdentityRoleClaim<string>>());

        public DbContext Context => throw new NotImplementedException();
        #endregion


        public int SaveChanges()
        {
            return 1;
        }

        public void Dispose()
        {
        }
    }
}
