using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TicketSaleCore.Models.IdentityWithoutEF;
namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        readonly UserManager<AppUser> userManager;
        public UsersController(
            UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
      public async Task<IActionResult> Details(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}