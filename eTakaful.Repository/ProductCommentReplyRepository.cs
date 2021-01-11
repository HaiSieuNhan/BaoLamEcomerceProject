using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Repository
{
    public class ProductCommentReplyRepository : BaseRepository<ProductCommentReply>, IProductCommentReplyRepository
    {
        public ProductCommentReplyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductCommentReplyViewModel>> GetMoreReplyComment(int SkipNum,Guid ProductCommentId)
        {
            var ListReply = await (from pcr in DbContext.ProductCommentReplys
                             join us in DbContext.Users on pcr.UserId equals us.Id
                             join up in DbContext.UserProfiles on us.Id equals up.UserId
                             where pcr.ProductCommentId == ProductCommentId && pcr.ProducCommentStatus == ProductCommentStatus.Qualified
                             select new ProductCommentReplyViewModel
                             {
                                 Id = pcr.Id,
                                 UserName = us.Username,
                                 AvatarUrl = up.AvatarUrl,
                                 Comment = pcr.Comment,
                                 CreatedDate = pcr.CreatedDate
                             }).Skip(SkipNum).ToListAsync();
            return ListReply;
        }
    }
}
