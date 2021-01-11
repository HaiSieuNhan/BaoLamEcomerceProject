using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface ICouponRepository: IRepository<Coupon>
    {
        Task<List<CouponAdminViewModel>> GetCouponAdminViewModels();
    }
}
