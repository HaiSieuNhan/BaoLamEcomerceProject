using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ManufacturerModel
{
    public class AddManufacturerViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên nhà sản xuất")]
        [DisplayName("Tên nhà sản xuất")]
        public string Name { get; set; }
        [StringLength(100)]
        [DisplayName("Mã nhà sản xuất")]
        public string CodeName { get; set; }
        [MaxLength(256)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [MaxLength(256)]
        [DisplayName("Website")]
        public string Website { get; set; }
        [DisplayName("Ảnh danh mục")]
        [MaxLength(256)]
        public string Logo { get; set; } 
        [NotMapped]
        [DisplayName("Ảnh Logo")]
        public IFormFile ImageFile { get; set; }
    }
}
