using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TicketSaleCore.Models.IRepository
{
  public  interface IRepository<TEntity> 
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        void Add(TEntity entity);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        void Delete(TEntity entity);

    }
    /*
    public interface IDbEntity
    {
        int Id { get; }
    }

    public abstract class RepositoryBase<T, DbT> : IRepository<T>
        where T : Entity where DbT : class, IDbEntity, new()
    {
        protected readonly DbContext context = new DbContext();

        public IQueryable<T> GetAll()
        {
            return GetTable().Select(GetConverter());
        }

        public bool Save(T entity)
        {
            DbT dbEntity;

            if (entity.IsNew())
            {
                dbEntity = new DbT();
            }
            else
            {
                dbEntity = GetTable().Where(x => x.Id == entity.Id).SingleOrDefault();
                if (dbEntity == null)
                {
                    return false;
                }
            }

            UpdateEntry(dbEntity, entity);

            if (entity.IsNew())
            {
                GetTable().InsertOnSubmit(dbEntity);
            }

            context.SubmitChanges();

            entity.Id = dbEntity.Id;
            return true;
        }

        public bool Delete(int id)
        {
            var dbEntity = GetTable().Where(x => x.Id == id).SingleOrDefault();

            if (dbEntity == null)
            {
                return false;
            }

            GetTable().DeleteOnSubmit(dbEntity);

            context.SubmitChanges();
            return true;
        }

        public bool Delete(T entity)
        {
            return Delete(entity.Id);
        }

        protected abstract Table<DbT> GetTable();
        protected abstract Expression<Func<DbT, T>> GetConverter();
        protected abstract void UpdateEntry(DbT dbEntity, T entity);
    }*/
    //public interface IDbContext
    //{
    //    IQueryable<T> Find<T>() where T : class;

    //    void MarkAsAdded<T>(T entity) where T : class;

    //    void MarkAsDeleted<T>(T entity) where T : class;

    //    void MarkAsModified<T>(T entity) where T : class;

    //    void Commit(bool withLogging);

    //    //откатывает изменения во всех модифицированных объектах
    //    void Rollback();

    //    // включает или отключает отслеживание изменений объектов
    //    void EnableTracking(bool isEnable);

    //    EntityState GetEntityState<T>(T entity) where T : class;

    //    void SetEntityState<T>(T entity, EntityState state) where T : class;

    //    // возвращает объект содержащий список объектов с их состоянием
    //    ChangeTracker GetChangeTracker();

    //    DbEntityEntry GetDbEntry<T>(T entity) where T : class;
    //}
}
