using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Services;
using TicketSaleCore.Models.BLL.Interfaces;

namespace TicketSaleCore.Features.Orders
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService context;

        public OrdersController(IOrdersService context)
        {
            this.context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string id)
        {
            return View(context.GetUserOrders(id));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  context.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }



        //private bool OrderExists(int id)
        //{
        //    return context.Orders.Any(e => e.Id == id);
        //}
    }
}
