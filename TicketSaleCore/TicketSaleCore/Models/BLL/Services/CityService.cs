using System;
using System.Collections.Generic;
using TicketSaleCore.Models.BLL.Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Cities with include Venues</returns>
        public IEnumerable<City> GetAll()
        {
            return (Context.Citys
                .Include(p => p.Venues));
        }
        /// <summary>
        /// Get City by Id
        /// </summary>
        /// <param name="id">Uniqe identifier</param>
        /// <returns>City with include Venues</returns>
        public City Get(int? id)
        {
            return Context.Citys
                .Include(p => p.Venues)
                .SingleOrDefault(m => m.Id == id);
        }
        /// <summary>
        /// Get City by Id
        /// </summary>
        /// <param name="id">Uniqe identifier</param>
        /// <returns>City with include Venues</returns>
        public City Get(string name)
        {
            return Context.Citys
                .Include(p => p.Venues)
                .SingleOrDefault(m => m.Name == name);
        }
        /// <summary>
        /// Delete City from db (not cascade)
        /// </summary>
        /// <param name="entity">City to delete</param>
        /// <returns>Success of the result</returns>
        /// <exception cref="BllValidationException">Need access to cascade removal</exception>
        public bool Delete(City entity)
        {
            if (this.IsExists(entity.Id))
            {

                if (entity.Venues.Count != 0)
                {
                    throw new BllValidationException($"This City {entity.Name} cannot delete" +
                                                     $" form DB because need cascade delete", "Need cascade");
                }
                else
                {
                    var ci = Context.Citys.Remove(entity);
                    Context.SaveChanges();
                    if (ci != null)
                    {
                        return true;
                    }
                }

            }
            else
            {
                throw new BllValidationException($"This City {entity.Name} cannot delete form DB because is not exist", "");
            }

            return false;
        }
        /// <summary>
        /// Add City to db with check on exist
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public City Add(City entity)
        {
            if (!IsExists(entity.Name))
            {
                Context.Citys.Add(entity);
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This City {entity.Name} is alredy exist", "alredy exist");
            }
            return entity;
        }
        /// <summary>
        /// Update exist City
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public City Update(City entity)
        {
            if (IsExists(entity.Id))
            {
                var updateEentitty = this.Get(entity.Id);
                updateEentitty.Name = entity.Name;
                Context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This City name:{entity.Name},Id:{entity.Id} cannot Update" +
                                                 $" in DB because is not exist", "");
            }
            return entity;
        }
        /// <summary>
        /// Check on exist in db
        /// </summary>
        /// <param name="id">unique Id</param>
        /// <returns></returns>
        public bool IsExists(int id)
        {
            return Context.Citys.Any(e => e.Id == id);
        }

        public bool IsExists(string name)
        {
            return Context.Citys.Any(e => e.Name == name);
        }
    }
}
