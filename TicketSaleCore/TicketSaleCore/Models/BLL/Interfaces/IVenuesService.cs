using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
  public  interface IVenuesService
    {
        Venue GetVenue(int? id);
        IEnumerable<Venue> GetVenues();

        void Dispose();
    }
}
