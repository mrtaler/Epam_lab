using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.DBConfig;

namespace TicketSaleCore.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public virtual DbSet<City> CityDbSet { get; set; }
        public virtual DbSet<Event> EventDbSet { get; set; }
        public virtual DbSet<Order> OrderDbSet { get; set; }
        public virtual DbSet<Ticket> TicketDbSet { get; set; }
        public virtual DbSet<Venue> VenueDbSet { get; set; }
        public virtual DbSet<Status> StatusDbSet { get; set; }

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
            #endregion

            #region Order
            modelBuilder.Entity<Order>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.Buyer)
                .WithMany(t => t.Orders)
                .HasForeignKey(t => t.BuyerId);
            #endregion

            #region Status
            modelBuilder.Entity<Status>().HasKey(t => t.Id);

            modelBuilder.Entity<Status>().HasMany(t => t.Orders)
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

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Order)
                .WithOne(tt => tt.Ticket)
                .HasForeignKey<Order>(z=>z.TicketFk);

            #endregion

            #region Venue

            modelBuilder.Entity<Venue>().HasKey(t => t.Id);

            modelBuilder.Entity<Venue>()
                .HasOne(t => t.City)
                .WithMany(t => t.Venues)
                .HasForeignKey(t => t.CityFk);
            #endregion


            //modelBuilder.AddConfiguration(new CityConfiguration());
            // modelBuilder.AddConfiguration(new EventConfiguration());
            // modelBuilder.AddConfiguration(new OrderConfiguration());
            // modelBuilder.AddConfiguration(new TicketConfiguration());
            // modelBuilder.AddConfiguration(new UserConfiguration());
            // modelBuilder.AddConfiguration(new VenueConfiguration());
            // modelBuilder.AddConfiguration(new StatusConfiguration());
        }
    }
}
