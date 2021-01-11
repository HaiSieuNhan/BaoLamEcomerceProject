using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EcommerceCommon.Infrastructure.Helper.Helper;
using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using EcommerceCommon.Infrastructure.Ultil;
using Ecommerce.Service.ViewModels.Admin.ManufacturerModel;

namespace Ecommerce.Admin.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Manufacture
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ManufacturerController(IManufacturerService manufacturerService, IWebHostEnvironment hostEnvironment)
        {
            _manufacturerService = manufacturerService;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> GetManufacturerList()
        {
            var model = await _manufacturerService.GetManufacturerAdminViewModels();
            return View(model);
        }

        // GET: Manufacturer/Create
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddManufacturerViewModel AddManufacturerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _manufacturerService.AddManufacturerAsync(AddManufacturerViewModel, _hostEnvironment.WebRootPath))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _manufacturerService.GetManufacturerAdminViewModels()) });
                }
            } 
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", AddManufacturerViewModel) });
        }

        // GET: Manufacturer/Edit/
        public async Task<ActionResult> Edit(Guid Id)
        {
            var model = await _manufacturerService.GetEditManufacturerViewModel(Id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: Manufacturer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditManufacturerViewModel manufacturer)
        {
            if (ModelState.IsValid)
            {
                if(await _manufacturerService.EditManufacturerAsync(manufacturer, _hostEnvironment.WebRootPath))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _manufacturerService.GetManufacturerAdminViewModels()) });
                }
                else
                {
                    return NotFound();
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", manufacturer) });
        }

        // POST: Manufacturer/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _manufacturerService.DeleteManufacturerAsync(id, _hostEnvironment.WebRootPath))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _manufacturerService.GetManufacturerAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }

        }
    }
}

