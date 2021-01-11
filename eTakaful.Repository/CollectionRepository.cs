using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<CollectionViewModel>> GetListCollectionViewModel()
        {
            var collection = await (from c in DbContext.Collections
                                    where c.IsDeleted == false
                                    orderby c.Sort ascending
                                    select new CollectionViewModel
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        ListProductHomepageViewModel = (from p in DbContext.Products
                                                                        where p.CollectionId == c.Id && p.IsDeleted== false
                                                                        select new ProductHomepage
                                                                        {
                                                                            Name = p.Name,
                                                                            ProductHomepageAttributeViewModel = (from pa in DbContext.ProductAttributes
                                                                                                                 join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                                                                 from pc in pcl.DefaultIfEmpty()
                                                                                                                 join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                                                                 from ps in psl.DefaultIfEmpty()
                                                                                                                 join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                                                                                                 orderby pc.Sort ascending
                                                                                                                 where pa.ProductId == p.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                                                                                                 group new { pa, pc, ps, pi } by new { pa.ProductId, pa.ProductColorId } into pg
                                                                                                                 select new ProductHomepageAttributeViewModel
                                                                                                                 {
                                                                                                                     Id = pg.FirstOrDefault().pa.Id,
                                                                                                                     UrlImage = pg.FirstOrDefault().pi.ImageLink,
                                                                                                                     PriceSale = pg.FirstOrDefault().pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                                                                     Price = pg.FirstOrDefault().pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                                                                     PercentSale = Math.Round((1 - pg.FirstOrDefault().pa.DiscountPrice / pg.FirstOrDefault().pa.Price) * 100, 0)
                                                                                                                 }).ToList()
                                                                        }).ToList()
                                    }).ToListAsync();
            return collection;
        }
    }
}
