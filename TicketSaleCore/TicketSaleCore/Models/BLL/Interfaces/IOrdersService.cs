using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
  public  interface IOrdersService
    {
        Order GetOrder(int? id);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetUserOrders(string id);

        void Dispose();
    }
}
