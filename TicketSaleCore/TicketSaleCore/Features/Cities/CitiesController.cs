using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;

namespace TicketSaleCore.Features.Cities
{
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly ICityService context;

        public CitiesController(ICityService context)
        {
            this.context = context;    
        }

        [AllowAnonymous]
        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(context.GetCities());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = context.GetCity(id);
               
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }
    }
}
