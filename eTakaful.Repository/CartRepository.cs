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
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddCart(Guid ProductSizeId, int Quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> GetCartViewModelByUserId(Guid? UserId)
        {
            var cart = await (from c in DbContext.Carts
                              where c.IsDeleted == false && c.UserId == UserId && c.CartStatus == CartStatus.PreOrder
                              select new CartViewModel
                              {
                                  Id = c.Id,
                                  NotionalPrice = c.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                  TotalPrice = c.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                  DiscountPrice = (c.TotalPrice - c.NotionalPrice).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                  CartDetailViewModels = (from cd in DbContext.CartDetails
                                                          where cd.CartId == c.Id
                                                          orderby cd.Sort ascending
                                                          select new CartDetailViewModel
                                                          {
                                                              Id = cd.Id,
                                                              ProductAttributeId = cd.ProductAttributeId,
                                                              ProductName = (from p in DbContext.Products
                                                                             join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                                                             where pa.Id == cd.ProductAttributeId
                                                                             select p.Name).FirstOrDefault(),
                                                              ProductImage = (from pi in DbContext.ProductImages
                                                                              join p in DbContext.Products on pi.ProductId equals p.Id
                                                                              join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                                                              join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                              from pc in pcl.DefaultIfEmpty()
                                                                              where pi.IsMainImage == true && pi.ProductColorId == pc.Id && pa.Id== cd.ProductAttributeId
                                                                              select pi.ImageLink).FirstOrDefault(),
                                                              ProductColor = (from pa in DbContext.ProductAttributes 
                                                                              join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                              from pc in pcl.DefaultIfEmpty()
                                                                              where  pa.Id == cd.ProductAttributeId
                                                                              select pc.Name).FirstOrDefault(),
                                                              ProductSize = (from pa in DbContext.ProductAttributes 
                                                                              join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                              from ps in psl.DefaultIfEmpty()
                                                                              where  pa.Id == cd.ProductAttributeId
                                                                              select ps.Name).FirstOrDefault(),
                                                              Price = cd.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                              DiscountPrice = cd.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                              Quantity = cd.Quantity,
                                                          }).ToList(),
                                  CouponApplyCartViewModels =  (from cou in DbContext.Coupons
                                                               join ca in DbContext.CouponApplies on cou.Id equals ca.CouponId
                                                               where ca.CartId == c.Id
                                                               select new CouponApplyCartViewModel
                                                               {
                                                                   Id = cou.Id,
                                                                   Name = cou.Name,
                                                                   DiscountAmount = cou.Amount
                                                               }).ToList()
                              }).FirstOrDefaultAsync();
            return cart;
        }
        public async Task<List<CartAdminViewModel>> GetCartAdminViewModels()
        {
            var cart = await (from c in DbContext.Carts
                              join u in DbContext.Users on c.UserId equals u.Id
                              join up in DbContext.UserProfiles on u.Id equals up.UserId
                              where c.IsDeleted == false
                              select new CartAdminViewModel
                              {
                                  Id = c.Id,
                                  CartName = c.CartName,
                                  NotionalPrice = c.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                  TotalPrice = c.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                  CustomerName = up.Name,
                                  CartStatus = c.CartStatus
                              }).ToListAsync();
            return cart;
        }

            public async Task<CartAdminViewModel> GetCartAdminViewModelById(Guid Id)
        {
            var cart = await(from c in DbContext.Carts
                               join u in DbContext.Users on c.UserId equals u.Id
                               join up in DbContext.UserProfiles on u.Id equals up.UserId
                               where c.IsDeleted == false && c.Id == Id
                               select new CartAdminViewModel
                               {
                                   CartName = c.CartName,
                                   CreatedDate = c.CreatedDate == null ? "" : c.CreatedDate.Value.ToString("dd-MM-yyyy"),
                                   NotionalPrice = c.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   TotalPrice = c.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   DiscountPrice = (c.TotalPrice - c.NotionalPrice).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   CustomerName = up.Name,
                                   CartStatus = c.CartStatus,
                                   CartDetailViewModels = (from cd in DbContext.CartDetails
                                                           where cd.CartId == c.Id
                                                           orderby cd.Sort ascending
                                                           select new CartDetailViewModel
                                                           {
                                                               Id = cd.Id,
                                                               ProductAttributeId = cd.ProductAttributeId,
                                                               ProductName = (from p in DbContext.Products
                                                                              join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                                                              where pa.Id == cd.ProductAttributeId
                                                                              select p.Name).FirstOrDefault(),
                                                               ProductImage = (from pi in DbContext.ProductImages
                                                                               join p in DbContext.Products on pi.ProductId equals p.Id
                                                                               join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                                                               join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                               from pc in pcl.DefaultIfEmpty()
                                                                               where pi.IsMainImage == true && pi.ProductColorId == pc.Id && pa.Id == cd.ProductAttributeId
                                                                               select pi.ImageLink).FirstOrDefault(),
                                                               ProductColor = (from pa in DbContext.ProductAttributes
                                                                               join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                               from pc in pcl.DefaultIfEmpty()
                                                                               where pa.Id == cd.ProductAttributeId
                                                                               select pc.Name).FirstOrDefault(),
                                                               ProductSize = (from pa in DbContext.ProductAttributes
                                                                              join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                              from ps in psl.DefaultIfEmpty()
                                                                              where pa.Id == cd.ProductAttributeId
                                                                              select ps.Name).FirstOrDefault(),
                                                               Price = cd.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                               DiscountPrice = cd.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                               TotalPrice = (cd.DiscountPrice * cd.Quantity).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                               Quantity = cd.Quantity,
                                                           }).ToList(),
                                   CouponApplyCartViewModels = (from cou in DbContext.Coupons
                                                                join ca in DbContext.CouponApplies on cou.Id equals ca.CouponId
                                                                where ca.CartId == c.Id
                                                                select new CouponApplyCartViewModel
                                                                {
                                                                    Id = cou.Id,
                                                                    Name = cou.Name,
                                                                    DiscountAmount = cou.Amount
                                                                }).ToList()
                               }).FirstOrDefaultAsync();
            return cart;
        }
    }
}
