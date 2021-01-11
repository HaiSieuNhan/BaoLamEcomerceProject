using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Web.Header;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.ViewComponents
{
    public class MenuViewComponent :ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public MenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesLevel0 = await _categoryService.GetCategoryLevel0();
            var models = new HeaderModel()
            {
                CategoryLevel0ViewModel = categoriesLevel0,
            };
            return View(models);
        }
    }
}
