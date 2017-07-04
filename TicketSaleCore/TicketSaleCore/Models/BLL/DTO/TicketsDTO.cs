using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.BLL.DTO
{
    public class TicketsDTO
    {

        public int Id
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }
        public string SellerNotes
        {
            get; set;
        }

        public int? OrderId
        {
            get; set;
        }
        //public Order Order
        //{
        //    get; set;
        //}

        public string SellerId
        {
            get; set;
        }
        //public AppUser Seller
        //{
        //    get; set;
        //}

        public int EventId
        {
            get; set;
        }
        //public Event Event
        //{
        //    get; set;
        //}

    }
}
