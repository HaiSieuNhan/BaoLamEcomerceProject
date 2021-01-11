using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using Ecommerce.Service.ViewModels.Admin.ProductImageModel;
using Ecommerce.Service.ViewModels.Admin.ProductModel;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.Ultil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ecommerce.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductImageService _productImageService;
        private readonly IProductSizeService _productSizeService;
        private readonly IProductColorService _productColorService;
        private readonly ICollectionService _collectionService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ISupplierService _supplierService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IProductService productService, ICollectionService collectionService, ICategoryService categoryService, IManufacturerService manufacturerService, ISupplierService supplierService, IWebHostEnvironment hostEnvironment, IProductSizeService productSizeService, IProductColorService productColorService, IProductAttributeService productAttributeService, IProductImageService productImageService)
        {
            _productService = productService;
            _collectionService = collectionService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _supplierService = supplierService;
            _hostEnvironment = hostEnvironment;
            _productSizeService = productSizeService;
            _productColorService = productColorService;
            _productAttributeService = productAttributeService;
            _productImageService = productImageService;
        }
        public async Task<IActionResult> GetProductList()
        {
            var models = await _productService.GetProductListAdminViewModel();
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetAddProductModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductViewModel AddProductViewModel, List<AddProductAttributeViewModel> listAttribute, List<AddProductImageViewModel> listImage)
        {
            if (ModelState.IsValid)
            {
                if(await _productService.CreateProduct(AddProductViewModel, listAttribute, listImage, _hostEnvironment.WebRootPath))
                {
                    return RedirectToAction("GetProductList", "Product");
                }
                else
                {
                    return NotFound();
                }
            }
            var model = await _productService.GetAddProductModel(AddProductViewModel);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = await _productService.GetEditProductModel(Id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel EditProductViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _productService.EditProduct(EditProductViewModel))
                {
                    return RedirectToAction("GetProductList", "Product");
                }
                else
                {
                    return NotFound();
                }

            }
            var model = await _productService.GetEditProductModel(EditProductViewModel);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (await _productService.DeleteProduct(Id, _hostEnvironment.WebRootPath))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _productService.GetProductListAdminViewModel()) });
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> UploadCKEditor(IFormFile upload)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = await Ultil.UploadFileAsync(upload, wwwRootPath, "uploads");
            return new JsonResult(new
            {
                uploaded = 1,
                fileName = fileName,
                url = "/uploads/" + fileName
            });
        }
        public IActionResult FileBrowse()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), _hostEnvironment.WebRootPath, "uploads"));
            ViewBag.fileInfos = dir.GetFiles();
            return View();
        }
        [HttpGet]
        [Route("product/detail/{Id?}")]
        public async Task<IActionResult> Detail(Guid Id)
        {
            var model = await _productService.GetProductDetailAdminViewModel(Id);
            return View(model);
        }
    }
}