using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Admin.Controllers
{
    public class ProductCommentController : Controller
    {
        private readonly IProductCommentService _productcommentService;
        public ProductCommentController(IProductCommentService productcommentService)
        {
            _productcommentService = productcommentService;
        }
        public async Task<IActionResult> GetProductCommentList()
        {
            var models = await _productcommentService.GetProductCommentListViewModel();
            return View(models);
        }

    }
}