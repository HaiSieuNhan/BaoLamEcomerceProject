using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.OrderModel;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class OrderHistoryService : EcommerceServices<OrderHistory>, IOrderHistoryService
    {
        private readonly IOrderHistoryRepository _orderhistoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;


        public OrderHistoryService(IOrderHistoryRepository orderhistoryRepository, IMapper mapper, IOrderRepository orderRepository) : base(orderhistoryRepository)
        {
            _orderhistoryRepository = orderhistoryRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<CustomerOrderHistoryModel> GetCustomerOrderHistoryModel(string Code)
        {
            var orderhistory = await GetCustomerOrderHistoryViewModels(Code);
            var dates = await GetOrderHistoryDates(orderhistory);
            var model = new CustomerOrderHistoryModel
            {
                CustomerOrderHistoryViewModels = orderhistory,
                OrderHistoryDates = dates
            };
            return model;
        }

        public async Task<List<CustomerOrderHistoryViewModel>> GetCustomerOrderHistoryViewModels(string Code)
        {
            var orderhistory = await _orderhistoryRepository.GetCustomerOrderHistoryViewModels(Code);
            return orderhistory;
        }


        public async Task<List<OrderHistoryDate>> GetOrderHistoryDates(List<CustomerOrderHistoryViewModel> customerOrderHistoryViewModels)
        {
            if(customerOrderHistoryViewModels == null)
            {
                return null;
            }
            var orderHistoryDate = new List<OrderHistoryDate>();
            var date = "";
            foreach(var item in customerOrderHistoryViewModels)
            {
                 if(item.Date != date)
                {
                    orderHistoryDate.Add(new OrderHistoryDate
                    {
                        Date = item.Date,
                        DayOfWeek = item.DayOfWeek
                    });
                    date = item.Date;
                }
            }
            return orderHistoryDate;
        }

    }
}