using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models;

namespace TicketSaleCore.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ApplicationContext context;

        public HomeController(
            IStringLocalizer<HomeController> localizer,
            ILoggerFactory loggerFactory, 
            ApplicationContext context)
        {
            this.context = context;
            this.localizer = localizer;
            logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            var events=context.EventDbSet
                .Include(p=>p.Tickets)
                .ThenInclude(e=>e.Order)
                .Include(p=>p.Venue);
            return View(events);
        }
       // [Authorize(Policy = "AgeLimit")]
        public IActionResult About()
        {
            ViewData["Message"] = localizer["Your application description page."];

            return View();
        }
        //[Authorize(Policy = "AgeLimit")]
        public IActionResult Contact()
        {
            ViewData["Message"] = localizer["Your contact page."];

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
