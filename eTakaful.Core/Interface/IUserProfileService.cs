using Ecommerce.Domain.Models;
using Ecommerce.Service.Dto;
using Ecommerce.Service.ViewModels.Web.Customer;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IUserProfileService : IServices<UserProfile>
    {
        Task<IList<CustomerAdminViewModel>> GetCustomerAdminViewModels();
        Task<IList<StaffAdminViewModel>> GetStaffAdminViewModels();

        Task<UserProfileDto> GetUserProfileDto(Guid Id);
        Task UpdateUserProfile(UserProfileDto userProfileDto,string wwwRootPath);
        // void Update (UserDto user, string password = null);
        Task<CustomerProfileViewModel> GetCustomerProfileViewModel(Guid UserId);
        Task<Validate> UpdateCustomerProfile(Guid UserId,CustomerProfileViewModel customerProfileViewModel);
        Task<IList<CustomerAdminViewModel>> GetNewCustomerAdminViewModels();
        Task<int> GetCountNewCustomer();


    }
}
