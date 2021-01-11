using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductAddCartErrorViewModel
    {
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int CountStock { get; set; }
    }
}
