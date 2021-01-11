using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Portal.Infrastructure.Wrappers;
using Ecommerce.Service.Dto;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {  
        private readonly AutoMapper.IMapper _mapper;

        public RoleController( IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("create-role")]
        public ApiResponse CreateRole([FromBody] RoleDto dto)
        {
            try
            {
                // save 
                return new ApiResponse("Add role success", 200);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return new ApiResponse("Can't add role", ex, 400);
            }
        }
    }
}