using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductImageModel;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.Ultil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static EcommerceCommon.Infrastructure.Helper.Helper;

namespace Ecommerce.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        private readonly IProductColorService _productColorService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductImageController(IProductImageService productImageService, IProductColorService productColorService, IWebHostEnvironment hostEnvironment)
        {
            _productImageService = productImageService;
            _productColorService = productColorService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> GetProductImageList(Guid Id)
        {
            var model = await _productImageService.GetProductImageAdminViewModels(Id);
            ViewBag.ProductId = Id;
            return View(model);
        }
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            var model = await _productImageService.GetAddProductImageModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddProductImageViewModel AddProductImageViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productImageService.AddProductImageAsync(AddProductImageViewModel, _hostEnvironment.WebRootPath))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewAndViewBagToString(this, "_ViewAll", await _productImageService.GetProductImageAdminViewModels(AddProductImageViewModel.ProductId), AddProductImageViewModel.ProductId) });
                }
            }
            var model = await _productImageService.GetAddProductImageModel(AddProductImageViewModel);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
        }

        // GET: ProductImage/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await _productImageService.GetEditProductImageModel(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: ProductImage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProductImageViewModel EditProductImageViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productImageService.EditProductImageAsync(EditProductImageViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewAndViewBagToString(this, "_ViewAll", await _productImageService.GetProductImageAdminViewModels(EditProductImageViewModel.ProductId), EditProductImageViewModel.ProductId) });
                }
                else
                {
                    return NotFound();
                }

            }
            var model = await _productImageService.GetEditProductImageModel(EditProductImageViewModel);
            if (model == null)
                return NotFound();
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });
        }


        // POST: ProductImage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Guid ProductId)
        {

            if (await _productImageService.DeleteProductImageAsync(id, _hostEnvironment.WebRootPath))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewAndViewBagToString(this, "_ViewAll", await _productImageService.GetProductImageAdminViewModels(ProductId),ProductId) });
            }
            else
            {
                return NotFound();
            }

        }
    }
}