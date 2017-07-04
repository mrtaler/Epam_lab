using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Models.BLL.Services;

namespace TicketSaleCore.Features.Venues
{
    //[Authorize]
    [Authorize(Roles = "NotTask01")]
    public class VenuesController : Controller
    {
        private readonly VenuesService context;

        public VenuesController(VenuesService context)
        {
            this.context = context;    
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            
            return View(context.GetVenues());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = /*await*/ context.GetVenue(id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        //// GET: Venues/Create
        //[Authorize(Roles = "admin")]
        //public IActionResult Create()
        //{
        //    ViewData["CitiesFk"] = new SelectList(context.Citys, "Id", "Name");
        //    return View();
        //}

        //// POST: Venues/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(
        //    [Bind("Id,Name,Address,CityFk")] Venue venue)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Venues.Add(venue);
        //        //await _context.SaveChangesAsync();
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    //   new SelectList(db.Attachments, "uiIndex", "szAttName", iTEM.uiIndex);
        //    ViewData["CitiesFk"] = new SelectList(context.Citys, "Id", "Name", venue.CityFk);
        //    return View(venue);
        //}

        //// GET: Venues/Edit/5
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var venue = await _context.Venues.SingleOrDefaultAsync(m => m.Id == id);
        //    var venue =  context.Venues.SingleOrDefault(m => m.Id == id);
        //    if (venue == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CitiesFk"] = new SelectList(context.Citys, "Id", "Name", venue.CityFk);
        //    return View(venue);
        //}

        //// POST: Venues/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,CityFk")] Venue venue)
        //{
        //    if (id != venue.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            context.Venues.Update(venue);
        //             context.SaveChanges();
        //            //await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!VenueExists(venue.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["CitiesFk"] = new SelectList(context.Citys, "Id", "Name", venue.CityFk);
        //    return View(venue);
        //}

        //// GET: Venues/Delete/5
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var venue = await _context.Venues
        //    //    //.Include(v => v.City)
        //    //    .SingleOrDefaultAsync(m => m.Id == id);
        //    var venue =  context.Venues
        //        //.Include(v => v.City)
        //        .SingleOrDefault(m => m.Id == id);
        //    if (venue == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(venue);
        //}

        //// POST: Venues/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var venue = await _context.VenueDbSet.SingleOrDefaultAsync(m => m.Id == id);
        //    var venue =  context.Venues.SingleOrDefault(m => m.Id == id);
        //    context.Venues.Remove(venue);
        //     context.SaveChanges();
        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool VenueExists(int id)
        //{
        //    return context.Venues.Any(e => e.Id == id);
        //}
    }
}
