using Ecommerce.Service.Dto;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;



namespace Ecommerce.Service.ViewModels.Web.Cart
{
    public class ProductCartViewModel
    {
        public IList<CartDetailViewModel> CartDetailListViewModel { get; set; }
        public CartViewModel CartViewModel { get; set; }
    }
}
