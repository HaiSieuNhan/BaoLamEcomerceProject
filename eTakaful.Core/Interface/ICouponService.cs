using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.CouponModel;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CouponAdminViewModel = EcommerceCommon.Infrastructure.ViewModel.Admin.CouponAdminViewModel;

namespace Ecommerce.Service.Interface
{

    public interface ICouponService : IServices<Coupon>
    {
        Task<List<CouponAdminViewModel>> GetCouponAdminViewModels();

        Task<AddCouponModel> GetAddCouponModel();

        Task<AddCouponModel> GetAddCouponViewModels(AddCouponViewModel AddCouponViewModel);

        Task<bool> AddCouponAsync(AddCouponViewModel AddCouponViewModel);

        Task<EditCouponModel> GetEditCouponModel(Guid Id);


        Task<EditCouponModel> GetEditCouponModel(EditCouponViewModel editCouponViewModel);

        Task<bool> EditCouponAsync(EditCouponViewModel editCouponViewModel);

        Task<bool> DeleteCouponAsync(Guid Id);









    }
}
