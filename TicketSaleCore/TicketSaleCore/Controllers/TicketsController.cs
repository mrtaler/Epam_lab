using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ApplicationContext context;

        public TicketsController(
            IStringLocalizer<HomeController> localizer,
            ILoggerFactory loggerFactory,
            ApplicationContext context)
        {
            this.context = context;
            this.localizer = localizer;
            logger = loggerFactory.CreateLogger<AccountController>();
        }
        [AllowAnonymous]
        // GET: Tickets
        public async Task<IActionResult> Index(int? id)
        {
            IEnumerable<Ticket> applicationContext;

            if (id!=null)
            {
                var q1 = context.TicketDbSet
                    .Where(p => p.EventId == id).Include(i=>i.Order).ToList();

                var q2 = q1.Where(p => p.Order== null).ToList();








                 applicationContext = context.TicketDbSet
                    .Where(p => p.EventId == id)
                    .Where(p=>p.Order==null)
                    .Include(t=>t.Event)
                    .Include(t => t.Seller);
                ViewData["CurentEvent"] = context.EventDbSet.Find(id);

            }
            else
            {
                applicationContext = context.TicketDbSet.Include(t => t.Event).Include(t => t.Seller);
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

            var ticket = await context.TicketDbSet
                .Include(t => t.Event)
                .Include(t => t.Seller)
                .SingleOrDefaultAsync(m => m.Id == id);
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
            ViewData["EventId"] = new SelectList(context.EventDbSet, "Id", "Id");
            ViewData["SellerId"] = new SelectList(context.Users, "Id", "Id");
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
                context.Add(ticket);
                await context.SaveChangesAsync();
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = @returnUrl;
            ViewData["EventId"] = new SelectList(context.EventDbSet, "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.Users, "Id", "Id", ticket.SellerId);
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

            var ticket = await context.TicketDbSet.SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] =@returnUrl;
            ViewData["EventId"] = new SelectList(context.EventDbSet, "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.Users, "Id", "Id", ticket.SellerId);
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
                    context.Update(ticket);
                    await context.SaveChangesAsync();
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
            ViewData["EventId"] = new SelectList(context.EventDbSet, "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.Users, "Id", "Id", ticket.SellerId);
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

            var ticket = await context.TicketDbSet
                .Include(t => t.Event)
                .Include(t => t.Seller)
                .SingleOrDefaultAsync(m => m.Id == id);
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
            var ticket = await context.TicketDbSet.SingleOrDefaultAsync(m => m.Id == id);
            context.TicketDbSet.Remove(ticket);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TicketExists(int id)
        {
            return context.TicketDbSet.Any(e => e.Id == id);
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
