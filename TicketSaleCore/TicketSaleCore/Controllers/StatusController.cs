using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Controllers
{
    [Authorize(Roles = "admin")]
    public class StatusController : Controller
    {
        private readonly IUnitOfWork _context;

        public StatusController(IUnitOfWork context)
        {
            _context = context;    
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            return View(/*await*/ _context.OrderStatuses);
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = /*await*/ _context.OrderStatuses
                .SingleOrDefault(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusName")] OrderStatus status)
        {
            if (ModelState.IsValid)
            {
                _context.OrderStatuses.Add(status);
                /*await*/ _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = /*await*/ _context.OrderStatuses.SingleOrDefault(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusName")] OrderStatus status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.OrderStatuses.Update(status);
                    /*await*/ _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.Id))
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
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = /*await*/ _context.OrderStatuses
                .SingleOrDefault(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = /*await*/ _context.OrderStatuses.SingleOrDefault(m => m.Id == id);
            _context.OrderStatuses.Remove(status);
            /*await*/ _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool StatusExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.Id == id);
        }
    }
}
