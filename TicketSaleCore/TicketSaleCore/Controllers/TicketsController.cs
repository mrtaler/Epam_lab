using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        //private readonly ILogger logger;
        //private readonly IStringLocalizer<TicketsController> localizer;
        private readonly IUnitOfWork context;

        public TicketsController(
            //IStringLocalizer<TicketsController> localizer,
            //ILoggerFactory loggerFactory,
            IUnitOfWork context)
        {
            this.context = context;
           // this.localizer = localizer;
          //  logger = loggerFactory.CreateLogger<TicketsController>();
        }
        [AllowAnonymous]
        // GET: Tickets
        public async Task<IActionResult> Index(int? id)
        {
            var availableTicketsToSale = new TicketIndexViewModel(context, id);
            return View(availableTicketsToSale);
           

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

            var ticket = await context.Tickets
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
        //[Authorize(Roles = "admin")]
        [Authorize(Roles = "NotTask01")]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = @returnUrl;
            ViewData["EventId"] = new SelectList(context.Events, "Id", "Id");
            ViewData["SellerId"] = new SelectList(context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [Authorize(Roles = "NotTask01")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,SellerId,EventId")] Ticket ticket, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                context.Tickets.Add(ticket);
                /*await*/ context.SaveChanges();
                return RedirectToLocal(returnUrl);
            }
            ViewData["ReturnUrl"] = @returnUrl;
            ViewData["EventId"] = new SelectList(context.Events, "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.AppUsers, "Id", "Id", ticket.SellerId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        //[Authorize(Roles = "admin")]
        [Authorize(Roles = "NotTask01")]
        public async Task<IActionResult> Edit(int? id, string returnUrl = null)
        {
           // var retUrl = returnUrl.Replace(@"/Tickets","");

            if (id == null)
            {
                return NotFound();
            }

            var ticket = await context.Tickets.SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] =@returnUrl;
            ViewData["EventId"] = new SelectList(context.Events, "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.AppUsers, "Id", "Id", ticket.SellerId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [Authorize(Roles = "NotTask01")]
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
                    /*await*/ context.SaveChanges();
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
            ViewData["EventId"] = new SelectList(context.Events, "Id", "Id", ticket.EventId);
            ViewData["SellerId"] = new SelectList(context.AppUsers, "Id", "Id", ticket.SellerId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        //[Authorize(Roles = "admin")]
        [Authorize(Roles = "NotTask01")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = /*await*/ context.Tickets
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
        //[Authorize(Roles = "admin")]
        [Authorize(Roles = "NotTask01")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = /*await*/ context.Tickets.SingleOrDefault(m => m.Id == id);
            context.Tickets.Remove(ticket);
            /*await*/ context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool TicketExists(int id)
        {
            return context.Tickets.Any(e => e.Id == id);
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
