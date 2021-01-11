using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CollectionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<ProductHomepage> ListProductHomepageViewModel { get; set; }
    }
}
