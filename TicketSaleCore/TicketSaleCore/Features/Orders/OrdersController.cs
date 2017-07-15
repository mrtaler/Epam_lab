using System.Linq;
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
            var qq = context.GetUserOrders(id).Where(p=>p.OrderTickets.Count>0);
            return View(qq);
        }
        // GET: Ordrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  context.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
