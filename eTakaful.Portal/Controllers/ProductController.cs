using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        //[HttpGet("get-product-by-id")]
        //public async Task<ProductViewModel> GetProductById(Guid id)
        //{
        //    var product = await _productService.GetByIdAsync(id);
        //    return _mapper.Map<ProductViewModel>(product);           
        //}

        /*[HttpGet("get-product-by-categoryid-orderby-view")]
        public async Task<List<ProductViewModel>> GetProductByCategoryIdAndOrderByView(Guid categoryId)
        {
            var product = await _productService.GetProductByCategoryIdAndOrderByView(categoryId);
            return _mapper.Map<List<ProductViewModel>>(product);
        }*/

    }
}