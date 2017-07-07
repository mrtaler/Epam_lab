using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.DAL.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        DbSet<City> Citys
        {
            get;
        }//1
        DbSet<Event> Events
        {
            get;
        }//2
        DbSet<EventsType> EventsTypes
        {
            get;
        }//3
        DbSet<Order> Orders
        {
            get;
        }//4
        DbSet<OrderStatus> OrderStatuses
        {
            get;
        }//5
        DbSet<Ticket> Tickets
        {
            get;
        }//6
        DbSet<Venue> Venues
        {
            get;
        }//7
        DbSet<AppUser> AppUsers
        {
            get;
        }//9

        DbSet<IdentityUserClaim<string>> UserClaims
        {
            get;
        }

        DbSet<IdentityUserLogin<string>> UserLogins
        {
            get;
        }

        DbSet<IdentityUserRole<string>> UserRoles
        {
            get;
        }

        DbSet<IdentityUserToken<string>> UserTokens
        {
            get;
        }

        DbSet<IdentityRole> Roles
        {
            get;
        }

        DbSet<IdentityRoleClaim<string>> RoleClaims
        {
            get;
        }


        int SaveChanges();

    }
}
