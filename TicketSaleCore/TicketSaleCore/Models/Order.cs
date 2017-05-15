using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public string Status { get; set; }
        public User Buyer { get; set; }
        public string TrackNo{ get; set; }
    }
}
