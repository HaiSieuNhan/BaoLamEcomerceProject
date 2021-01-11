using EcommerceCommon.Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class Supplier :BaseModel
    {
        [MaxLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [DisplayName("Tên nhà cung cấp")]
        public string Name { get; set; }
        [MaxLength(255)]
        [DisplayName("Mã nhà cung cấp")]
        public string CodeName { get; set; }
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập lại Email")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [MaxLength(30)]
        [PhoneVN(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [MaxLength(30)]
        [DisplayName("Fax")]
        public string Fax { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
