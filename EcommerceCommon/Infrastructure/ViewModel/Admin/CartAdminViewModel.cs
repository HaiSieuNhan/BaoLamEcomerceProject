
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class CartAdminViewModel: BaseViewModel
    {
        public string CreatedDate { get; set; }
        public string CartName { get; set; }
        public string CustomerName { get; set; }
        public string NotionalPrice { get; set; }       
        public string TotalPrice { get; set; }
        public string DiscountPrice { get; set; }
        public List<CartDetailViewModel> CartDetailViewModels { get; set; }
        public List<CouponApplyCartViewModel> CouponApplyCartViewModels { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}
