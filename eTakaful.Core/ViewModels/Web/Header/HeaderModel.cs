using Ecommerce.Service.Dto;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.Header
{
    public class HeaderModel
    {
        public IList<CategoryLevel0ViewModel> CategoryLevel0ViewModel { get; set; }
        public UserDto UserDto {get;set;}
    }
}
