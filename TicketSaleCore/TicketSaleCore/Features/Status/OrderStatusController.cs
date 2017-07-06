using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Interfaces;

namespace TicketSaleCore.Features.Status
{
    //[Authorize(Roles = "admin")]
    [Authorize(Roles = "NotTask01")]
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusService context;

        public OrderStatusController(IOrderStatusService context)
        {
            this.context = context;    
        }
        // GET: Status
        public async Task<IActionResult> Index()
        {
            return View(context.GetAll());
        }
        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = context.Get(id);
               
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }
    }
}
