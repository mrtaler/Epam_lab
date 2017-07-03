﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models.BLL.Interfaces;

namespace TicketSaleCore.Features.Home
{
  //  [ServiceFilter(typeof(LanguageActionFilter))]
    public class HomeController : Controller
    {
        //private readonly IUnitOfWork context;
        private readonly IEventService eventService;
        public HomeController(
            ILoggerFactory loggerFactory,
           /* IUnitOfWork context*/
            IEventService eventService
            )
        {
         //   this.context = context;
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            var events = eventService.GetEvents().Where(t => t.Tickets.Count(c => c.Order == null) > 0);


            //show all Events (need to add data.now check)
            //var events=context.Events
            //    .Include(p=>p.Tickets)
            //    .ThenInclude(e=>e.Order)
            //    .Include(p=>p.Venue)
            //    .ThenInclude(c=>c.City).OrderBy(t=>t.Date)
            //    .Where(t=>t.Tickets.Count(c=>c.Order==null)>0)
            //    .Where(t=>t.Date>=DateTime.UtcNow);


            return View(events);
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
