using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Interface
{
    public interface IProductCommentReplyService : IServices<ProductCommentReply>
    {
        Task<List<ProductCommentReplyViewModel>> GetMoreReplyComment(int SkipNum, Guid ProductCommentId);
    }
}
