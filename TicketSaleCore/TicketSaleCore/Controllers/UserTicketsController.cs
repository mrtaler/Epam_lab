using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using TicketSaleCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class UserTicketsController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserTicketsController(
            SignInManager<User> signInManager,
            IStringLocalizer<HomeController> localizer,
            ILoggerFactory loggerFactory,
            ApplicationContext context,
            UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;
            this.localizer = localizer;
            logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var qm = userManager.GetUserName(User);

                //Find All User Selling Ticket
                var applicationContext = context.TicketDbSet

                    .Where(p => p.Seller.UserName == qm)
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

                    .Where(p => p.Seller.UserName == qm)
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
                    
                    .Where(p => p.Seller.UserName == qm)
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