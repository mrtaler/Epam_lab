using System.Collections.Generic;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
  public  interface ICityService
    {
        City GetCity(int? id);
        IEnumerable<City> GetCities();
        void Dispose();
    }
}
