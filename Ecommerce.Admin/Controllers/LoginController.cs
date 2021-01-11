using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        public LoginController(IUserService userService,IUserProfileService userProfileService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Login(userLoginDto.Username, userLoginDto.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("userName", user.Username);
                    HttpContext.Session.SetString("avatar", user.Avatar);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.Message = "Thông tin đăng nhập không đúng.";
                    return View();
                }
            }

            return View();
            //var token = await _userService.Authenticate(userLoginDto.Username, userLoginDto.Password);
            //if (token == null)
            //    return Unauthorized();
            //return Ok(token);
            //return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserLoginDto userLoginDto)
        {
            var token = await _userService.Authenticate(userLoginDto.Username, userLoginDto.Password);
            if (token == null)
                return Unauthorized();
            Ok(token);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}