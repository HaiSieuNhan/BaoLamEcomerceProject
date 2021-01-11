using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CouponModel
{
    public class AddCouponViewModel
    {

        [MaxLength(256)]
        [Required(ErrorMessage = "Vui lòng nhập mã giảm giá")]
        [DisplayName("Tên mã giảm giá")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [DisplayName("Giá")]
        public string Amount { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [DisplayName("Số lượng")]
        public string NumberApply { get; set; }
        [DisplayName("Ngày bắt đầu")]

        public string StartTime { get; set; }
        [DisplayName("Ngày kết thúc")]
        public string EndTime { get; set; }
        [DisplayName("Danh mục áp dụng")]
        public Guid? CategoryId { get; set; }
        [DisplayName("Bộ sưu tập áp dụng")]

        public Guid? CollectionId { get; set; }
    }
}
