using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;

namespace TicketSaleCore.Controllers
{
    [Authorize]
    public class VenuesController : Controller
    {
        private readonly ApplicationContext _context;

        public VenuesController(ApplicationContext context)
        {
            _context = context;    
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.VenueDbSet.Include(v => v.City);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.VenueDbSet
                .Include(v => v.City)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["CitiesFk"] = new SelectList(_context.CityDbSet, "Id", "Name");
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Address,CityFk")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //   new SelectList(db.Attachments, "uiIndex", "szAttName", iTEM.uiIndex);
            ViewData["CitiesFk"] = new SelectList(_context.CityDbSet, "Id", "Name", venue.CityFk);
            return View(venue);
        }

        // GET: Venues/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.VenueDbSet.SingleOrDefaultAsync(m => m.Id == id);
            if (venue == null)
            {
                return NotFound();
            }
            ViewData["CitiesFk"] = new SelectList(_context.CityDbSet, "Id", "Name", venue.CityFk);
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,CityFk")] Venue venue)
        {
            if (id != venue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.Id))
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
            ViewData["CitiesFk"] = new SelectList(_context.CityDbSet, "Id", "Name", venue.CityFk);
            return View(venue);
        }

        // GET: Venues/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.VenueDbSet
                .Include(v => v.City)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.VenueDbSet.SingleOrDefaultAsync(m => m.Id == id);
            _context.VenueDbSet.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VenueExists(int id)
        {
            return _context.VenueDbSet.Any(e => e.Id == id);
        }
    }
}
