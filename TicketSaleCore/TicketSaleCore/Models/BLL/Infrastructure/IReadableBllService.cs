using System.Collections.Generic;

namespace TicketSaleCore.Models.BLL.Infrastructure
{
    /// <summary>
    /// Interface for read data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadableBllService<out T>
    {
        /// <summary>
        /// Get Data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int? id);
        /// <summary>
        /// Get Data by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get(string name);
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// check on exist in db by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsExists(int id);
        /// <summary>
        /// check on exist in db by name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExists(string name);
    
    }
}
