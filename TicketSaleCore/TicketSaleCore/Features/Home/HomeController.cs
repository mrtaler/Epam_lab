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
        private IOrdersService ordersService;
        public HomeController(
            IEventService eventService, IOrdersService ordersService

            )
        {
            this.ordersService = ordersService;
            this.eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
            var tic = eventService.GetAll().First().Tickets.ToList();
            tic.AddRange(eventService.GetAll().Last().Tickets);

            var sing= ordersService.Get("User1 Order #1");

            if (sing!=null)
            {
                sing.TrackNo = "new test";

                var up = ordersService.NewOrder(tic);

                //var upRes = ordersService.Update(sing);

                //var resDel = ordersService.Delete(sing);
            }



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
