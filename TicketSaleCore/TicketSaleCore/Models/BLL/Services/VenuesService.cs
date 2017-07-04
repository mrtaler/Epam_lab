using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class VenuesService : IVenuesService

    {
        private IUnitOfWork Context
        {
            get;
        }
        public VenuesService(IUnitOfWork context)
        {
            this.Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }


        public Venue GetVenue(int? id)
        {
            return Context.Venues.SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Venue> GetVenues()
        {
            return Context.Venues;
        }
    }
}
