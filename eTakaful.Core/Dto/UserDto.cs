using EcommerceCommon.Infrastructure.Attribute;
using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Service.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập họ tên")]
        [DisplayName("Họ tên")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên đăng nhập")]
        [UserName(ErrorMessage = "Tên đăng nhập có ít nhất 6 và nhiều nhất 32 ký tự bắt đầu bằng chữ cái")]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Vui lòng nhập lại Email")]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [DisplayName("Giới tính")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập chọn ngày sinh")]
        [DisplayName("Ngày sinh")]
        public string Birthday { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        [Password(ErrorMessage = "Mật khẩu có ít nhất 6 và nhiều nhất 32 ký tự")]
        public string Password { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}
