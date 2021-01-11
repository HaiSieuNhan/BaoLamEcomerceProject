using EcommerceCommon.Infrastructure.Attribute;
using EcommerceCommon.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Service.Dto
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        [DisplayName("Họ tên")]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [DisplayName("Giới tính")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chọn ngày sinh")]
        [DisplayName("Ngày sinh")]
        public string Birthday { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Ảnh đại diện")]
        public IFormFile ImageFile { get; set; }
    }
}
