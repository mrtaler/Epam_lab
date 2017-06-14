using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Memory
{
    public class MemoryGenericRepository<T> : DbSet<T> where T : class 
    {
        private DbSet<T> list = new InternalDbSet<T>(null);


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

        public void Add(T item)
        {
            list.Add(item);
        }
    }
}
