using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductImageModel
{
    public class AddProductImageViewModel
    {
        public Guid ProductId { get; set; }
        [DisplayName("Màu Sắc")]
        public Guid ProductColorId { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [DisplayName("Ảnh Chính")]
        public string isMainImage { get; set; }
    }
}
