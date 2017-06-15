using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using TicketSaleCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.IdentityWithoutEF;
using TicketSaleCore.Models.IRepository;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class UserTicketsController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<UserTicketsController> localizer;
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
            this.localizer = localizer;
            logger = loggerFactory.CreateLogger<AccountController>();
        }

        public async Task<IActionResult> Index(string id=null)
        {
            if (signInManager.IsSignedIn(User))
            {
                string userId = null;
            
                
                userId = id ?? userManager.GetUserId(User);
                 UserTicketsViewModel UserTickets=new UserTicketsViewModel(context, userId);
               

                return View(UserTickets);
            }
            else
            {
                return View("Error");
            }

            // 

        }
    }
}