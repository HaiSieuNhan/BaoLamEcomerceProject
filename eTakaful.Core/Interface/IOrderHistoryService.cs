using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.OrderModel;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IOrderHistoryService : IServices<OrderHistory>
    {
        Task<List<CustomerOrderHistoryViewModel>> GetCustomerOrderHistoryViewModels(string Code);
        Task<CustomerOrderHistoryModel> GetCustomerOrderHistoryModel(string Code);
        Task<List<OrderHistoryDate>> GetOrderHistoryDates(List<CustomerOrderHistoryViewModel> customerOrderHistoryViewModels);
    }
}
