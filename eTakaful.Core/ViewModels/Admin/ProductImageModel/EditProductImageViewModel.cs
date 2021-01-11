using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductImageModel
{
    public class EditProductImageViewModel 
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [DisplayName("Màu Sắc")]
        public Guid ProductColorId { get; set; }
        [DisplayName("Ảnh")]
        public string ImageLink { get; set; }
        [DisplayName("Ảnh Chính")]
        public bool IsMainImage { get; set; }
    }
}
