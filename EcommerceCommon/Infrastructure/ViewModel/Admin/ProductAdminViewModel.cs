using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class ProductAdminViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Keyword { get; set; }
        public string Sku { get; set; }
        public int View { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ManufacturerName { get; set; }
        public string SupplierName { get; set; }
        public ProductStatus ProductStatus { get; set; }

    }
}
