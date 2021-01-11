using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductHomepage 
    {
        public IList<ProductHomepageAttributeViewModel> ProductHomepageAttributeViewModel { get; set; }
        public string Name { get; set; }
    }
}
