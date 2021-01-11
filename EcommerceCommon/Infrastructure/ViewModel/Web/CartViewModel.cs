using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string NotionalPrice { get; set; }
        public string TotalPrice { get; set; }
        public string DiscountPrice { get; set; }
        public List<CartDetailViewModel> CartDetailViewModels { get; set; }
        public List<CouponApplyCartViewModel> CouponApplyCartViewModels { get; set; }
    }
}
