using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EcommerceCommon.Infrastructure.Helper.Helper;

namespace Ecommerce.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerList
        private readonly IUserProfileService _customerListService;
        public CustomerController(IUserProfileService customerListService)
        {
            _customerListService = customerListService;
        }
        public async Task<IActionResult> GetCustomerList()
        {
            var models = await _customerListService.GetCustomerAdminViewModels();
            return View(models);
        }

      

    }
}
