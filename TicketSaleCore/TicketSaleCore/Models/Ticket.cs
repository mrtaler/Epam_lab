using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public decimal Price { get; set; }
        public User Seller { get; set; }
    }
}
