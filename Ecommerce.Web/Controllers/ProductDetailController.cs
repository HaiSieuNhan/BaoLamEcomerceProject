using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.Ultil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ecommerce.Web.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProductImageService _productImageService;
        private readonly IProductCommentService _productCommentService;
        private readonly IProductCommentReplyService _productCommentReplyService;
        private readonly IProductCommentImageService _productCommentImageService;
        private readonly IProductSizeService _productSizeService;
        private readonly IProductColorService _productColorService;
        public ProductDetailController(IProductService productService, IProductAttributeService productAttributeService,IProductCommentService productCommentService
            , IProductCommentImageService productCommentImageService, IWebHostEnvironment hostEnvironment, IProductImageService productImageService
            , IProductCommentReplyService productCommentReplyService, IProductSizeService productSizeService, IProductColorService productColorService)
        {
            _productService = productService;
            _productAttributeService = productAttributeService;
            this._hostEnvironment = hostEnvironment;
            _productImageService = productImageService;
            _productCommentService = productCommentService;
            _productCommentImageService = productCommentImageService;
            _productCommentReplyService = productCommentReplyService;
            _productSizeService = productSizeService;
            _productColorService = productColorService;
        }
        public async Task<IActionResult> GetProductDetail(Guid ProductAttributeID)
        {
            var products = await _productService.GetProductFirstAttributeByProductAttribute(ProductAttributeID);
            var productImage = await _productImageService.GetProductImageFirstAttribute(ProductAttributeID);
            var productComment = await _productCommentService.GetProductCommentByProductId(products.ProductId);
            var productRating = await _productCommentService.GetProductRatingViewModel(products.ProductId);
            var productColor = await _productColorService.GetProductColorByProductId(products.ProductId);
            var productSize = await _productSizeService.GetProductSizeByProductId(products.ProductId);
            var productSizeFirstAttribute = await _productSizeService.GetListProductSizeByProductColor(products.ProductId,products.ProductColorId);
            var models = new ProductDetailModel()
            {
                ProductFirstAttributeViewModel = products,
                ProductImageFirstAttributeViewModel = productImage,
                ProductComment = new ProductComment(),
                ProductCommentViewModel =productComment,
                ProductRatingViewModel = productRating,
                ProductCommentReply = new ProductCommentReply(),
                ProductSizeViewModel = productSize,
                ProductSizeFirstAttributeViewModel = productSizeFirstAttribute,
                ProductColorViewModel = productColor
            };
            return View(models);
        }
        public async Task<IActionResult> AddProductComment(ProductComment productComment)
        {
            if (productComment.UserId == Guid.Empty)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Lưu ảnh vào thư mục wwwroot/image
                    await _productCommentService.AddAsync(productComment);
                    if (productComment.ImageFiles != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var item in productComment.ImageFiles)
                        {
                            var productCommentImage = new ProductCommentImage();
                            productCommentImage.ImageLink = await Ultil.UploadFileAsync(item, wwwRootPath, "images");
                            productCommentImage.ProductCommentId = productComment.Id;
                            await _productCommentImageService.AddAsync(productCommentImage);
                        }

                    }
                    return Json(new { isValid = true, html = "Nhận xét của bạn đã thành công chờ xét duyệt" });
                }
                return Json(new { isValid = false, html = "Nhận xét thất bại" });
            }
            
        }
        public async Task<IActionResult> AddProductCommentReply(ProductCommentReply productCommentReply)
        {
            if (productCommentReply.UserId == Guid.Empty)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    await _productCommentReplyService.AddAsync(productCommentReply);
                    
                    return Json(new { isValid = true, html = "Nhận xét của bạn đã thành công chờ xét duyệt" });
                }
                return Json(new { isValid = false, html = "Nhận xét thất bại" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ViewMore(int SkipNum,Guid productCommentId)
        {
            var listReplyComment = await _productCommentReplyService.GetMoreReplyComment(SkipNum, productCommentId);
            string listReplyJson = JsonConvert.SerializeObject(listReplyComment, new IsoDateTimeConverter() { DateTimeFormat = "M/d/yyyy h:mm:ss tt" });
            return Json(new { isValid = true, html = listReplyJson });
        }
        [HttpGet]
        public async Task<IActionResult> GetProductCommentPagination(Guid productId,int skip,int take)
        {
            var listProductComment = await _productCommentService.GetProductCommentPagination(productId, skip, take);
            string listProductCommentJson = JsonConvert.SerializeObject(listProductComment, new IsoDateTimeConverter() { DateTimeFormat = "M/d/yyyy h:mm:ss tt" });
            return Json(new { isValid = true, html = listProductCommentJson });
        }
        [HttpGet]
        public async Task<IActionResult> GetProductDetailByProductColor(Guid productId, Guid productColorId)
        {
            var listImage = await _productImageService.GetProductImageByProductColor(productId, productColorId);
            var productDetail = await _productAttributeService.GetProductAttributeByProductColor(productId, productColorId);
            var listSize = await _productSizeService.GetListProductSizeByProductColor(productId, productColorId);
            var listAllSize = await _productSizeService.GetProductSizeByProductId(productId);
            string listImageJson = JsonConvert.SerializeObject(listImage);
            string productDetailJson = JsonConvert.SerializeObject(productDetail);
            string listSizeJson = JsonConvert.SerializeObject(listSize);
            string listAllSizeJson = JsonConvert.SerializeObject(listAllSize);
            return Json(new { listImage = listImageJson ,productDetail= productDetailJson,listSize=listSizeJson, listAllSize = listAllSizeJson });
        }
        [HttpGet]
        public async Task<IActionResult> GetProductDetailByProductSize(Guid productId, Guid? productColorId,Guid productSizeId)
        {
            var productDetail = await _productAttributeService.GetProductAttributeByProductSize(productId, productColorId,productSizeId);
            string productDetailJson = JsonConvert.SerializeObject(productDetail);
            return Json(new { productDetail = productDetailJson });
        }
    }
}