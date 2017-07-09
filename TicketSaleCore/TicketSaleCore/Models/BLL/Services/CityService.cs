using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<City> GetAll()
        {
            return (Context.Citys);
        }
        public City Get(int? id)
        {
            return Context.Citys.SingleOrDefault(m => m.Id == id);
        }

        public bool Delete(City entity)
        {
            throw new NotImplementedException();
        }
        public City Add(City entity)
        {
            throw new NotImplementedException();
        }
        public City Update(City entity)
        {
            var updateEentitty = this.Get(entity.Id);
            updateEentitty.Name = entity.Name;
            Context.SaveChanges();
            return entity;
        }

        public bool IsExists(int id)
        {
            return   Context.Citys.Any(e => e.Id == id);
        }
    }
}
