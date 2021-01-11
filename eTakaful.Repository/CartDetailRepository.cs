using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using EcommerceCommon.Infrastructure.Enums;

namespace Ecommerce.Repository
{
    public class CartDetailRepository : BaseRepository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

       

        public async Task<List<CartDetailViewModel>> GetListCartDetailByUserId(Guid UserId)
        {
            var cartDetail = await (from c in DbContext.Carts
                                    join cd in DbContext.CartDetails on c.Id equals cd.CartId
                                    join pa in DbContext.ProductAttributes on cd.ProductAttributeId equals pa.Id
                                    join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                    from pc in pcl.DefaultIfEmpty()
                                    join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                    from ps in psl.DefaultIfEmpty()
                                    join p in DbContext.Products on pa.ProductId equals p.Id
                                    join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                    where c.IsDeleted == false && c.CartStatus==CartStatus.PreOrder && c.UserId == UserId 
                                    && pi.IsMainImage == true && pi.ProductColorId == pc.Id 
                                    select new CartDetailViewModel
                                    {
                                        ProductName = p.Name,
                                        ProductImage = pi.ImageLink,
                                        ProductSize = ps.Name,
                                        ProductColor = pc.Name,
                                        Price = cd.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                        DiscountPrice = cd.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                        Quantity = cd.Quantity,
                                        CountStock = pa.CountStock,
                                    }).ToListAsync();
            return cartDetail;
        }
    }
}
