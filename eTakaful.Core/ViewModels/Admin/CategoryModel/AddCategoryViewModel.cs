using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CategoryModel
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Danh mục cha")]
        public Guid? ParentId { get; set; }
        [DisplayName("Ảnh danh mục")]
        public string URLImage { get; set; }
        [DisplayName("Ảnh danh mục")]
        public IFormFile ImageFile { get; set; }
    }
}
