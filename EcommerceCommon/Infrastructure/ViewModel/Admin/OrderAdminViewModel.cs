using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class OrderAdminViewModel :BaseViewModel
    {
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Descriptions { get; set; }
        public string OrderStatus { get; set; } 
        public string NotionalPrice { get; set; }
        public string TotalPrice { get; set; }
        public string Username { get; set; }
        public string CouponName { get; set; }

    }
}
