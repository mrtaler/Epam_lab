using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Memory
{
    public class MemoryGenericRepository<T> : DbSet<T>, IEnumerable<T> where T : class 
    {
        private List<T> list = new List<T>();
       
        public IEnumerable<T> GetAll()
        {
            return new List<T>(list);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return list.Where(predicate).ToList();
        }
        public T FindById(int id)
        {
            return null;
                //list.Find();
        }

        public void Create(T item)
        {
            list.Add(item);
        }
        public void Update(T item)
        {
         //   list.Remove(this.FindById(1));
            list.Add(item);
        }
        public void Remove(T item)
        {
            list.Remove(item);
        }

       public override EntityEntry<T> Add(T item)
       {
             // return this.SetEntityState<TEntity>(Check.NotNull<TEntity>(entity, "entity"), EntityState.Added);
            list.Add(item);
           return null;
       }


        IEnumerator<T>  IEnumerable<T>.GetEnumerator()
        {
           return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
