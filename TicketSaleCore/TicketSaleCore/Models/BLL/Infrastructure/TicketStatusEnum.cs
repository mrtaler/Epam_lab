using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.BLL.Infrastructure
{
    public static class TicketStatusEnum
    {
        public enum TicketStatus
        {
            SellingTickets,
            WaitingConfomition,
            Sold
        }
    }
}
