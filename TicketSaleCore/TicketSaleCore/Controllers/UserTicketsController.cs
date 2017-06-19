using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Identity;
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
    }
}