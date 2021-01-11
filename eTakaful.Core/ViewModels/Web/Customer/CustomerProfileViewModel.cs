using EcommerceCommon.Infrastructure.Attribute;
using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.Customer
{
    public class CustomerProfileViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Birthday { get; set; }

    }
}
