using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductColorModel;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using static EcommerceCommon.Infrastructure.Helper.Helper;

namespace Ecommerce.Admin.Controllers
{
    public class ProductColorController : Controller
    {
        private readonly IProductColorService _productColorService;

        public ProductColorController(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        public async Task<IActionResult> GetProductColorList()
        {
            var model = await _productColorService.GetProductColorAdminViewModels();
            return View(model);
        }

        // GET: ProductColor/Create
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddProductColorViewModel AddProductColorViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productColorService.AddProductColorAsync(AddProductColorViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productColorService.GetProductColorAdminViewModels()) });
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", AddProductColorViewModel) });
        }

        // GET: ProductColor/Edit/
        public async Task<ActionResult> Edit(Guid Id)
        {
            var model = await _productColorService.GetEditProductColorViewModel(Id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: ProductColor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProductColorViewModel EditProductColorViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productColorService.EditProductColorAsync(EditProductColorViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productColorService.GetProductColorAdminViewModels()) });
                }
                else
                {
                    return NotFound();
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", EditProductColorViewModel) });
        }

        // POST: ProductColor/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _productColorService.DeleteProductColorAsync(id))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productColorService.GetProductColorAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
