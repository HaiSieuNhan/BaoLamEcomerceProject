using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcommerceCommon.Infrastructure.Helper.Helper;
using EcommerceCommon.Infrastructure.Ultil;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using Ecommerce.Service.ViewModels.Admin.CouponModel;

namespace Ecommerce.Admin.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        private readonly ICategoryService _categoryService;
        private readonly ICollectionService _collectionService;

        public CouponController(ICouponService couponService, ICategoryService categoryService, ICollectionService collectionService)
        {
            _categoryService = categoryService;

            _couponService = couponService;
            _collectionService = collectionService;

        }
        public async Task<IActionResult> GetCouponList()
        {
            var models = await _couponService.GetCouponAdminViewModels();

          
            return View(models);
        }

        // GET: Coupon/Create
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {

            var model = await _couponService.GetAddCouponModel();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCouponViewModel AddCouponViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _couponService.AddCouponAsync(AddCouponViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _couponService.GetCouponAdminViewModels()) });

                }
                else
                {
                    return NotFound();
                }
            }
            var model = await _couponService.GetAddCouponViewModels(AddCouponViewModel);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            // var coupon = await _couponService.GetByIdAsync(id);

            var models = await _couponService.GetEditCouponModel(id);


            if (models == null)
            {
                return NotFound();
            }
            return View(models);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCouponViewModel editCouponViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _couponService.EditCouponAsync(editCouponViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _couponService.GetCouponAdminViewModels())});
                }
                else
                {
                    return NotFound();
                }

            }
            var model = await _couponService.GetEditCouponModel(editCouponViewModel);
            if (model == null)
                return NotFound();
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {

            if (await _couponService.DeleteCouponAsync(id))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _couponService.GetCouponAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }

        }
    }
}
