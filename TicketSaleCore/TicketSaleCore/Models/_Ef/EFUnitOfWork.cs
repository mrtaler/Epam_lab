using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Ef
{
    //public class EFUnitOfWork : ApplicationContext, IUnitOfWork
    //{
    //    DbContext context = null;
    //    IRepository<Order> orders;
    //    IRepository<City> citys;
    //    IRepository<Event> events;
    //    IRepository<EventsType> eventsTypes;
    //    IRepository<OrderStatus> orderStatuses;
    //    IRepository<Ticket> tickets;
    //    IRepository<TicketsOrder> ticketsOrders;
    //    IRepository<Venue> venues;
    //    IRepository<AppRole> appRoles;
    //    IRepository<AppUser> appUsers;

    //    public EFUnitOfWork(DbContextOptions<ApplicationContext> options) :base(options)  
    //    {
            
    //        this.context = new ApplicationContext(options);
    //    }

    //    //public EFUnitOfWork(DbContext context)
    //    //{
    //    //    if (context == null)
    //    //    {
    //    //        throw new ArgumentNullException("Context was not supplied");
    //    //    }
    //    //    this.context = context;
    //    //}

    //    public IRepository<Order> Orders => orders ?? (orders = new EFGenericRepository<Order>(context));

    //    public IRepository<City> Citys => citys ?? (citys = new EFGenericRepository<City>(context));

    //    public IRepository<Event> Events => events ?? (events = new EFGenericRepository<Event>(context));

    //    public IRepository<EventsType> EventsTypes => eventsTypes ?? (eventsTypes = new EFGenericRepository<EventsType>(context));

    //    public IRepository<OrderStatus> OrderStatuses => orderStatuses ?? (orderStatuses = new EFGenericRepository<OrderStatus>(context));

    //    public IRepository<Ticket> Tickets => tickets ?? (tickets = new EFGenericRepository<Ticket>(context));

    //    public IRepository<TicketsOrder> TicketsOrders => ticketsOrders ?? (ticketsOrders = new EFGenericRepository<TicketsOrder>(context));

    //    public IRepository<Venue> Venues => venues ?? (venues = new EFGenericRepository<Venue>(context));

    //    public IRepository<AppRole> AppRoles => appRoles ?? (appRoles = new EFGenericRepository<AppRole>(context));

    //    public IRepository<AppUser> AppUsers => appUsers ?? (appUsers = new EFGenericRepository<AppUser>(context));


    //    public void SaveChanged()
    //    {
    //        context.SaveChanges();
    //    }

    //}
}
