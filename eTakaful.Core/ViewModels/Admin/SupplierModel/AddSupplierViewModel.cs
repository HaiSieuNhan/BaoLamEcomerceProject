using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.Attribute;

namespace Ecommerce.Service.ViewModels.Admin.SupplierModel
{
    public class AddSupplierViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [DisplayName("Tên nhà cung cấp")]
        public string Name { get; set; }
        [DisplayName("Mã nhà cung cấp")]
        public string CodeName { get; set; }
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập lại Email")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [DisplayName("Fax")]
        public string Fax { get; set; }
    }
}
