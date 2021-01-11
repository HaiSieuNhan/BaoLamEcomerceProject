using Ecommerce.Admin.ImportSuppliers;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.SupplierModel;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static EcommerceCommon.Infrastructure.Helper.Helper;

namespace Ecommerce.Admin.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IWebHostEnvironment _hostEnvironment;
       // private readonly IImportSuppliersExcelService _importSuppliersExcelService;
        public SupplierController(ISupplierService supplierService, IWebHostEnvironment hostEnvironment)
        {
            _supplierService = supplierService;
            _hostEnvironment = hostEnvironment;
            //_importSuppliersExcelService = importSuppliers;
        }
        public async Task<IActionResult> GetSupplierList()
        {
            var model = await _supplierService.GetSupplierAdminViewModels();
            return View(model);
        }

        // GET: Supplier/Create
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Import()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Import(IFormFile file)
        {

            if (file != null)
            {
                var listSupplier = await SaveImportFormFile(file);
                foreach (var item in listSupplier)
                {
                    var model = new Supplier();
                    model.Id = Guid.NewGuid();
                    model.CreatedDate = DateTime.Now;
                    model.Name = item.Name;
                    model.CodeName = item.CodeName;
                    model.Email = item.Email;
                    model.Phone = item.Phone;
                    model.Fax = item.Fax;
                    model.Sort = item.Sort;
                    model.IsDeleted = false;

                    await _supplierService.AddAsync(model);
                }
                return RedirectToAction(nameof(GetSupplierList));
            }
            return RedirectToAction(nameof(Import));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddSupplierViewModel AddSupplierViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _supplierService.AddSupplierAsync(AddSupplierViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _supplierService.GetSupplierAdminViewModels()) });
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", AddSupplierViewModel) });
        }

        // GET: Supplier/Edit/
        public async Task<ActionResult> Edit(Guid Id)
        {
            var model = await _supplierService.GetEditSupplierViewModel(Id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: Supplier/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSupplierViewModel EditSupplierViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _supplierService.EditSupplierAsync(EditSupplierViewModel))
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _supplierService.GetSupplierAdminViewModels()) });
                }
                else
                {
                    return NotFound();
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", EditSupplierViewModel) });
        }

        // POST: Supplier/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _supplierService.DeleteSupplierAsync(id))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _supplierService.GetSupplierAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }
        }



        private async Task<List<ImportSuppliersExcelData>> ImportFile(FileInfo file)
        {
            var dataExcell = await GetDataExcell(file);

            var supplierModels = await ValidateData(dataExcell);
                return supplierModels;
            
         
        }

        private async Task<List<ImportSuppliersExcelData>> ValidateData(List<ImportSuppliersExcelData> dataExcell)
        {
            var supplier = new List<ImportSuppliersExcelData>();
            foreach (var item in dataExcell)
            {
                var Supplier = new ImportSuppliersExcelData();

                if (string.IsNullOrEmpty(item.Name))
                {
                    Supplier.IsValid = false;
                    Supplier.Errors = " Không nhập fullName,";
                }
                Supplier.Name = item.Name;

                if (string.IsNullOrEmpty(item.CodeName))
                {
                    Supplier.Errors += " Không nhập mã người dùng,";
                    Supplier.CodeName = " Không nhập mã";
                }
                else
                {
                    Supplier.CodeName = item.CodeName;
                }

                if (string.IsNullOrEmpty(item.Email))
                {
                    Supplier.IsValid = false;
                    Supplier.Errors += " Không nhập Email,";
                }
                Supplier.Email = item.Email;

                if (string.IsNullOrEmpty(item.Phone))
                {
                    Supplier.IsValid = false;
                    Supplier.Errors += " Không nhập Số điện thoại,";
                }
                Supplier.Phone = item.Phone;

                Supplier.CreateDate = DateTime.Now;
                Supplier.Id = Guid.NewGuid();
                supplier.Add(Supplier);
            }
            return supplier;
        }

        private async Task<List<ImportSuppliersExcelData>> GetDataExcell(FileInfo file)
        {
            var importExcelDatas = new List<ImportSuppliersExcelData>();
            using (var package = new ExcelPackage(file))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                if (worksheet == null)
                {
                    return importExcelDatas;
                }
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = 3; row <= rowCount; row++)
                {
                    importExcelDatas.Add(new ImportSuppliersExcelData
                    {

                        Name = worksheet.Cells[row, 1].GetValue<string>()?.Trim(),
                        CodeName = worksheet.Cells[row, 2].GetValue<string>()?.Trim(),
                        Email = worksheet.Cells[row, 3].GetValue<string>()?.Trim(),
                        Phone = worksheet.Cells[row, 4].GetValue<string>()?.Trim(),
                        Fax = worksheet.Cells[row, 5].GetValue<string>()?.Trim(),
                        Sort = int.Parse(worksheet.Cells[row, 6].GetValue<string>()?.Trim()),
                    });
                }
            }

            return importExcelDatas;
        }

        private async Task<List<ImportSuppliersExcelData>> SaveImportFormFile(IFormFile file)
        {
            var filePath = Directory.GetCurrentDirectory() + "\\Excel\\";
            var fileName = $"Supplier-{DateTime.Now.Ticks}.xlsx";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += fileName;
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                if (fileStream != null)
                {
                    await file.CopyToAsync(fileStream);
                }
                
            }
            FileInfo fileInfo = new FileInfo(filePath);

            return await ImportFile(fileInfo);
        }
    }

}
