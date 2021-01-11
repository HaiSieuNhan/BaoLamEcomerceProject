using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CategoryModel
{
    public class CategoryAdminViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Danh mục cha")]
        public Guid? ParentId { get; set; }
        [DisplayName("Ảnh danh mục")]
        public string URLImage { get; set; }
    }
}
