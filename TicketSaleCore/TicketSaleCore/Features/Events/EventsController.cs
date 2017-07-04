using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Features.Events
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService context;

        public EventsController(
            IEventService context
            )
        {
            this.context = context;    
        }
        [AllowAnonymous]
        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(context.GetEvents());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = context.GetEvent(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }
    }
}
