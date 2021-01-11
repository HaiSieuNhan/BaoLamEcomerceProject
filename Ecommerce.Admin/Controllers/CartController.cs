using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Admin.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;
        public CartController(ICartService cartService, ICartDetailService cartDetailService)
        {
            _cartService = cartService;
            _cartDetailService = cartDetailService;
        }
        public async Task<IActionResult> GetCartList()
        {
            var models = await _cartService.GetCartAdminViewModels();
            return View(models);
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            var models = await _cartService.GetCartAdminViewModelById(Id);

            return View(models);
        }
    }
}
