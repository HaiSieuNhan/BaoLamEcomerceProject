using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Web.Models;
using Ecommerce.Service.Interface;
using Ecommerce.Service.Dto;
using Ecommerce.EmailService;
using EcommerceCommon.Infrastructure.Helper;
using Ecommerce.Service.ViewModels.Web.Homepage;
using System;
using Newtonsoft.Json;
using Ecommerce.Service.ViewModels.Web.ProductList;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ICollectionService _collectionService;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;


        public HomeController(ICategoryService categoryService, IProductService productService, IEmailSender emailSender, ICollectionService collectionService, IUserService userService, IProductAttributeService productAttributeService)
        {
            _collectionService = collectionService;
            _categoryService = categoryService;
            _productService = productService;
            _emailSender = emailSender;
            _userService = userService;
            _productAttributeService = productAttributeService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var collection = await _collectionService.GetCollectionHomepage();
            var listCollection = await _collectionService.GetListCollectionHomepage();
            var products = await _productService.GetProductTopView(searchString);
            
            var models = new HomepageModel()
            {
                ProductHomepage = products,
                CollectionHomepageViewModel =collection,
                CollectionViewModel = listCollection
            };

            return View(models);

        }

        public async Task<IActionResult> AllProduct(string searchString)
        {

            var Listproducts = await _productService.GetAllProduct(searchString);
            var models = new GetListProduct()
            {
                ProductAhihi = Listproducts
            };
            return View(models);

        }


        [HttpGet]
        public async Task<IActionResult> GetProductHomepageByProductAttribute(Guid productAttributeId)
        {
            var productHomepage = await _productAttributeService.GetProductHomepageByProductAttribute(productAttributeId);
            string productHomepageJson = JsonConvert.SerializeObject(productHomepage);
            return Json(new { productHomepage = productHomepageJson });
        }
    }
}
