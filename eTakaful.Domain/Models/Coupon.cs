using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class Coupon :BaseModel
    {
        [MaxLength(256)]
        [Required(ErrorMessage = "Vui lòng nhập mã giảm giá")]
        [DisplayName("Tên mã giảm giá")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [DisplayName("Giá")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [DisplayName("Số lượng")]
        public int NumberApply { get; set; }
        [DisplayName("Ngày bắt đầu")]

        public DateTime? StartTime { get; set; }
        [DisplayName("Ngày kết thúc")]

        public DateTime? EndTime { get; set; }
        [DisplayName("Danh mục áp dụng")]

        public Guid? CategoryId { get; set; }
        [DisplayName("Bộ sưu tập áp dụng")]

        public Guid? CollectionId { get; set; }
        #region Relationship
        public virtual ICollection<CouponApply> CouponApplies { get; set; }
        #endregion
    }
}
