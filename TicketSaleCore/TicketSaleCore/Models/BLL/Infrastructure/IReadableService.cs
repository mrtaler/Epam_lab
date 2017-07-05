using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.BLL.Infrastructure
{
    /// <summary>
    /// Interface for read data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadableService<T>
    {
        /// <summary>
        /// Get Data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int? id);

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

    }
}
