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
using EcommerceCommon.Infrastructure.ViewModel.Web;
using EcommerceCommon.Infrastructure.Helper;

namespace Ecommerce.Repository
{
    public class OrderHistoryRepository : BaseRepository<OrderHistory>, IOrderHistoryRepository
    {
        public OrderHistoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CustomerOrderHistoryViewModel>> GetCustomerOrderHistoryViewModels(string Code)
        {
            var orderhistory = await (from oh in DbContext.OrderHistories
                                      join or in DbContext.Orders on oh.OrderId equals or.Id
                                      where or.Code == Code
                                      orderby oh.Sort descending
                                      
                                      select new CustomerOrderHistoryViewModel
                                      {
                                          Id = oh.Id,
                                          OrderStatus = oh.OrderStatus,
                                          DayOfWeek = TimeHelper.GetDayOfWeek(oh.Date),
                                          Date = oh.Date.ToString("dd-MM-yyyy"),
                                          Hour = oh.Date.ToString("HH:mm")
                                      }).ToListAsync();
            return orderhistory;
        }


        
    }
}
