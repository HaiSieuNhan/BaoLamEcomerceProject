using EcommerceCommon.Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CheckOutInfoViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PaymentMethod { get; set; }
    }
}
