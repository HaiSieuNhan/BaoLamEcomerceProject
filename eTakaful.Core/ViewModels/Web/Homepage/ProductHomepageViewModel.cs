using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.Homepage
{
    public class ProductHomepageViewModel : CollectionHomepageViewModel
    {
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
    }
}
