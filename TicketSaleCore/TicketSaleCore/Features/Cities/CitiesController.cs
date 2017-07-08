using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSaleCore.Features.Cities.ViewModels;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;

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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<City, CityEditCreateViewModel>());
            var qe = Mapper.Map<City, CityEditCreateViewModel>(context.Get(id));
            return View(Mapper.Map<City, CityEditCreateViewModel>(context.Get(id)));
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityEditCreateViewModel model)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<CityEditCreateViewModel, City>());
            context.Update(Mapper.Map<CityEditCreateViewModel, City>(model));
            return View();
        }
    }
}
