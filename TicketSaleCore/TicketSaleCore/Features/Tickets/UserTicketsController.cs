using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.BLL.Services;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Features.Tickets
{
    [Authorize]
    public class UserTicketsController : Controller
    {
        private readonly ITicketsService context;
        private readonly UserManager<AppUser> userManager;

        public UserTicketsController(
            ITicketsService context,
            UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> IndexAnotherUser(string userId)
        {
            if(userId != null)
            {
                if(!userId.Equals(User.FindFirst(p=>p.Type==ClaimTypes.NameIdentifier).Value)) //if id not equal Authorized User
                {
                    var qq = await userManager.Users.FirstOrDefaultAsync(p => p.Id == userId);//find User id repository
                    return View(qq);
                }
                else
                {
                    return RedirectToAction("Index", "UserTickets");
                }

            }
            return View("Error");
        }

        public async Task<IActionResult> SellingTickets(string userId = null)
        {
            return PartialView(context.GetAllUserTickets(userId, TicketStatus.SellingTickets));
        }
        public async Task<IActionResult> WaitingConfomition(string userId = null)
        {
            return PartialView("SellingTickets", context.GetAllUserTickets(userId, TicketStatus.WaitingConfomition));
        }
        public async Task<IActionResult> Sold(string userId = null)
        {
            return PartialView("SellingTickets", context.GetAllUserTickets(userId, TicketStatus.Sold));
        }

    }
}