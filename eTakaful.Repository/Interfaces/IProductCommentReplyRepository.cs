using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductCommentReplyRepository : IRepository<ProductCommentReply>
    {
        Task<List<ProductCommentReplyViewModel>> GetMoreReplyComment(int SkipNum,Guid ProductCommentId);
    }
}
