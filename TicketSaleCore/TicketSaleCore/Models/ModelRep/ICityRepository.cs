using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models.ModelRep
{
    public interface ICityRepository:IRepository<City>
    {
        IEnumerable<City> getTwoCites();
    }
}
