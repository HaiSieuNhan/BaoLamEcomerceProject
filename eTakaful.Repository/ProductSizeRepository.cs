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

namespace Ecommerce.Repository
{
    public class ProductSizeRepository : BaseRepository<ProductSize>, IProductSizeRepository
    {
        public ProductSizeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductSizeViewModel>> GetListProductSizeByProductColor(Guid ProductId, Guid? ProductColorId)
        {
            var productSizes = await (from ps in DbContext.ProductSizes
                                      join pa in DbContext.ProductAttributes on ps.Id equals pa.ProductSizeId
                                      where pa.ProductId == ProductId && pa.ProductColorId == ProductColorId
                                      orderby ps.Sort ascending
                                      select new ProductSizeViewModel
                                      {
                                          Id = ps.Id,
                                          Name = ps.Name,
                                          Sort = ps.Sort
                                      }).ToListAsync();
            return productSizes;
        }

        public async Task<List<ProductSizeViewModel>> GetListProductSizeByProductId(Guid ProductId)
        {
            var productSizes = await (from ps in DbContext.ProductSizes
                                       join pa in DbContext.ProductAttributes on ps.Id equals pa.ProductSizeId
                                       where pa.ProductId == ProductId
                                       group ps by ps.Id into pg
                                       orderby pg.FirstOrDefault().Sort ascending
                                       select new ProductSizeViewModel
                                       {
                                           Id = pg.FirstOrDefault().Id,
                                           Name = pg.FirstOrDefault().Name,
                                           Sort = pg.FirstOrDefault().Sort
                                       }).ToListAsync();
            return productSizes;
        }

        public async Task<List<ProductSizeViewModel>> GetListProductSizeViewModel()
        {
            var sizes = await (from ps in DbContext.ProductSizes
                               where ps.IsDeleted == false
                               orderby ps.Sort ascending
                               select new ProductSizeViewModel
                               {
                                   Id = ps.Id,
                                   Name = ps.Name,
                                   Sort = ps.Sort
                               }).ToListAsync();
            return sizes;
        }
    }

}
