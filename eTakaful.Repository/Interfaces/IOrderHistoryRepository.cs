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
    public interface IOrderHistoryRepository : IRepository<OrderHistory>
    {
        /*Task<List<Product>> GetProductByCategoryIdAndOrderByView(Guid categoryId);
        Task<bool> GrowUpViewByProductId(Guid productId);
        */


        Task<List<CustomerOrderHistoryViewModel>> GetCustomerOrderHistoryViewModels(string Code);
    }
}
