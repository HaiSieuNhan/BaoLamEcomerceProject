using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Interface
{
    public interface IProductCommentService :IServices<ProductComment>
    {
        Task<List<ProductCommentAdminViewModel>> GetProductCommentListViewModel();
        Task<List<ProductCommentViewModel>> GetProductCommentByProductId(Guid ProductId);
        Task<List<ProductCommentViewModel>> GetProductCommentPagination(Guid ProductId,int skip,int take);
        Task<List<ProductRatingViewModel>> GetProductRatingViewModel(Guid ProductId);
    }
}
