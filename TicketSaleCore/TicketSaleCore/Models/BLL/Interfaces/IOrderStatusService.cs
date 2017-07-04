using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
    public interface IOrderStatusService
    {

        OrderStatus GetOrderStatus(int? id);
        IEnumerable<OrderStatus> GetOrderStatuses();

        void Dispose();
    }
}
