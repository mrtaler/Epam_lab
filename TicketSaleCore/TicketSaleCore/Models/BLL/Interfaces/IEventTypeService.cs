using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.BLL.DTO;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
    public interface IEventTypeService : IBllService<EventsType>
    {
        bool IsExists(string name);
        EventsType Get(string name);
    }
}
