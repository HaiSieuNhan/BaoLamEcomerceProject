using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {

        private readonly IManufacturerService _manufacturerService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandViewComponent(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var models = await _manufacturerService.GetManufacturerHomepage();
            return View(models);
        }
    }
}
