using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartDetailService _cartDetailService;

        public CheckOutController(IOrderService orderService, ICartDetailService cartDetailService)
        {
            _orderService = orderService;
            _cartDetailService = cartDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = await _orderService.GetCheckOutViewModel(new Guid(userId));
                return View(model);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutInfoViewModel CheckOutInfoViewModel)
        {
            var userId = HttpContext.Session.GetString("userId");
            var cartCount = HttpContext.Session.GetString("cartCount");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }else if(cartCount == null || cartCount == "0")
            {
                return RedirectToAction("GetCart", "Cart");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if(await _orderService.CreateOrder(CheckOutInfoViewModel , new Guid(userId)))
                    {
                        HttpContext.Session.SetString("cartCount", "0");
                        return RedirectToAction("CheckOutSuccess", "CheckOut");
                    }
                    else
                    {
                        return RedirectToAction("GetCart", "Cart");
                    }
                }
                else
                {
                    var model = await _orderService.GetCheckOutViewModel(CheckOutInfoViewModel, new Guid(userId));
                    return View(model);
                }
            }
        }
        public async Task<IActionResult> CheckOutSuccess()
        {
            return View();
        }
    }
}