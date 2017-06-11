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

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class UserTicketsController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<UserTicketsController> localizer;
        private readonly ApplicationContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserTicketsController(
            SignInManager<AppUser> signInManager,
            IStringLocalizer<UserTicketsController> localizer,
            ILoggerFactory loggerFactory,
            ApplicationContext context,
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
             //   var users = User.FindFirst(ClaimTypes.NameIdentifier).Value;
             //var LoggedInUser = User.Identity;

             //   var claimsIdentity = (ClaimsIdentity)this.User.Identity;
             //   var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
             //   var userIdsss = claim.Value;


             //   //  var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

             //   var qwe=User.Claims.First(p=>p.Type.Contains("id")).Value;
             //   var qq = userManager.GetUserName(User);
                
                userId = id ?? userManager.GetUserId(User);

                //Find All User Selling Ticket
                var applicationContext = context.TicketDbSet

                    .Where(p => p.Seller.Id == userId)
                    .Where(z => z.Order == null)
                    .Include(p => p.Event)
                    .Include(p => p.Order)
                    .Include(p => p.Seller).ToList();

                //Find All User Selling Ticket Waiting for conformation
                var waitConf = context.TicketDbSet
                    .Include(p => p.Order)
                        .ThenInclude(p => p.Status)
                        .Include(z => z.Order.Buyer)
                    .Include(p => p.Seller)
                    .Include(p => p.Event)

                    .Where(p => p.Seller.Id == userId)
                    .Where(p => p.Order.Status.StatusName=="Waiting for conformation")
                    .ToList();


                ViewData["WaitConf"] = waitConf;

                //Find All User Selling Ticket Sold
                var confirmed = context.TicketDbSet
                    .Include(p => p.Order)
                        .ThenInclude(p => p.Status)
                        .Include(z=>z.Order.Buyer)
                    .Include(p => p.Seller)
                    .Include(p => p.Event)
                    
                    .Where(p => p.Seller.Id == userId)
                    .Where(p => p.Order.Status.StatusName=="Confirmed")
                   .ToList();
                ViewData["Confirmed"] = confirmed;

                return View(applicationContext);
            }
            else
            {
                return View();
            }

            // 

        }
    }
}