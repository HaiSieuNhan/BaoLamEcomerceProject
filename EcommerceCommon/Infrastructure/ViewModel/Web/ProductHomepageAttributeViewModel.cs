using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductHomepageAttributeViewModel
    {
        public Guid Id { get; set; }
        public string UrlImage { get; set; }
        public string Price { get; set; }
        public string PriceSale { get; set; }
        public decimal PercentSale { get; set; }
    }
}
