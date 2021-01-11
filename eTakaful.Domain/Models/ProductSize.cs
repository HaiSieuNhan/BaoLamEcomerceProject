using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class ProductSize:BaseModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên kích cỡ")]
        [DisplayName("Tên kích cỡ")]
        public string Name { get; set; }

    }
}
