using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.BLL.Infrastructure
{
    /// <summary>
    /// interface for edit service in BLL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEditableBllService<T>
    {
        /// <summary>
        /// Delete entry T from DAL
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>sucsecs</returns>
        bool Delete(T entity);
        /// <summary>
        /// Add entry T to DAL 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);
        /// <summary>
        /// Update entry in DAL
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);
    }
}
