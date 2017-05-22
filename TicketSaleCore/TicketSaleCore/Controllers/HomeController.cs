using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using TicketSaleCore.Models;

namespace TicketSaleCore.Controllers
{
   [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ApplicationContext _context;

        public HomeController(IStringLocalizer<HomeController> localizer, ILoggerFactory loggerFactory, ApplicationContext context)
        {
            _context = context;
            this.localizer = localizer;
            logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            ViewData["MyTitle"] = localizer["Yourapplicationdescriptionpage"];
            ViewData["MyTitle1"] = localizer["Yourcontactpage"];
         //   ViewData["Event"] = _context.Event.First();
            var qwe = _context.EventDbSet.First();
            return View(_context.EventDbSet.First());
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
