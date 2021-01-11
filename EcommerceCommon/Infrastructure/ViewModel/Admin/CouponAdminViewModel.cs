using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class CouponAdminViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public int NumberApply { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; } 
        public string CollectionName { get; set; }
        public string CategoryName { get; set; }       
    }
}
