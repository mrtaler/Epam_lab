using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;
using TicketSaleCore.Models.BLL.Infrastructure;

namespace TicketSaleCore.Models.BLL.Services
{
    public class VenuesService : IVenuesService
    {
        private readonly IUnitOfWork context;
        public VenuesService(IUnitOfWork context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }


        public Venue Get(int? id)
        {
            return context.Venues.SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Venue> GetAll()
        {
            return context.Venues;
        }

        public bool Delete(Venue entity)
        {
            if (IsExists(entity.Id))
            {

                if (entity.Events.Count != 0)
                {
                    throw new BllValidationException($"This Venue {entity.Name} cannot delete" +
                                                     $" form DB because need cascade delete", "Need cascade");
                }
                else
                {
                    //  context.Entry(item).State = EntityState.Deleted

                    context.Venues.Remove(entity).State = EntityState.Deleted;
                    //   Remove(entity);
                    return Convert.ToBoolean(context.SaveChanges());

                }

            }
            else
            {
                throw new BllValidationException($"This Venue {entity.Name} cannot delete form DB because is not exist", "");
            }
        }

        public Venue Add(Venue entity)
        {
            if (!IsExists(entity.Name))
            {
                context.Venues.Add(entity).State=EntityState.Added;
                context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This City {entity.Name} is alredy exist", "alredy exist");
            }
            return entity;
        }

        public Venue Update(Venue entity)
        {
            if (IsExists(entity.Id))
            {
                // var updateEentitty = Get(entity.Id);

                //  updateEentitty = entity;
                context.Venues.Update(entity).State = EntityState.Modified;
                //  Context.Context.Entry(entity).State = EntityState.Modified;
                //  updateEentitty.Name = entity.Name;
                context.SaveChanges();
            }
            else
            {
                throw new BllValidationException($"This Venue name:{entity.Name},Id:{entity.Id} cannot Update" +
                                                 $" in DB because is not exist", "");
            }
            return entity;
        }

        public bool IsExists(int id)
        {
            return context.Venues.Any(e => e.Id == id);
        }

        public bool IsExists(string name)
        {
            return context.Venues.Any(e => e.Name == name);
        }

        public Venue Get(string name)
        {
            return context.Venues
                .Include(p => p.Events)
                .SingleOrDefault(m => m.Name == name);
        }
    }
}
