using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductFirstAttributeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Sku { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }     
        public Guid? ProductSizeId { get; set; }
        public Guid? ProductColorId { get; set; }
        public Guid ProductId { get; set; }
    }
}
