using System.Collections.Generic;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
    public interface IOrdersService : IBllService<Order>
    {
        Order NewOrder(IEnumerable<Ticket> orderingTickets);
       
        IEnumerable<Order> GetUserOrders(string id);
    }
}
