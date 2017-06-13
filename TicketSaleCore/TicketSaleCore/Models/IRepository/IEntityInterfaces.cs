using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.IdentityWithoutEF;

namespace TicketSaleCore.Models.IRepository
{
    public interface ICityRepository : IRepository
    {
        IEnumerable<City> All();
    }

    public interface IEventRepository : IRepository
    {
        IEnumerable<Event> All();
    }

    public interface IOrderRepository : IRepository
    {
        IEnumerable<Order> All();
    }

    public interface ITicketRepository : IRepository
    {
        IEnumerable<Ticket> All();
    }

    public interface IVenueRepository : IRepository
    {
        IEnumerable<Venue> All();
    }

    public interface IStatusRepository : IRepository
    {
        IEnumerable<OrderStatus> All();
    }

    public interface IEventsTypeRepository : IRepository
    {
        IEnumerable<EventsType> All();
    }

    public interface IAppRoleRepository : IRepository
    {
        IEnumerable<AppRole> All();
    }

    public interface IAppUserRepository : IRepository
    {
        IEnumerable<AppUser> All();
    }
}
