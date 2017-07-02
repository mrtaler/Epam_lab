using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.DAL.IRepository;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork context;

        public OrdersController(IUnitOfWork context)
        {
            this.context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string id)
        {

            var applicationContext = context.Orders.Include(o => o.Buyer)
                .Where(s => s.Buyer.Id.Equals(id))
                .Include(st=>st.Status)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Event)
                .Include(t => t.OrderTickets).ThenInclude(z => z.Seller);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await context.Orders
                .Include(o => o.Buyer)
                .SingleOrDefaultAsync(m => m.Id == id);
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