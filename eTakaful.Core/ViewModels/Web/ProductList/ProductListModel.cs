using Ecommerce.Service.Dto;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.ProductList
{
    public class ProductListModel
    {
        public List<ProductHomepage> ProductHomepage { get; set; }
        public CategoryProductListViewModel CategoryProductListViewModel { get; set; }
        public List<CategoryProductListViewModel> ListCategoryProductListViewModel { get; set; }
        public List<ProductColorViewModel> ListProductColorViewModel { get; set; }
        public List<ProductSizeViewModel> ListProductSizeViewModel { get; set; }
    }
}
