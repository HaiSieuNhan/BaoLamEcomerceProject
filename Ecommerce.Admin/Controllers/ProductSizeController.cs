using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductSizeModel;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using static EcommerceCommon.Infrastructure.Helper.Helper;

namespace Ecommerce.Admin.Controllers
{
    public class ProductSizeController : Controller
    {
        private readonly IProductSizeService _productSizeService;

        public ProductSizeController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        public async Task<IActionResult> GetProductSizeList()
        {
            var model = await _productSizeService.GetProductSizeAdminViewModels();
            return View(model);
        }

        // GET: ProductSize/Create
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddProductSizeViewModel AddProductSizeViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productSizeService.AddProductSizeAsync(AddProductSizeViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productSizeService.GetProductSizeAdminViewModels()) });
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", AddProductSizeViewModel) });
        }

        // GET: ProductSize/Edit/
        public async Task<ActionResult> Edit(Guid Id)
        {
            var model = await _productSizeService.GetEditProductSizeViewModel(Id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: ProductSize/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProductSizeViewModel EditProductSizeViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productSizeService.EditProductSizeAsync(EditProductSizeViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productSizeService.GetProductSizeAdminViewModels()) });
                }
                else
                {
                    return NotFound();
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", EditProductSizeViewModel) });
        }

        // POST: ProductSize/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _productSizeService.DeleteProductSizeAsync(id))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productSizeService.GetProductSizeAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
