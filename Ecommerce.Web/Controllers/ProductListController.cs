using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Web.ProductList;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Web.Controllers
{
    public class ProductListController : Controller
    {
        // GET: ProductListController
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductColorService _productColorService;
        private readonly IProductSizeService _productSizeService;
        public ProductListController(ICategoryService categoryService, IProductService productService, IProductColorService productColorService, IProductSizeService productSizeService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _productColorService = productColorService;
            _productSizeService = productSizeService;
        }
        public async Task<IActionResult> GetProductByCategory(Guid ID)
        {
            var products = await _productService.GetProductByCategory(ID);
            var categoryProductList = await _categoryService.GetCategoryProductList(ID);
            var listCategoryProductList = await _categoryService.GetListCategoryProductList(categoryProductList.ParentId);
            var listColor = await _productColorService.GetListProductColorViewModel();
            var listSize = await _productSizeService.GetListProductSizeViewModel();
            foreach(var item in listCategoryProductList)
            {
                 item.ProductCount = _productService.GetProductByCategory(item.Id).Result.Count;
                foreach(var item1 in item.CategoryChildren)
                {
                    item1.ProductCount = _productService.GetProductByCategory(item1.Id).Result.Count;
                }
            }
            var models = new ProductListModel()
            {
                ProductHomepage = products,
                CategoryProductListViewModel = categoryProductList,
                ListCategoryProductListViewModel = listCategoryProductList,
                ListProductColorViewModel = listColor,
                ListProductSizeViewModel = listSize
            };
            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductPagination(Guid CategoryId, int skip, int take)
        {
            var listProduct = await _productService.GetProductByCategoryPagination(CategoryId,skip,take);
            string listProductJson = JsonConvert.SerializeObject(listProduct);
            return Json(new { listProduct = listProductJson });
        }


    }
}
