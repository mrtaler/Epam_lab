using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.BLL.DTO;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
    public interface IEventService
    {
        void MakeEvent(Event _event);
        
        Event GetEvent(int? id);
        IEnumerable<Event> GetEvents();
        void Dispose();
    }
}
