using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using EcommerceCommon.Infrastructure.ViewModel.Admin;

namespace Ecommerce.Repository
{
    public class ProductAttributeRepository : BaseRepository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductAttributeViewModel> GetProductAttributeByProductColor(Guid ProductId, Guid ProductColorId)
        {
            var products = await (from pa in DbContext.ProductAttributes
                                  join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id 
                                  into psl from ps in psl.DefaultIfEmpty()
                                  where pa.ProductId == ProductId && pa.ProductColorId == ProductColorId
                                  orderby ps.Sort ascending
                                  select new ProductAttributeViewModel
                                  {
                                      Id = pa.Id,
                                      ProductSizeId = ps.Id,
                                      ProductColorId = pa.ProductColorId,
                                      Size=ps.Name,
                                      DiscountPrice = pa.DiscountPrice,
                                      Price = pa.Price
                                  }).FirstOrDefaultAsync();
            return products;
        }
        public async Task<ProductHomepageAttributeViewModel> GetProductHomepageByProductAttribute(Guid ProductAttributeId)
        {
            var products = await (from pa in DbContext.ProductAttributes
                                  join p in DbContext.Products on pa.ProductId equals p.Id
                                  join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                  where pa.Id == ProductAttributeId && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                  select new ProductHomepageAttributeViewModel
                                  {
                                      Id = pa.Id,
                                      UrlImage = pi.ImageLink,
                                      PriceSale = pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                      Price = pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                      PercentSale = Math.Round((1 - pa.DiscountPrice / pa.Price) * 100, 0)
                                  }).FirstOrDefaultAsync();
            return products;
        }
        public async Task<ProductAttributeViewModel> GetProductAttributeByProductSize(Guid ProductId,Guid? ProductColorId, Guid ProductSizeId)
        {
            var products = await (from pa in DbContext.ProductAttributes
                                  join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id
                                  where pa.ProductId == ProductId && pa.ProductSizeId == ProductSizeId && pa.ProductColorId == ProductColorId
                                  select new ProductAttributeViewModel
                                  {
                                      Id = pa.Id,
                                      ProductSizeId = pa.ProductSizeId,
                                      ProductColorId = pa.ProductColorId,
                                      Size = ps.Name,
                                      DiscountPrice = pa.DiscountPrice,
                                      Price = pa.Price
                                  }).FirstOrDefaultAsync();
            return products;
        }

        public async Task<ProductAddCartErrorViewModel> GetProductAddCartErrorViewModel(Guid ProductId, Guid? ProductSizeId, Guid? ProductColorId)
        {
            var product = await (from p in DbContext.Products
                                 join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                 join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                 from ps in psl.DefaultIfEmpty()
                                 join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                 from pc in pcl.DefaultIfEmpty()
                                 where p.Id == ProductId && p.IsDeleted == false && pa.ProductColorId == ProductColorId && pa.ProductSizeId == ProductSizeId
                                 select new ProductAddCartErrorViewModel
                                 {
                                     ProductName = p.Name,
                                     Color = pc.Name,
                                     Size = ps.Name,
                                     CountStock = pa.CountStock
                                 }).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<ProductAttributeAdminViewModel>> GetListProductAttributeAdminViewModel(Guid ProductId)
        {
            var attribute = await (from pa in DbContext.ProductAttributes
                                   join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                   from ps in psl.DefaultIfEmpty()
                                   join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                   from pc in pcl.DefaultIfEmpty()
                                   join p in DbContext.Products on pa.ProductId equals p.Id
                                   where p.Id == ProductId && pa.IsDeleted == false && p.IsDeleted == false
                                   select new ProductAttributeAdminViewModel
                                   {
                                       Id = pa.Id,
                                       ProductName = p.Name,
                                       ColorName = pc.Name,
                                       SizeName = ps.Name,
                                       Price = pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                       DiscountPrice = pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                       CountStock = pa.CountStock
                                   }).ToListAsync();
            return attribute;
        }
    }

}
