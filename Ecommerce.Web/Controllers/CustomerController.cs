using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Web.Customer;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.Web.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IOrderService _orderService;
        private readonly IOrderHistoryService _orderHistoryService;

        public CustomerController(IUserProfileService userProfileService, IOrderService orderService, IOrderHistoryService orderHistoryService)
        {
            _userProfileService = userProfileService;
            _orderService = orderService;
            _orderHistoryService = orderHistoryService;
        }

        // GET: /<controller>/
        [Route("profile/edit")]
        public async Task<IActionResult> EditCustomerProfile()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = await _userProfileService.GetCustomerProfileViewModel(new Guid(userId));
                return View(model);
            }            
        }
        [HttpPost]
        [Route("profile/edit")]
        public async Task<IActionResult> EditCustomerProfile(CustomerProfileViewModel profile)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var validate = await _userProfileService.UpdateCustomerProfile(new Guid(userId),profile);
                    return Json(new { isValid = validate.IsValid, msg = validate.Message, html = Helper.RenderRazorViewToString(this, "EditCustomerProfile", profile) });
                }

                return Json(new { isValid = false ,msg = "Có lỗi trong quá trình cập nhật", html = Helper.RenderRazorViewToString(this, "EditCustomerProfile", profile) });
            }            
        }
        [Route("order/history")]
        public async Task<IActionResult> GetOrderHistory()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = await _orderService.GetCustomerOrderViewModels(new Guid(userId));
                return View(model);
            }
        }
        [Route("order/view/{code}")]
        public async Task<IActionResult> GetOrderHistoryDetail(string Code)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = await _orderService.GetCustomerOrderViewModelByCode(Code);
                return View(model);
            }
        }
        [Route("order/tracking/{code}")]
        public async Task<IActionResult> GetOrderTracking(string Code)
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = await _orderHistoryService.GetCustomerOrderHistoryModel(Code);
                ViewBag.Code = Code;
                return View(model);
            }
        }
    }
}
