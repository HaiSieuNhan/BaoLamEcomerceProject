using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class ProductAttributeAdminViewModel :BaseViewModel
    {
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public int CountStock { get; set; }

    }
}
