using System;
using System.Collections.Generic;
using System.Linq;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Memory
{
    public class CityRepository : ICityRepository
    {
        public readonly IList<City> CityItems;

        public CityRepository()
        {
            CityItems = new List<City>();

            var cityMinsk = new City { Id = 1, Name = "Minsk" };
            var cityGomel = new City { Id = 2, Name = "Gomel" };
            var cityGrodno = new City { Id = 3, Name = "Grodno" };
            var cityVitebsk = new City { Id = 4, Name = "Vitebsk" };
            var cityBrest = new City { Id = 5, Name = "Brest" };
            var cityMogilev = new City { Id = 6, Name = "Mogilev" };

            CityItems.Add(cityMinsk);
            CityItems.Add(cityGomel);
            CityItems.Add(cityGrodno);
            CityItems.Add(cityVitebsk);
            CityItems.Add(cityBrest);
            CityItems.Add(cityMogilev);

        }

        public void SetStorageContext(IStorageContext storageContext)
        {
        }

        public IEnumerable<City> All()
        {
            return this.CityItems.ToList();
        }
    }

    public class EventRepository : IEventRepository
    {
        public readonly IList<Event> EventItems;
        public IEnumerable<Event> All()
        {
            return this.EventItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class OrderRepository : IOrderRepository
    {
        public readonly IList<Order> OrderItems;
        public IEnumerable<Order> All()
        {
            return this.OrderItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class TicketRepository : ITicketRepository
    {
        public readonly IList<Ticket> TicketItems;
        public IEnumerable<Ticket> All()
        {
            return this.TicketItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class VenueRepository : IVenueRepository
    {
        public readonly IList<Venue> VenueItems;
        public IEnumerable<Venue> All()
        {
            return this.VenueItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class OrderStatusRepository : IStatusRepository
    {
        public readonly IList<OrderStatus> StatusItems;

        public OrderStatusRepository(IList<OrderStatus> statusItems)
        {
            StatusItems = new List<OrderStatus>();

            var statusWaiting = new OrderStatus { StatusName = "Waiting for conformation" };
            var statusConfirmed = new OrderStatus { StatusName = "Confirmed" };
            var statusRejected = new OrderStatus { StatusName = "Rejected" };

            StatusItems.Add(statusWaiting);
            StatusItems.Add(statusConfirmed);
            StatusItems.Add(statusRejected);
        }

        public IEnumerable<OrderStatus> All()
        {
            return this.StatusItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class EventsTypeRepository : IEventsTypeRepository
    {
        public readonly IList<EventsType> EventsTypeItems;

        public EventsTypeRepository()
        {
            EventsTypeItems = new List<EventsType>();
            EventsTypeItems.Add(new EventsType
            {
                Id = 1,
                NameEventsType = "Cinema"
            });
            EventsTypeItems.Add(new EventsType
            {
                Id = 2,
                NameEventsType = "Theater"
            });
            EventsTypeItems.Add(new EventsType
            {
                Id = 3,
                NameEventsType = "Sport"
            });
        }

        public IEnumerable<EventsType> All()
        {
            return this.EventsTypeItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class AppRoleRepository : IAppRoleRepository
    {
        public readonly IList<AppRole> AppRoleItems;
        public IEnumerable<AppRole> All()
        {
            return this.AppRoleItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }

    public class AppUserRepository : IAppUserRepository
    {
        public readonly IList<AppUser> AppUserItems;
        public IEnumerable<AppUser> All()
        {
            return this.AppUserItems.ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
             
        }
    }
}