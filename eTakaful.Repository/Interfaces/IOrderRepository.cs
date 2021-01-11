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
    public interface IOrderRepository : IRepository<Order>
    {
        /*Task<List<Product>> GetProductByCategoryIdAndOrderByView(Guid categoryId);
        Task<bool> GrowUpViewByProductId(Guid productId);
        */

        Task<List<OrderAdminViewModel>> GetOrderAdminViewModels();
        Task<List<CustomerOrderViewModel>> GetCustomerOrderViewModels(Guid UserId);
        Task<CustomerOrderViewModel> GetCustomerOrderViewModelByCode(string Code);
        Task<OrderInfoAdminViewModel> GetOrderInfoAdminViewModelByCode(string Code);
        Task<List<RevenueAdminViewModel>> GetRevenueDayAdminViewModels();
        Task<List<RevenueAdminViewModel>> GetRevenueMonthAdminViewModels();
        Task<List<RevenueAdminViewModel>> GetRevenueYearAdminViewModels();  
        Task<List<OrderNewAdminViewModel>> GetOrderNewAdminViewModels();
        Task<List<OrderAdminViewModel>> GetOrderProcessAdminViewModels();

    }
}
