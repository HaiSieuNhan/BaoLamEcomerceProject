using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Admin.Controllers
{
    public class StaffController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public StaffController(IUserProfileService userProfileService, IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            _userProfileService = userProfileService;
            _userService = userService;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> GetStaffList()
        {
            var models = await _userProfileService.GetStaffAdminViewModels();
            return View(models);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto staff)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Register(staff,Role.Staff);
                if (user == null)
                {
                    var message = "Tên đăng nhập đã tồn tại";
                    return Json(new { isValid = false, html = Helper.RenderRazorViewAndMessageToString(this, "Create", staff, message) });
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _userProfileService.GetStaffAdminViewModels()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", staff) });
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = await _userProfileService.GetUserProfileDto(Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileDto staff)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                await _userProfileService.UpdateUserProfile(staff,wwwRootPath);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _userProfileService.GetStaffAdminViewModels()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", staff) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var staff = await _userService.GetByIdAsync(id);
            await _userService.DeleteUser(staff,wwwRootPath);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _userProfileService.GetStaffAdminViewModels()) });
        }
    }
}
