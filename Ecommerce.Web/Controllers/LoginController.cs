using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.EmailService;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IEmailSender _emailSender;
        private readonly ICartDetailService _cartDetailService;
        public LoginController(IProductService productService, IUserService userService,
            IUserProfileService userProfileService, ICartDetailService cartDetailService, IEmailSender emailSender)
        {
            _productService = productService;
            _userService = userService;
            _userProfileService = userProfileService;
            _emailSender = emailSender;
            _cartDetailService = cartDetailService;
        }
        public async Task<IActionResult> Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user=await _userService.Register(userDto,Role.Customer);
                if (user == null)
                {
                    ViewBag.Message = "Tên đăng nhập đã tồn tại";
                    return View();
                }
                // Tạo ra đường link để active
               // // cấu trúc url => http://hostname/Login/ActiveAccountByEmail?username=usename
               // string domainName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

               // var url = domainName +"/Login/ActiveAccountByEmail?username=" +user.Username;
               // var content = "Click vào link dưới đây để kích hoạt tài khoản " + url;
               // // send email
               // var message = new EmailService.Message(new string[]
               //         { userDto.Email },
               //"Test email", content, null);

               // await _emailSender.SendEmailAsync(message);
                return RedirectToAction("RegisterSuccess","Login"/*,new {email =userDto.Email }*/);
            }
            //return Json(new { isValid = false, html = Helper.RenderRazorViewToString });
            return View();
        }
        public async Task<IActionResult> RegisterSuccess(string email)
        {
            ViewBag.Email = email;
            return View();
            
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
                    var cartCount = await _cartDetailService.GetCartCount(user.Id);
                    HttpContext.Session.SetString("userName", user.Username);
                    HttpContext.Session.SetString("customerName", user.Name);
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("cartCount", cartCount.ToString());
                    //HttpContext.Session.SetString("Avatar", user.Avatar);
                    //HttpContext.Session.SetString("Name", user.Name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Thông tin đăng nhập không đúng.";
                    return View();
                }
            }

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ActiveAccountByEmail(string username)
        {
            var isActive = await _userService.ActiveUser(username);
            //if (isActive == false)
            //{
            //    // return view
            //}
            return View();

        }

    }
}
