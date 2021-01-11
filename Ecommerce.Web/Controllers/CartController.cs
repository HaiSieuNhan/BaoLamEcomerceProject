using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductAttributeService _productAttributeService;

        public CartController(ICartService cartService,IProductAttributeService productAttributeService, ICartDetailService cartDetailService)
        {
            _cartService = cartService;
            _productAttributeService = productAttributeService;
            _cartDetailService = cartDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = HttpContext.Session.GetString("userId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = await _cartService.GetCartViewModelByUserId(new Guid(userId));
                return View(model);
            }
            
        }        
        [HttpPost]
        public async Task<IActionResult> AddCart(Guid ProductId ,Guid? ProductSizeId,Guid? ProductColorId,int quantity)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if(await _cartService.AddCart(ProductId,ProductSizeId, ProductColorId, quantity, new Guid(userId))){
                    var cartCount = await _cartDetailService.GetCartCount(new Guid(userId));
                    HttpContext.Session.SetString("cartCount", cartCount.ToString());
                    return Json(new { isValid = true,html = cartCount});
                }
                else
                {
                    var product = await _productAttributeService.GetProductAddCartError(ProductId, ProductSizeId, ProductColorId);
                    string msg = "";
                    if (product.CountStock > 0)
                    {
                        msg = "Sản phẩm " + product.ProductName;
                        if(product.Color != null)
                        {
                            msg += " màu " + product.Color;
                        }
                        if(product.Size != null)
                        {
                            msg += " cỡ " + product.Size;
                        }
                        msg +=" chỉ còn " + product.CountStock + " sản phẩm!";
                    }
                    else
                    {
                        msg = "Sản phẩm đã hết hàng !";
                    }
                    return Json(new { isValid = false, html = msg });
                }
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart(Guid CartDetailId, int Quantity)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var validate = await _cartService.UpdateCart(CartDetailId, Quantity);
                var cart = await _cartService.GetCartViewModelByUserId(new Guid(userId));
                var cartJson = JsonConvert.SerializeObject(cart);
                var cartCount = await _cartDetailService.GetCartCount(new Guid(userId));
                HttpContext.Session.SetString("cartCount", cartCount.ToString());
                if (validate.IsValid)
                {                    
                    return Json(new {isValid = true, cart = cartJson, cartCount = cartCount });
                }
                else 
                {
                    return Json(new { isValid = false, cart = cartJson, cartCount = cartCount, msg = validate.Message ,quantity = validate.Quantity});
                }
                
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(Guid CartDetailId)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var validate = await _cartService.DeleteCart(CartDetailId);
                var cart = await _cartService.GetCartViewModelByUserId(new Guid(userId));
                var cartJson = JsonConvert.SerializeObject(cart);
                var cartCount = await _cartDetailService.GetCartCount(new Guid(userId));
                HttpContext.Session.SetString("cartCount", cartCount.ToString());
                
                if (validate.IsValid)
                {
                    return Json(new { isValid = true, cart = cartJson, cartCount = cartCount });
                }
                else
                {
                    return Json(new { isValid = false, cart = cartJson, cartCount = cartCount, msg = validate.Message});
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(string CodeName)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var validate = await _cartService.ApplyCoupon(CodeName, new Guid(userId));
                var cart = await _cartService.GetCartViewModelByUserId(new Guid(userId));
                var cartJson = JsonConvert.SerializeObject(cart);
                if (validate.IsValid)
                {
                    return Json(new { isValid = true, cart = cartJson });
                }
                else
                {
                    return Json(new { isValid = false, cart = cartJson, msg = validate.Message});
                }
            }
        }
    }
}