using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.OrderModel;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Admin.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderHistoryService _orderHistoryService;
        public OrderController(IOrderService orderService, IOrderHistoryService orderHistoryService)
        {
            _orderService = orderService;
            _orderHistoryService = orderHistoryService;
        }
        [Route("report")]
        public async Task<IActionResult> Report()
        {
            ViewBag.ThisMonth = DateTime.Now.Month;
            ViewBag.ThisYear = DateTime.Now.Year;
            ViewBag.ThisDay = DateTime.Now.Day;
            ViewBag.TotalRevenueThisMonth = await _orderService.GetRevenueThisMonth();
            ViewBag.TotalRevenueThisYear = await _orderService.GetRevenueThisYear();
            ViewBag.TotalRevenuaThisDay = await _orderService.GetRevenueThisDay();
            var xxx = await _orderService.GetRevenueThisDay();
            var month = await _orderService.GetRevenueThisMonth();
            return View();
        }
        [Route("get-list-order")]
        public async Task<IActionResult> GetOrderList()
        {
            var models = await _orderService.GetOrderAdminViewModels();
            return View(models);
        }
        [Route("info/{code}")]
        public async Task<IActionResult> GetOrderInfo(string Code)
        {
            var models = await _orderService.GetOrderInfoAdminViewModelByCode(Code);
            return View(models);
        }
        [Route("history/{code}")]
        public async Task<IActionResult> GetOrderHistory(string Code)
        {
            var models = await _orderHistoryService.GetCustomerOrderHistoryModel(Code);
            return View(models);
        }
        [Route("edit/{code?}")]
        public async Task<IActionResult> Edit(string Code)
        {
            var model = await _orderService.GetEditOrderViewModel(Code);
            return View(model);
        }
        [Route("edit/{code?}")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditOrderViewModel editOrderViewModel)
        {
            if(await _orderService.EditOrderAsync(editOrderViewModel))
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _orderService.GetOrderAdminViewModels()) });
            }
            else
            {
                return NotFound();
            }
            
        }

        [Route("report-month")]
        public async Task<IActionResult> ReportMonth()
        {
            var revenue = await _orderService.GetRevenueMonthAdminViewModels();
            return View(revenue);
        }
        [Route("report-day")]
        public async Task<IActionResult> ReportDay()
        {
            var revenue = await _orderService.GetRevenueDayAdminViewModels();
            return View(revenue);
        }
        [Route("report-year")]
        public async Task<IActionResult> ReportYear()
        {
            var revenue = await _orderService.GetRevenueYearAdminViewModels();
            return View(revenue);
        }
    }
}