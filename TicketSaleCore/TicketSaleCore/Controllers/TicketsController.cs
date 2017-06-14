using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<TicketsController> localizer;
        private readonly IUnitOfWork context;

        public TicketsController(
            IStringLocalizer<TicketsController> localizer,
            ILoggerFactory loggerFactory,
            IUnitOfWork context)
        {
            this.context = context;
            this.localizer = localizer;
            logger = loggerFactory.CreateLogger<TicketsController>();
        }
        [AllowAnonymous]
        // GET: Tickets
        public async Task<IActionResult> Index(int? id)
        {
            IEnumerable<Ticket> applicationContext;

            if (id!=null)
            {
                applicationContext = context.Tickets.GetAll()
                    .Where(p => p.EventId == id)
                    .Where(p => p.Order == null);
                    //.Include(t => t.Event)
                    //.Include(t => t.Seller);

                ViewData["CurentEvent"] = context.Events.GetAll();
                    //.Include(p => p.Venue).ThenInclude(z=>z.City).First(p=>p.Id==id);

            }
            else
            {
               applicationContext = context.Tickets.GetAll();//.Include(t => t.Event).Include(t => t.Seller);
            }
          

            
            return View(applicationContext);
           

          //  var applicationContext = context.TicketDbSet.Include(t => t.Event).Include(t => t.Seller);
         //   return RedirectToAction("Index",await cont.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = /*await*/ context.Tickets.GetAll().SingleOrDefault(m => m.Id == id);
                //.Include(t => t.Event)
                //.Include(t => t.Seller)
                //.SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = @returnUrl;
            ViewData["EventId"] = new SelectList(context.Events.GetAll(), "Id", "Id");
            ViewData["SellerId"] = new SelectList(context.AppUsers.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,SellerId,EventId")] Ticket ticket, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                context.Tickets.Add(ticket);
                /*await*/ context.SaveChanged();
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = @returnUrl;
            ViewData["EventId"] = new SelectList(context.Events.GetAll(), "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.AppUsers.GetAll(), "Id", "Id", ticket.SellerId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id, string returnUrl = null)
        {
           // var retUrl = returnUrl.Replace(@"/Tickets","");

            if (id == null)
            {
                return NotFound();
            }

        //    var ticket = await context.TicketDbSet.SingleOrDefaultAsync(m => m.Id == id);
            var ticket = /*await*/ context.Tickets.GetAll().SingleOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] =@returnUrl;
            ViewData["EventId"] = new SelectList(context.Events.GetAll(), "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.AppUsers.GetAll(), "Id", "Id", ticket.SellerId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,SellerId,EventId")] Ticket ticket, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = @returnUrl;
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Tickets.Update(ticket);
                    /*await*/ context.SaveChanged();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction("Index");
                return RedirectToLocal(returnUrl);
            }
            ViewData["EventId"] = new SelectList(context.Events.GetAll(), "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.AppUsers.GetAll(), "Id", "Id", ticket.SellerId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = /*await*/ context.Tickets.GetAll()
              //  .Include(t => t.Event)
              //  .Include(t => t.Seller)
                .SingleOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = /*await*/ context.Tickets.GetAll().SingleOrDefault(m => m.Id == id);
            context.Tickets.Remove(ticket);
            /*await*/ context.SaveChanged();
            return RedirectToAction("Index");
        }

        private bool TicketExists(int id)
        {
            return context.Tickets.GetAll().Any(e => e.Id == id);
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
