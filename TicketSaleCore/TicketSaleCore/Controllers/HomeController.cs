using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models.IRepository;
using TicketSaleCore.ViewModels.HomeViewModels;

namespace TicketSaleCore.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUnitOfWork context;

        public HomeController(
            ILoggerFactory loggerFactory,
            IUnitOfWork context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            //show all Events (need to add data.now check)
            var events=context.Events
                .Include(p=>p.Tickets)
                .ThenInclude(e=>e.Order)
                .Include(p=>p.Venue)
                .ThenInclude(c=>c.City).OrderBy(t=>t.Date);

            List<EventsHomeViewModel> evHvEventsHomeViewModels=new List<EventsHomeViewModel>();

            foreach (var item in events)
            {
                var evTi = new EventsHomeViewModel(item);
                if (evTi.AvailableTicket>0)
                {
                    evHvEventsHomeViewModels.Add(evTi);
                }
               
            }
            return View(evHvEventsHomeViewModels);
        }

        public IActionResult About()
        {
            return View();
        }

        //[Authorize(Policy = "AgeLimit")]
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
