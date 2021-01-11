using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CollectionModel
{
    public class AddCollectionViewModel
    {
        //[Required(ErrorMessage = "Vui lòng nhập tên bộ sưu tập")]
        [DisplayName("Tên bộ sưu tập")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public string URLImage { get; set; }
        [DisplayName("Ảnh danh mục")]
        public IFormFile ImageFile { get; set; }
    }
}
