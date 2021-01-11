using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductModel
{
    public class AddProductViewModel
    {
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Mô tả ngắn")]
        public string ShortDescription { get; set; }
        [DisplayName("Bộ sưu tập")]
        public Guid? CollectionId { get; set; }
        [DisplayName("Danh mục ")]
        public Guid CategoryId { get; set; }
        [DisplayName("Nhà sản xuất")]
        public Guid ManufacturerId { get; set; }
        [DisplayName("Nhà cung cấp")]
        public Guid SupplierId { get; set; }
    }
}
