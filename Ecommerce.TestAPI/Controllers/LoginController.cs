using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.TestAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        public LoginController(IUserService userService, IUserProfileService userProfileService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }

        [HttpPost("authenticate")]
        public async Task <IActionResult> Authenticate([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _userService.Authenticate(userLoginDto.Username, userLoginDto.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}