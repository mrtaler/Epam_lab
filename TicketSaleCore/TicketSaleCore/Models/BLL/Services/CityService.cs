using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Services
{
    public class CityService : ICityService
    {
        private IUnitOfWork Context
        {
            get; 
        }
        public CityService(IUnitOfWork context)
        {
            this.Context = context;
        }
        public void Dispose()
        {
           Context.Dispose();
        }

        public IEnumerable<City> GetCities()
        {
          return (Context.Citys);
        }

        public City GetCity(int? id)
        {
          return Context.Citys.SingleOrDefault(m => m.Id == id);
        }
    }
}
