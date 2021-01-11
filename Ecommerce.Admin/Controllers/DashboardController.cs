using System;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserProfileService _userProfileService ;



        public DashboardController(IProductService productService, IOrderService orderService, IUserProfileService userProfileService)
        {
            _productService = productService;
            _orderService = orderService;
            _userProfileService = userProfileService;

        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            //var baseUrl = "https://localhost:44329/weatherforecast";
            //var result = await baseUrl.AppendPathSegment("/get").GetJsonAsync<List<WeatherForecast>>();
            ViewBag.ThisMonth = DateTime.Now.Month;
            ViewBag.ThisYear = DateTime.Now.Year;
            ViewBag.TotalRevenueThisMonth = await _orderService.GetRevenueThisMonth();
            ViewBag.TotalRevenueThisYear = await _orderService.GetRevenueThisYear();
            ViewBag.TotalProduct = await _productService.GetTotalQuantityProductRemainAdmin();
            ViewBag.TotalQuantityProductOrdered = await _productService.GetTotalQuantityProductOrderedAdmin();
            ViewBag.NewOrders = await _orderService.GetOrderNewAdminViewModels();
            ViewBag.CountProcessOrder = await _orderService.GetCountOrderProcess();
            ViewBag.GetNewAccount = await _userProfileService.GetCountNewCustomer();
            return View();
        }
        public async Task<IActionResult> GetRevenueThisMonth()
        {
            var revenue = await _orderService.GetRevenueMonthAdminViewModels();
            var revenueJson = JsonConvert.SerializeObject(revenue);
            return Json(new { revenue = revenueJson});
        }
        public async Task<IActionResult> GetProductOrdered()
        {
            var model = await _productService.GetProductOrderedAdminViewModels();
            return View(model);
        }
        public async Task<IActionResult> GetProductRemain()
        {
            var model = await _productService.GetProductRemainAdminViewModels();
            return View(model);
        }

        public async Task<IActionResult> GetOrderProcess()
        {
            var model = await _orderService.GetOrderProcessAdminViewModels();
            return View(model);
        }

        public async Task<IActionResult> GetNewCustomer()
        {
            var model = await _userProfileService.GetNewCustomerAdminViewModels();
            return View(model);
        }



    }
}