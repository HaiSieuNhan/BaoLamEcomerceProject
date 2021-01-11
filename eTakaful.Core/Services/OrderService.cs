using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.OrderModel;
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class OrderService : EcommerceServices<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;


        public OrderService(IOrderRepository orderRepository, IMapper mapper, ICartRepository cartRepository, IUserProfileRepository userProfileRepository, IOrderDetailRepository orderDetailRepository, IOrderHistoryRepository orderHistoryRepository, IProductAttributeRepository productAttributeRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _userProfileRepository = userProfileRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderHistoryRepository = orderHistoryRepository;
            _productAttributeRepository = productAttributeRepository;
        }

        public async Task<bool> CreateOrder(CheckOutInfoViewModel CheckOutInfoViewModel, Guid UserId)
        {
            var cart = await _cartRepository.GetCartViewModelByUserId(UserId);
            if (cart != null && cart.CartDetailViewModels.Count != 0 && UserId != Guid.Empty)
            {
                foreach (var item in cart.CartDetailViewModels)
                {
                    var product = await _productAttributeRepository.FindAsync(x => x.Id == item.ProductAttributeId);
                    if (product == null)
                    {
                        return false;
                    }
                    if (product.CountStock < item.Quantity)
                    {
                        return false;
                    }
                    product.CountStock -= item.Quantity;
                    await _productAttributeRepository.UpdateAsync(product);
                }
                var order = new Order
                {
                    CustomerName = CheckOutInfoViewModel.Name,
                    Phone = CheckOutInfoViewModel.Phone,
                    Address = CheckOutInfoViewModel.Address,
                    PaymentMethod = CheckOutInfoViewModel.PaymentMethod,
                    TotalPrice = decimal.Parse(cart.TotalPrice.Replace(".", "")),
                    NotionalPrice = decimal.Parse(cart.NotionalPrice.Replace(".", "")),
                    UserId = UserId,
                    OrderStatus = OrderStatus.OrderSuccess
                };
                await _orderRepository.AddAsync(order);

                foreach (var item in cart.CartDetailViewModels)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductAttributeId = item.ProductAttributeId,
                        Price = decimal.Parse(item.DiscountPrice.Replace(".", "")),
                        Quantity = item.Quantity,
                        OrderId = order.Id,

                    };
                    await _orderDetailRepository.AddAsync(orderDetail);


                }

                var orderHistory = new OrderHistory
                {
                    OrderStatus = OrderStatus.OrderSuccess,
                    OrderId = order.Id
                };
                await _orderHistoryRepository.AddAsync(orderHistory);

                var cartOrder = await _cartRepository.GetByIdAsync(cart.Id);
                cartOrder.CartStatus = CartStatus.Ordered;
                await _cartRepository.UpdateAsync(cartOrder);
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<CheckOutViewModel> GetCheckOutViewModel(Guid UserId)
        {
            var cart = await _cartRepository.GetCartViewModelByUserId(UserId);
            var userInfo = await _userProfileRepository.GetCheckOutInfoViewModel(UserId);
            var model = new CheckOutViewModel
            {
                CartViewModel = cart,
                CheckOutInfoViewModel = userInfo
            };
            return model;
        }

        public async Task<CheckOutViewModel> GetCheckOutViewModel(CheckOutInfoViewModel CheckOutInfoViewModel, Guid UserId)
        {
            var cart = await _cartRepository.GetCartViewModelByUserId(UserId);
            var model = new CheckOutViewModel
            {
                CartViewModel = cart,
                CheckOutInfoViewModel = CheckOutInfoViewModel
            };
            return model;
        }

        public async Task<CustomerOrderViewModel> GetCustomerOrderViewModelByCode(string Code)
        {
            var order = await _orderRepository.GetCustomerOrderViewModelByCode(Code);
            return order;
        }
        public async Task<EditOrderViewModel> GetEditOrderViewModel(string Code)
        {
            var order = await _orderRepository.GetFirstOrDefaultAsync(x => x.Code == Code);
            return _mapper.Map<EditOrderViewModel>(order);
        }

        public async Task<List<CustomerOrderViewModel>> GetCustomerOrderViewModels(Guid UserId)
        {
            var orders = await _orderRepository.GetCustomerOrderViewModels(UserId);
            return orders;
        }

        public async Task<List<OrderAdminViewModel>> GetOrderAdminViewModels()
        {
            var orderlist = await _orderRepository.GetOrderAdminViewModels();
            return orderlist;
        }

        public async Task<OrderInfoAdminViewModel> GetOrderInfoAdminViewModelByCode(string Code)
        {
            var orderList = await _orderRepository.GetOrderInfoAdminViewModelByCode(Code);
            return orderList;
        }

        public async Task<bool> EditOrderAsync(EditOrderViewModel editOrderViewModel)
        {
            var order = await _orderRepository.GetFirstOrDefaultAsync(x => x.Code == editOrderViewModel.Code);
            if (order == null)
            {
                return false;
            }
            else
            {
                var status = new OrderHistory
                {
                    OrderStatus = editOrderViewModel.OrderStatus,
                    OrderId = order.Id
                };
                await _orderHistoryRepository.AddAsync(status);
                order.OrderStatus = editOrderViewModel.OrderStatus;
                await _orderRepository.UpdateAsync(order);
                return true;
            }
        }


        public async Task<List<RevenueAdminViewModel>> GetRevenueDayAdminViewModels()
        {
            var revenue = await _orderRepository.GetRevenueDayAdminViewModels();
            return revenue;
        }
        public async Task<List<RevenueAdminViewModel>> GetRevenueMonthAdminViewModels()
        {
            var revenue = await _orderRepository.GetRevenueMonthAdminViewModels();
            return revenue;
        }
        public async Task<List<RevenueAdminViewModel>> GetRevenueYearAdminViewModels()
        {
            var revenue = await _orderRepository.GetRevenueYearAdminViewModels();
            return revenue;
        }
        public async Task<string> GetRevenueThisDay()
        {
            var revenue = await _orderRepository.GetRevenueDayAdminViewModels();
            var total = revenue.Select(x => x.TotalRevenue).Sum().ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is"));
            return total;
        }
        public async Task<string> GetRevenueThisMonth()
        {
            var revenue = await _orderRepository.GetRevenueMonthAdminViewModels();
            var total = revenue.Select(x => x.TotalRevenue).Sum().ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is"));
            return total;
        }

        public async Task<string> GetRevenueThisYear()
        {
            var revenue = await _orderRepository.GetRevenueYearAdminViewModels();
            var total = revenue.Select(x => x.TotalRevenue).Sum().ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is"));
            return total;
        }

        public async Task<List<OrderNewAdminViewModel>> GetOrderNewAdminViewModels()
        {
            var orders = await _orderRepository.GetOrderNewAdminViewModels();
            return orders;
        }

        public async Task<List<OrderAdminViewModel>> GetOrderProcessAdminViewModels()
        {
            var orderprocess = await _orderRepository.GetOrderProcessAdminViewModels();
            return orderprocess;
        }

        public async Task<int> GetCountOrderProcess()
        {
            var orderprocess = await _orderRepository.GetOrderProcessAdminViewModels();
            var total = orderprocess.Count();
            return total;
        }

        
    }
}
