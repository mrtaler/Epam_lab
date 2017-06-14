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
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly IUnitOfWork context;

        public CitiesController(IUnitOfWork context)
        {
            this.context = context;    
        }
        [AllowAnonymous]
        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(/*await*/ context.Citys);
        }
        [AllowAnonymous]
        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = /*await*/ context.Citys
                .SingleOrDefault(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }
        [Authorize(Roles = "admin")]
        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] City city)
        {
            if (ModelState.IsValid)
            {
                context.Citys.Add(city);
                /*await*/ context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }
        [Authorize(Roles = "admin")]
        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = /*await*/ context.Citys.SingleOrDefault(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Citys.Update(city);
                    /*await*/ context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
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
            return View(city);
        }
        [Authorize(Roles = "admin")]
        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = /*await*/ context.Citys
                .SingleOrDefault(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = /*await*/ context.Citys
                .SingleOrDefault(m => m.Id == id);
            context.Citys.Remove(city);
            /*await*/ context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CityExists(int id)
        {
            return context.Citys.Any(e => e.Id == id);
        }
    }
}
