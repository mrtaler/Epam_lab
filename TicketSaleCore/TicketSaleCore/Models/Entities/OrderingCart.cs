using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.Entities
{
    public class OrderingCart
    {
        public int Id { get; set; }

        public string OrderId { get; set; }
        public int TicketId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
