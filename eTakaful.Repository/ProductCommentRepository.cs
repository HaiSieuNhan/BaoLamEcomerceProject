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
using EcommerceCommon.Infrastructure.ViewModel.Admin;

namespace Ecommerce.Repository
{
    public class ProductCommentRepository : BaseRepository<ProductComment>, IProductCommentRepository
    {
        public ProductCommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductCommentAdminViewModel>> GetProductCommentListViewModel()
        {
            var productcomment = await (from pm in DbContext.ProductComments
                                        join p in DbContext.Products on pm.ProductId equals p.Id
                                        join us in DbContext.Users on pm.UserId equals us.Id
                                        where p.IsDeleted == false && pm.IsDeleted == false
                                        select new ProductCommentAdminViewModel
                                        {
                                            Id = pm.Id,
                                            Comment = pm.Comment,
                                            Rating = pm.Rating,
                                            Username = us.Username,
                                            NameProduct = p.Name,
                                            Sort = pm.Sort,

                                        }).ToListAsync();
            return productcomment;
        }

        public async Task<List<ProductCommentViewModel>> GetProductCommentPagination(Guid ProductId, int skip, int take)
        {
            var productcomment = await(from pm in DbContext.ProductComments
                                       join us in DbContext.Users on pm.UserId equals us.Id
                                       join up in DbContext.UserProfiles on us.Id equals up.UserId
                                       where pm.IsDeleted == false && pm.ProductId == ProductId && pm.ProducCommentStatus == ProductCommentStatus.Qualified
                                       select new ProductCommentViewModel
                                       {
                                           Id = pm.Id,
                                           UserName = us.Username,
                                           AvatarUrl = up.AvatarUrl,
                                           Title = pm.Title,
                                           Comment = pm.Comment,
                                           Rating = pm.Rating,
                                           CreatedDate = pm.CreatedDate,
                                           ListImage = (from pci in DbContext.ProductCommentImages
                                                        where pci.ProductCommentId == pm.Id
                                                        select new ProductCommentImageViewModel
                                                        {
                                                            ImageUrl = pci.ImageLink
                                                        }).ToList(),
                                           ListReply = (from pcr in DbContext.ProductCommentReplys
                                                        join us in DbContext.Users on pcr.UserId equals us.Id
                                                        join up in DbContext.UserProfiles on us.Id equals up.UserId
                                                        where pcr.ProductCommentId == pm.Id && pcr.ProducCommentStatus == ProductCommentStatus.Qualified
                                                        select new ProductCommentReplyViewModel
                                                        {
                                                            Id = pcr.Id,
                                                            UserName = us.Username,
                                                            AvatarUrl = up.AvatarUrl,
                                                            Comment = pcr.Comment,
                                                            CreatedDate = pcr.CreatedDate
                                                        }).ToList()
                                       }).Skip(skip).Take(take).ToListAsync();
            return productcomment;
        }

        public async Task<List<ProductCommentViewModel>> GetProductCommentViewModel(Guid ProductId)
        {
            var productcomment = await (from pm in DbContext.ProductComments
                                        join us in DbContext.Users on pm.UserId equals us.Id
                                        join up in DbContext.UserProfiles on us.Id equals up.UserId
                                        where pm.IsDeleted == false && pm.ProductId == ProductId && pm.ProducCommentStatus == ProductCommentStatus.Qualified
                                        select new ProductCommentViewModel
                                        {
                                            Id=pm.Id,
                                            UserName = us.Username,
                                            AvatarUrl = up.AvatarUrl,
                                            Title = pm.Title,
                                            Comment = pm.Comment,
                                            Rating = pm.Rating,
                                            CreatedDate = pm.CreatedDate,
                                            ListImage = (from pci in DbContext.ProductCommentImages
                                                         where pci.ProductCommentId == pm.Id
                                                         select new ProductCommentImageViewModel
                                                         {
                                                             ImageUrl = pci.ImageLink,
                                                         }).ToList(),
                                            ListReply =(from pcr in DbContext.ProductCommentReplys
                                                        join us in DbContext.Users on pcr.UserId equals us.Id
                                                        join up in DbContext.UserProfiles on us.Id equals up.UserId
                                                        where pcr.ProductCommentId ==pm.Id && pcr.ProducCommentStatus == ProductCommentStatus.Qualified
                                                        select new ProductCommentReplyViewModel
                                                        {
                                                            Id = pcr.Id,
                                                            UserName =us.Username,
                                                            AvatarUrl = up.AvatarUrl,
                                                            Comment = pcr.Comment,
                                                            CreatedDate = pcr.CreatedDate
                                                        }).ToList()
                                        }).ToListAsync();
            return productcomment;
        }


    }
}
