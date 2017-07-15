using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TicketSaleCore.Features.Orders
{
    public class OrderCartController : Controller
    {

        public OrderCartController()
        {
            
        }

        public IActionResult Index()
        {
            return PartialView();
        }
    }
}