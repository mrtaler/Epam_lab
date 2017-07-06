using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Features.Home;
using TicketSaleCore.Models.BLL.Interfaces;

namespace TicketSaleCore.Features.Tickets
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketsService ticketsService;
        public TicketsController(
           ITicketsService ticketsService
            )
        {
            this.ticketsService = ticketsService;
        }
        [AllowAnonymous]
        // GET: Tickets
        public async Task<IActionResult> Index(int? id)
        {
            return View(ticketsService.GetTicketByEvent(id));
        }
        [AllowAnonymous]
        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var ticket = ticketsService.Get(id);
            if(ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
