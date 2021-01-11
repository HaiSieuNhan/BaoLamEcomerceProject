using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class ProductColor :BaseModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên màu sắc")]
        [DisplayName("Tên màu sắc")]
        public string Name { get; set; }
    }
}
