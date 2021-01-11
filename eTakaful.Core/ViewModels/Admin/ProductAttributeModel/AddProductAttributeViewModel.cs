using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductAttributeModel
{
    public class AddProductAttributeViewModel
    {
        public Guid ProductId { get; set; }
        [DisplayName("Kích cỡ")]
        public Guid ProductSizeId { get; set; }
        [DisplayName("Màu Sắc")]
        public Guid ProductColorId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [DisplayName("Giá")]
        public string Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá khuyến mãi")]
        [DisplayName("Giá khuyến mãi")]
        public string DiscountPrice { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [DisplayName("Số lượng")]
        public string CountStock { get; set; }
    }
}
