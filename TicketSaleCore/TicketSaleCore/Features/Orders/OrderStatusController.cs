using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Features.Cities.ViewModels;
using TicketSaleCore.Features.Orders.OrderStatus.ViewModels;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.Entities;

namespace TicketSaleCore.Features.Orders
{
    [Authorize(Roles = "admin")]
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusService context;

        public OrderStatusController(IOrderStatusService context)
        {
            this.context = context;    
        }

        public async Task<IActionResult> Index()
        {
            return View(context.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = context.Get(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View(orderStatus);
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

            Mapper.Initialize(cfg => cfg.CreateMap<Models.Entities.OrderStatus, OrderStatusEditCreateViewModel>());
            var qery = Mapper.Map<Models.Entities.OrderStatus, OrderStatusEditCreateViewModel>(context.Get(id));

            if (qery == null)
            {
                return NotFound();
            }
            return View(qery);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderStatusEditCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OrderStatusEditCreateViewModel, Models.Entities.OrderStatus>());
                    context.Update(Mapper.Map<OrderStatusEditCreateViewModel, Models.Entities.OrderStatus>(model));
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
        public async Task<IActionResult> Create(OrderStatusEditCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OrderStatusEditCreateViewModel, Models.Entities.OrderStatus>());
                    context.Add(Mapper.Map<OrderStatusEditCreateViewModel, Models.Entities.OrderStatus>(model));

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

            var orderStatusDelete = context.Get(id);

            if (orderStatusDelete == null)
            {
                return NotFound();
            }

            return View(orderStatusDelete);
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
