using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketSaleCore.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
        public DbSet<Event> EventsDbSet { get; set; }
        public DbSet<EventType> EventTypesDbSet { get; set; }
        public DbSet<Venue> VenueDbSet { get; set; }
        public DbSet<City> CityDbSet { get; set; }
    }
}
