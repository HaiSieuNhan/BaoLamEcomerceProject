using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcommerceCommon.Infrastructure.Helper.Helper;
using EcommerceCommon.Infrastructure.Ultil;
using Ecommerce.Service.ViewModels.Admin.CategoryModel;

namespace Ecommerce.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private readonly ICategoryService _categoryService;
        private readonly ICollectionService _collectionService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(ICategoryService categoryService, ICollectionService collectionService, IWebHostEnvironment hostEnvironment)
        {
            _categoryService = categoryService;
            _collectionService = collectionService;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> GetCategoryList()
        {
            var model = await _categoryService.GetCategoryAdminViewModels();
            return View(model);
        }


        // GET: Category/Create
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            var model = await _categoryService.GetAddCategoryModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCategoryViewModel AddCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await _categoryService.AddCategoryAsync(AddCategoryViewModel, _hostEnvironment.WebRootPath))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _categoryService.GetCategoryAdminViewModels()) });
                }
            }
            var model = await _categoryService.GetAddCategoryModel(AddCategoryViewModel);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await _categoryService.GetEditCategoryModel(id);
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCategoryViewModel EditCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                if( await _categoryService.EditCategoryAsync(EditCategoryViewModel, _hostEnvironment.WebRootPath))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _categoryService.GetCategoryAdminViewModels()) });
                }
                else
                {
                    return NotFound();
                }

            }
            var model = await _categoryService.GetEditCategoryModel(EditCategoryViewModel);
            if (model == null)
                return NotFound();
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", model) });
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {

            if(await _categoryService.DeleteCategoryAsync(id, _hostEnvironment.WebRootPath))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _categoryService.GetCategoryAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }

        }
    }
}