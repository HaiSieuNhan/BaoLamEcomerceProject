using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.OrderModel;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IOrderService : IServices<Order>
    {
        Task<List<OrderAdminViewModel>> GetOrderAdminViewModels();
        Task<CheckOutViewModel> GetCheckOutViewModel(Guid UserId);
        Task<CheckOutViewModel> GetCheckOutViewModel(CheckOutInfoViewModel CheckOutInfoViewModel, Guid UserId);
        Task<bool> CreateOrder(CheckOutInfoViewModel CheckOutInfoViewModel, Guid UserId);
        Task<List<CustomerOrderViewModel>> GetCustomerOrderViewModels(Guid UserId);
        Task<CustomerOrderViewModel> GetCustomerOrderViewModelByCode(string Code);
        Task<OrderInfoAdminViewModel> GetOrderInfoAdminViewModelByCode(string Code);
        Task<EditOrderViewModel> GetEditOrderViewModel(string Code);
        Task<bool> EditOrderAsync(EditOrderViewModel editOrderViewModel);
        Task<List<RevenueAdminViewModel>> GetRevenueDayAdminViewModels();
        Task<List<RevenueAdminViewModel>> GetRevenueMonthAdminViewModels();
        Task<List<RevenueAdminViewModel>> GetRevenueYearAdminViewModels();
        Task<string> GetRevenueThisMonth();
        Task<string> GetRevenueThisYear();
        Task<string> GetRevenueThisDay();
        Task<List<OrderNewAdminViewModel>> GetOrderNewAdminViewModels();
        Task<List<OrderAdminViewModel>> GetOrderProcessAdminViewModels();
        Task<int> GetCountOrderProcess();


    }
}
