﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Models.BLL.Interfaces
{
    public interface IOrderStatusService : IBllService<OrderStatus>
    {
        bool IsExists(string name);
        OrderStatus Get(string name);
    }
}
