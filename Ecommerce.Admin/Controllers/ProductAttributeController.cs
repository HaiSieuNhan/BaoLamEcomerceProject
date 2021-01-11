using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using static EcommerceCommon.Infrastructure.Helper.Helper;

namespace Ecommerce.Admin.Controllers
{
    public class ProductAttributeController : Controller
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductSizeService _productSizeService;
        private readonly IProductColorService _productColorService;

        public ProductAttributeController(IProductAttributeService productAttributeService, IProductSizeService productSizeService, IProductColorService productColorService)
        {
            _productAttributeService = productAttributeService;
            _productSizeService = productSizeService;
            _productColorService = productColorService;
        }

        public async Task<IActionResult> GetProductAttributeList(Guid Id)
        {
            var model = await _productAttributeService.GetListProductAttributeAdminViewModel(Id);
            ViewBag.ProductId = Id;
            return View(model);
        }
        [NoDirectAccess]
        public async Task<IActionResult> Create(Guid ProductId)
        {
            var model = await _productAttributeService.GetAddProductAttributeModel();
            ViewBag.ProductId = ProductId;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddProductAttributeViewModel AddProductAttributeViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await _productAttributeService.AddProductAttributeAsync(AddProductAttributeViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewAndViewBagToString(this, "_ViewAll", await _productAttributeService.GetListProductAttributeAdminViewModel(AddProductAttributeViewModel.ProductId), AddProductAttributeViewModel.ProductId) });

                }
                else
                {
                    return NotFound();
                }
            }
            var model = await _productAttributeService.GetAddProductAttributeModel(AddProductAttributeViewModel);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = await _productAttributeService.GetEditProductAttributeModel(Id);
            if(model == null)
            {
                return NotFound();
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductAttributeViewModel EditProductAttributeViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await _productAttributeService.EditProductAttributeAsync(EditProductAttributeViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewAndViewBagToString(this, "_ViewAll", await _productAttributeService.GetListProductAttributeAdminViewModel(EditProductAttributeViewModel.ProductId), EditProductAttributeViewModel.ProductId) });

                }
                else
                {
                    return NotFound();
                }
            }
            var model = await _productAttributeService.GetEditProductAttributeModel(EditProductAttributeViewModel);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id,Guid ProductId)
        {
            if(await _productAttributeService.DeleteProductAttributeAsync(id))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewAndViewBagToString(this, "_ViewAll", await _productAttributeService.GetListProductAttributeAdminViewModel(ProductId), ProductId) });
            }
            else{
                return NotFound();
            }
        }


    }
}