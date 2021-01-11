using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlImage { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
