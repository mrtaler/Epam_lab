using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Features.Cities.ViewModels;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketSaleCore.Features.Cities
{
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly ICityService context;

        public CitiesController(ICityService context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(context.GetAll());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var city = context.Get(id);
            if(city == null)
            {
                return NotFound();
            }
            return View(city);
        }
        #region Edit [Authorize(Roles = "admin")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<City, CityEditCreateViewModel>());
            var qery = Mapper.Map<City, CityEditCreateViewModel>(context.Get(id));

            if(qery == null)
            {
                return NotFound();
            }
            return View(qery);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityEditCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<CityEditCreateViewModel, City>());
                    context.Update(Mapper.Map<CityEditCreateViewModel, City>(model));
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!context.IsExists(model.Id))
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
            return View(model);
        }
        #endregion
        #region Create  [Authorize(Roles = "admin")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(CityEditCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CityEditCreateViewModel, City>());
                context.Add(Mapper.Map<CityEditCreateViewModel, City>(model));

                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
    }
}
