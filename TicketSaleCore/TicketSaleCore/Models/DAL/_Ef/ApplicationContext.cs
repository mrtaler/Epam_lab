using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

/*this EF G Rep (maybe)
 */
namespace TicketSaleCore.Models.DAL._Ef
{
    public class ApplicationContext : IdentityDbContext<AppUser>, IUnitOfWork
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
          
        }

        

        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventsType> EventsTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }

        public DbContext Context => this;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region City
            modelBuilder
                    .Entity<City>().HasKey(t => t.Id);
            #endregion

            #region Event
            modelBuilder.Entity<Event>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Event>()
                .HasOne(t => t.Venue)
                .WithMany(t => t.Events)
                .HasForeignKey(t => t.VenueId);
            modelBuilder.Entity<Event>()
                .HasOne(t => t.EventsType)
                .WithMany(t => t.Events)
                .HasForeignKey(t => t.EventsTypeId);
            #endregion

            #region Order
            modelBuilder.Entity<Order>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.Buyer)
                .WithMany(t => t.Orders)
                .HasForeignKey(t => t.BuyerId);

            modelBuilder.Entity<Order>()
                .HasMany(t => t.OrderTickets)
                .WithOne(t => t.Order)
                .HasForeignKey(еt => еt.OrderId);
            #endregion

            #region Status
            modelBuilder.Entity<OrderStatus>().HasKey(t => t.Id);

            modelBuilder.Entity<OrderStatus>().HasMany(t => t.Orders)
                .WithOne(t => t.Status);
            #endregion

            #region Ticket
            modelBuilder.Entity<Ticket>().HasKey(t => t.Id);

            modelBuilder.Entity<Ticket>().HasOne(t => t.Event)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.EventId);

            modelBuilder.Entity<Ticket>().HasOne(t => t.Seller)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.SellerId);



            #endregion

            #region Venue

            modelBuilder.Entity<Venue>().HasKey(t => t.Id);

            modelBuilder.Entity<Venue>()
                .HasOne(t => t.City)
                .WithMany(t => t.Venues)
                .HasForeignKey(t => t.CityFk);
            #endregion

            #region next feature
            //modelBuilder.AddConfiguration(new CityConfiguration());
            // modelBuilder.AddConfiguration(new EventConfiguration());
            // modelBuilder.AddConfiguration(new OrderConfiguration());
            // modelBuilder.AddConfiguration(new TicketConfiguration());
            // modelBuilder.AddConfiguration(new UserConfiguration());
            // modelBuilder.AddConfiguration(new VenueConfiguration());
            // modelBuilder.AddConfiguration(new StatusConfiguration());.
            #endregion
        }

        
    }
}
