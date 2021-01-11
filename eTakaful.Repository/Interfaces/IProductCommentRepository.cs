using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductCommentRepository: IRepository<ProductComment>
    {
        Task<List<ProductCommentAdminViewModel>> GetProductCommentListViewModel();
        Task<List<ProductCommentViewModel>> GetProductCommentViewModel(Guid ProductId);
        Task<List<ProductCommentViewModel>> GetProductCommentPagination(Guid ProductId,int skip,int take);
    }
}
