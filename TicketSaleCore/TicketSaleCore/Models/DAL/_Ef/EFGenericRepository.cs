//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;
//using TicketSaleCore.Models.IRepository;

//namespace TicketSaleCore.Models.DAL._Ef
//{
//    public class EfGenericRepository<T> : IRepository<T> where T : class
//    {
//        private readonly DbContext context;
//       // private DbSet<T> dbSet;

//        public EfGenericRepository(DbContext context)
//        {
//            this.context = context;
//         //   this.dbSet = context.Set<T>();
//        }
//        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
//        {
//            IQueryable<T> dbQuery = context.Set<T>();

//            //Apply eager loading
//            foreach(Expression<Func<T, object>> navigationProperty in navigationProperties)
//                dbQuery = dbQuery.Include<T, object>(navigationProperty);

//            var list = dbQuery
//                .AsNoTracking()
//                .ToList<T>();
//            return list;
//        }

//        public virtual IList<T> GetList(Func<T, bool> where,
//            params Expression<Func<T, object>>[] navigationProperties)
//        {
//            IQueryable<T> dbQuery = context.Set<T>();

//            //Apply eager loading
//            foreach(Expression<Func<T, object>> navigationProperty in navigationProperties)
//                dbQuery = dbQuery.Include<T, object>(navigationProperty);

//            var list = dbQuery
//                .AsNoTracking()
//                .Where(@where)
//                .ToList<T>();
//            return list;
//        }

//        public virtual T GetSingle(Func<T, bool> where,
//            params Expression<Func<T, object>>[] navigationProperties)
//        {
//            T item = null;
//            IQueryable<T> dbQuery = context.Set<T>();

//            //Apply eager loading
//            foreach(Expression<Func<T, object>> navigationProperty in navigationProperties)
//                dbQuery = dbQuery.Include<T, object>(navigationProperty);

//            item = dbQuery
//                .AsNoTracking() //Don't track any changes for the selected item
//                .FirstOrDefault(where); //Apply where clause
//            return item;
//        }

//        public virtual void Remove(params T[] items)
//        {
//            foreach(T item in items)
//            {
//                context.Entry(item).State = EntityState.Deleted;
//            }
//            context.SaveChanges();
//        }
//        public virtual void Add(params T[] items)
//        {
//            foreach(T item in items)
//            {
//                context.Entry(item).State = EntityState.Added;
//            }
//            context.SaveChanges();
//        }

//        public virtual void Update(params T[] items)
//        {
//            foreach(T item in items)
//            {
//                context.Entry(item).State = EntityState.Modified;
//            }
//            context.SaveChanges();
//        }
//    }
//}
