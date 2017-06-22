using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.Models.IRepository;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class UserTicketsController : Controller
    {
        private readonly IUnitOfWork context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserTicketsController(
            SignInManager<AppUser> signInManager,
            IStringLocalizer<UserTicketsController> localizer,
            ILoggerFactory loggerFactory,
            IUnitOfWork context,
            UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;

        }

        public async Task<IActionResult> Index(string id = null)
        {
            if (signInManager.IsSignedIn(User))
            {
                bool userId=true;

                if (id!=null)
                {
                    userId = id.Equals(userManager.GetUserId(User));
                }

                UserTicketsViewModel userTickets = new UserTicketsViewModel(
                  context: context,
                  id: userManager.GetUserId(User),
                  userTag: userId);
                
                return View(userTickets);
            }
            else
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> first(string orderStatus,string userId = null)
        {
            if (userId==null)
            {
                userId = userManager.GetUserId(User);
            }
            List<Ticket> sellingTickets=new List<Ticket>(
                context.Tickets
                .Where(p => p.Seller.Id == userId)
                .Where(z => z.Order == null)
                .Include(p => p.Event)
                .Include(p => p.Order)
                .Include(p => p.Seller));
            JsonResult res=new JsonResult(sellingTickets);

           
            return PartialView(sellingTickets);
        }
        public async Task<IActionResult> second(string orderStatus, string userId = null)
        {
            if(userId == null)
            {
                userId = userManager.GetUserId(User);
            }
            List<Ticket> sellingTickets = new List<Ticket>(
                context.Tickets
                    .Include(p => p.Order)
                    .ThenInclude(p => p.Status)
                    .Include(z => z.Order.Buyer)
                    .Include(p => p.Seller)
                    .Include(p => p.Event)
                    .Where(p => p.Seller.Id == userId)
                    .Where(p => p.Order.Status.StatusName == "Waiting for conformation"));

            return PartialView("first", sellingTickets);
        }

    }
}