using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Services;

namespace TicketSaleCore.Features.Venues
{
    //[Authorize]
    [Authorize(Roles = "NotTask01")]
    public class VenuesController : Controller
    {
        private readonly VenuesService context;

        public VenuesController(VenuesService context)
        {
            this.context = context;    
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            
            return View(context.GetAll());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue =  context.Get(id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }
    }
}
