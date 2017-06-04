﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class TicketsOrder
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}