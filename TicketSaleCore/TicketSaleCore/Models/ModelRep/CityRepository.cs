using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.ModelRep
{
    public class CityRepository : IRepository.Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationContext context) : base(context)
        {

        }
        public IEnumerable<City> getTwoCites()
        {
            throw new NotImplementedException();
        }
    }
}
