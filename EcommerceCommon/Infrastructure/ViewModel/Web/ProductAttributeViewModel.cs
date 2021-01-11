using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductAttributeViewModel
    {
        public Guid Id { get; set; }
        public Guid? ProductSizeId { get; set; }
        public Guid? ProductColorId { get; set; }
        public string Size { get; set; }
        public decimal Price{ get; set; }
        public decimal DiscountPrice{ get; set; }
    }
}
