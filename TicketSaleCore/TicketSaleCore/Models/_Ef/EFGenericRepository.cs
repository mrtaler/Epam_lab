//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using TicketSaleCore.Models.IRepository;

//namespace TicketSaleCore.Models._Ef
//{
//    public class EFGenericRepository<T> : IRepository<T> where T : class 
//    {
//        private DbContext context;
//        private DbSet<T> dbSet;

//        public EFGenericRepository(DbContext context)
//        {
//            this.context = context;
//            this.dbSet = context.Set<T>();
//        }

//        public IEnumerable<T> GetAll()
//        {
//            return dbSet.AsNoTracking().ToList();
//        }

//        public IEnumerable<T> Find(Func<T, bool> predicate)
//        {
//            return dbSet.AsNoTracking().Where(predicate).ToList();
//        }
//        public T FindById(int id)
//        {
//            return dbSet.Find(id);
//        }

//        public void Create(T item)
//        {
//            dbSet.Add(item);
//            context.SaveChanges();
//        }
//        public void Update(T item)
//        {
//            context.Entry(item).State = EntityState.Modified;
//            context.SaveChanges();
//        }
//        public void Remove(T item)
//        {
//            dbSet.Remove(item);
//            context.SaveChanges();
//        }

//        public void Add(T item)
//        {
//            dbSet.Add(item);
//            context.SaveChanges();
//        }
//    }
//}
