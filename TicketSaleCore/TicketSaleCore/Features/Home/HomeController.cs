using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TicketSaleCore.AuthorizationPolit.ResourceBased;

namespace TicketSaleCore.Features.Home
{
    //  [ServiceFilter(typeof(LanguageActionFilter))]
    public class HomeController : Controller
    {
        private readonly IEventService eventService;
        public HomeController(
            IEventService eventService
            )
        {
            this.eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
            var events = eventService.GetAllEventWithTickets();
            return View(events);
        }
        public IActionResult About()
        {
            return View();
        }
        //[Authorize(Policy = "Read")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
