using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class OrderNewAdminViewModel
    {
        public string Code { get; set; }
        public List<ProductOrderedAdminViewModel> ProductOrderedAdminViewModels { get; set; }
        public string OrderStatus { get; set; }
        public string TotalPrice { get; set; }
    }
}
