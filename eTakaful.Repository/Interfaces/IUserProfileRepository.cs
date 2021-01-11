using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        Task<IList<CustomerAdminViewModel>> GetCustomerAdminViewModels();
        Task<IList<StaffAdminViewModel>> GetStaffAdminViewModels();
        Task<CheckOutInfoViewModel> GetCheckOutInfoViewModel(Guid UserId);
        Task<IList<CustomerAdminViewModel>> GetNewCustomerAdminViewModels();

    }
}
