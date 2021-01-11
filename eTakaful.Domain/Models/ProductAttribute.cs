using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class ProductAttribute :BaseModel
    {
        [DisplayName("Kích cỡ")]
        public Guid? ProductSizeId { get; set; }
        [DisplayName("Màu Sắc")]
        public Guid? ProductColorId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [DisplayName("Số lượng")]
        public int CountStock { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [DisplayName("Giá")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá khuyến mãi")]
        [DisplayName("Giá khuyến mãi")]
        public decimal DiscountPrice { get; set; }
        #region Relationship
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
