using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class ProductImageAdminViewModel :BaseViewModel
    {
        public string ImageLink { get; set; }
        public bool IsMainImage { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
    }
}
