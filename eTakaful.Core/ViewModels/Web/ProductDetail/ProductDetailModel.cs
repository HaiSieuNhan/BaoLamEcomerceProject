using Ecommerce.Domain.Models;
using Ecommerce.Service.Dto;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.ProductDetail
{
    public class ProductDetailModel
    {
        public ProductFirstAttributeViewModel ProductFirstAttributeViewModel { get; set; }
        public IList<ProductImageFirstAttributeViewModel> ProductImageFirstAttributeViewModel { get; set; }
        public ProductComment ProductComment { get; set; }
        public ProductCommentReply ProductCommentReply { get; set; }
        public IList<ProductCommentViewModel> ProductCommentViewModel { get; set; }
        public IList<ProductCommentImageViewModel> ProductCommentImageViewModel { get; set; }
        public IList<ProductRatingViewModel> ProductRatingViewModel { get; set; }
        public IList<ProductColorViewModel> ProductColorViewModel { get; set; }
        public IList<ProductSizeViewModel> ProductSizeViewModel { get; set; }
        public IList<ProductSizeViewModel> ProductSizeFirstAttributeViewModel { get; set; }
    }
}
