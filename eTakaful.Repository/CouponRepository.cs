using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.ViewModel.Admin;

namespace Ecommerce.Repository
{
    public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CouponAdminViewModel>> GetCouponAdminViewModels()
        {
            var coupon = await (from cou in DbContext.Coupons
                                join cate in DbContext.Categories on cou.CategoryId equals cate.Id 
                                into catel from cate in catel.DefaultIfEmpty()
                                join col in DbContext.Collections on cou.CollectionId equals col.Id
                                into coll from col in coll.DefaultIfEmpty()
                                where cou.IsDeleted == false
                                select new CouponAdminViewModel
                                {
                                    Id = cou.Id,
                                    Name = cou.Name,
                                    NumberApply = cou.NumberApply,
                                    Amount = cou.Amount.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                    StartTime = cou.StartTime == null ? "" : cou.StartTime.Value.ToString("dd/MM/yyyy"),
                                    EndTime = cou.EndTime == null ? "" : cou.EndTime.Value.ToString("dd/MM/yyyy"),
                                    CategoryName = cate.Name,
                                    CollectionName = col.Name
                                }).ToListAsync();
            return coupon;
        }

        

    }
}
