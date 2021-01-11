using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductModel
{
    public class EditProductViewModel :AddProductViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Trạng thái")]
        public ProductStatus ProductStatus { get; set; }
        
    }
}
