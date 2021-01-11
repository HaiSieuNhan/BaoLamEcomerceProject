using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.ViewModel.Admin;

namespace Ecommerce.Repository
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductImageAdminViewModel>> GetListProductImageAdminViewModel(Guid ProductId)
        {
            var listImage = await (from pi in DbContext.ProductImages
                                   join p in DbContext.Products on pi.ProductId equals p.Id
                                   join pc in DbContext.ProductColors on pi.ProductColorId equals pc.Id into pcl
                                   from pc in pcl.DefaultIfEmpty()
                                   where pi.ProductId == ProductId && p.IsDeleted == false && pi.IsDeleted == false
                                   select new ProductImageAdminViewModel
                                   {
                                       Id = pi.Id,
                                       ProductName = p.Name,
                                       ColorName = pc.Name,
                                       ImageLink = pi.ImageLink,
                                       IsMainImage = pi.IsMainImage,
                                       Sort = pi.Sort
                                   }).ToListAsync();
            return listImage;
        }

        public async Task<List<ProductImageFirstAttributeViewModel>> GetProductImageFirstAttribute(Guid ProductAttributeId)
        {
            var image = await (from pi in DbContext.ProductImages
                               join p in DbContext.Products on pi.ProductId equals p.Id
                               join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                               where pi.ProductColorId == pa.ProductColorId && pa.Id == ProductAttributeId 
                               select new ProductImageFirstAttributeViewModel
                               {
                                   Id = pi.Id,
                                   ProductColorId = pi.ProductColorId,
                                   ImageLink = pi.ImageLink,
                                   IsMainImage = pi.IsMainImage
                               }).ToListAsync();
            return image;
        }
    }
}

