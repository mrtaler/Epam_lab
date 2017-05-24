using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationContext context;
        private IHostingEnvironment appEnvironment;
        private IStringLocalizer<EventsController> localizer;
        private ILoggerFactory loggerFactory;

        public EventsController(ApplicationContext context, IHostingEnvironment appEnvironment, IStringLocalizer<EventsController> localizer, ILoggerFactory loggerFactory)
        {
         this.appEnvironment = appEnvironment;
            this.localizer = localizer;
            this.loggerFactory = loggerFactory;
            this.context = context;    
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await context.EventDbSet.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.EventDbSet
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["VenueFk"] = new SelectList(context.VenueDbSet, "Id", "Address");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Date,Banner,Description,VenueId")] Event @event,
            IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        string path = "/images/EventImg/" + uploadedFile.FileName;
                        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        @event.Banner = path;
                    }

                    context.Add(@event);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
    
            ViewData["VenueFk"] = new SelectList(context.VenueDbSet, "Id", "Address",@event.VenueId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.EventDbSet.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Banner,Description")] Event @event, IFormFile uploadedFile)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        string path = "/images/EventImg/" + uploadedFile.FileName;
                        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        @event.Banner = path;
                    }

                    context.Update(@event);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(@event); //RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.EventDbSet
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await context.EventDbSet.SingleOrDefaultAsync(m => m.Id == id);
            context.EventDbSet.Remove(@event);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return context.EventDbSet.Any(e => e.Id == id);
        }
    }
}
