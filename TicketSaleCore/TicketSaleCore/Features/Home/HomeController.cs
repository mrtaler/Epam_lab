namespace TicketSaleCore.Features.Home
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;

    using TicketSaleCore.Models.BLL.Interfaces;

    // [ServiceFilter(typeof(LanguageActionFilter))]
    public class HomeController : Controller
    {
        /// <summary>
        /// The event service.
        /// </summary>
        private readonly IEventService eventService;

        /// <summary>
        /// The orders service.
        /// </summary>
        private readonly IOrdersService ordersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="eventService">
        /// The event service.
        /// </param>
        /// <param name="ordersService">
        /// The orders service.
        /// </param>
        public HomeController(IEventService eventService, IOrdersService ordersService)
        {
            this.ordersService = ordersService;
            this.eventService = eventService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IActionResult> Index()
        {
            var tic = this.eventService.GetAll().First().Tickets.ToList();
            tic.AddRange(this.eventService.GetAll().Last().Tickets);

            var sing = this.ordersService.Get("User1 Order #1");

            var up = this.ordersService.NewOrderWithTickets(tic);

            if (sing != null)
            {
               // sing.TrackNo = "new test";

                

                // var upRes = ordersService.Update(sing);

                // var resDel = ordersService.Delete(sing);
            }

            var events = this.eventService.GetAllEventWithTickets();
            return this.View(events);
        }

        /// <summary>
        /// The about.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult About()
        {
            return this.View();
        }

        // [Authorize(Policy = "Read")]
        /// <summary>
        /// The contact.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Contact()
        {
            return this.View();
        }

        /// <summary>
        /// The set language.
        /// </summary>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            this.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return this.LocalRedirect(returnUrl);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
