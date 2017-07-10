using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TicketSaleCore.Models.BLL.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Features.Events.EventsType.ViewModels;

namespace TicketSaleCore.Features.Events
{
    [Authorize(Roles = "admin")]
    public class EventsTypeController : Controller
    {
        private readonly IEventTypeService context;

        public EventsTypeController(IEventTypeService context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(context.GetAll());
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EventType = context.Get(id);
            if (EventType == null)
            {
                return NotFound();
            }
            return View(EventType);
        }

        #region Edit [Authorize(Roles = "admin")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Models.Entities.EventsType, EventsTypeEditCreateViewModel>());
            var qery = Mapper.Map<Models.Entities.EventsType, EventsTypeEditCreateViewModel>(context.Get(id));

            if (qery == null)
            {
                return NotFound();
            }
            return View(qery);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventsTypeEditCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<EventsTypeEditCreateViewModel, Models.Entities.EventsType>());
                    context.Update(Mapper.Map<EventsTypeEditCreateViewModel, Models.Entities.EventsType>(model));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.IsExists(model.Id))
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
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventsTypeEditCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<EventsTypeEditCreateViewModel, Models.Entities.EventsType>());
                    context.Add(Mapper.Map<EventsTypeEditCreateViewModel, Models.Entities.EventsType>(model));

                    return RedirectToAction("Index");
                }
                catch (BllValidationException er)
                {
                    ModelState.AddModelError("Data exist", er.Message);
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region delete  [Authorize(Roles = "admin")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityDelete = context.Get(id);

            if (cityDelete == null)
            {
                return NotFound();
            }

            return View(cityDelete);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            try
            {
                context.Delete(context.Get(id));

                return RedirectToAction("Index");
            }
            catch (BllValidationException er)
            {
                ModelState.AddModelError(er.Property, er.Message);
                return View(context.Get(id));
            }

        }
        #endregion
    }
}