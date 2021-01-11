using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class ProductDetailAdminViewModel
    {
        public string Name { get; set; }
        public string PublicationDate { get; set; }
        public string Keyword { get; set; }
        public string Sku { get; set; }
        public int View { get; set; }
        public string CategoryName { get; set; }
        public string CollectionName { get; set; }
        public string ManufacturerName { get; set; }
        public string SupplierName { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
