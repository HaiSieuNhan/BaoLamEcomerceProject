using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CouponApplyCartViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountAmount { get; set; }

    }
}
