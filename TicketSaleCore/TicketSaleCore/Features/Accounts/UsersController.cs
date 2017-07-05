using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.Entities;
using TicketSaleCore.AuthorizationPolit.ResourceBased;

namespace TicketSaleCore.Features.Accounts
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthorizationService authorizationService;
        public UsersController(
            UserManager<AppUser> userManager,
            IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.authorizationService = authorizationService;
        }
        public async Task<IActionResult> Details(string id)
        {
            if(await authorizationService.AuthorizeAsync(this.User,
                userManager,
                Operations.Read))
            {
                AppUser user = await userManager.FindByIdAsync(id);
                if(user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            return new ChallengeResult();
        }
    }
}