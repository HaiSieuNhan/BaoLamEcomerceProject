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
    public class ProductColorRepository : BaseRepository<ProductColor>, IProductColorRepository
    {
        public ProductColorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductColorViewModel>> GetListProductColorByProductId(Guid ProductId)
        {
            var productColors = await (from pa in DbContext.ProductAttributes
                                       join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id
                                       where pa.ProductId == ProductId
                                       orderby pc.Sort ascending
                                       group pc by pc.Id into pg
                                       select new ProductColorViewModel
                                       {
                                           Id = pg.FirstOrDefault().Id,
                                           Name = pg.FirstOrDefault().Name,
                                           Sort = pg.FirstOrDefault().Sort
                                       }).ToListAsync();
            return productColors;
        }

        public async Task<List<ProductColorViewModel>> GetListProductColorViewModel()
        {
            var colors = await (from pc in DbContext.ProductColors
                                orderby pc.Sort ascending
                                where pc.IsDeleted == false
                                select new ProductColorViewModel
                                {
                                    Id = pc.Id,
                                    Name = pc.Name,
                                    Sort = pc.Sort
                                }).ToListAsync();
            return colors;
        }
    }

}
