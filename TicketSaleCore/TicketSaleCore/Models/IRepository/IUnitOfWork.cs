using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.IdentityWithoutEF;

namespace TicketSaleCore.Models.IRepository
{
    public interface IUnitOfWork
    {

        DbSet<City> Citys { get; }//1
        DbSet<Event> Events { get;  }//2
        DbSet<EventsType> EventsTypes { get; }//3
        DbSet<Order> Orders { get;  }//4
        DbSet<OrderStatus> OrderStatuses { get; }//5
        DbSet<Ticket> Tickets { get; }//6
        DbSet<TicketsOrder> TicketsOrders { get;  }//7
        DbSet<Venue> Venues { get;  }//8
        DbSet<AppRole> AppRoles { get; }//9
        DbSet<AppUser> AppUsers { get;  }//10

        int SaveChanges();

    }
}
