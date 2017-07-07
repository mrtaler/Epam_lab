using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.DAL.IRepository;

namespace TicketSaleCore.Models.BLL.Infrastructure
{
    public interface IBllService<T> : IEditableBllService<T>, IReadableBllService<T>
    {
        void Dispose();
    }
}
