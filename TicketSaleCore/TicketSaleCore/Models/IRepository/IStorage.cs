using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.IRepository
{
    public interface IStorage
    {
        T GetRepository<T>() where T : IRepository;
        void Save();
    }
}
