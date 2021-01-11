using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.Helper;

namespace Ecommerce.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CustomerOrderViewModel> GetCustomerOrderViewModelByCode(string Code)
        {
            var order = await (from or in DbContext.Orders
                               where or.Code == Code
                               select new CustomerOrderViewModel
                               {
                                   Id = or.Id,
                                   Code = or.Code,
                                   CreatedDate = or.CreatedDate == null ? "" : or.CreatedDate.Value.ToString("HH:mm dd/MM/yyyy"),
                                   CustomerName = or.CustomerName,
                                   Address = or.Address,
                                   Phone = or.Phone,
                                   PaymentMethod =or.PaymentMethod,
                                   OrderStatus = or.OrderStatus,
                                   TotalAmount = or.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   NotionalAmount = or.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   DiscountAmount = (or.TotalPrice - or.NotionalPrice).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   CustomerOrderDetailViewModels = (from od in DbContext.OrderDetails
                                                                    join pa in DbContext.ProductAttributes on od.ProductAttributeId equals pa.Id
                                                                    join p in DbContext.Products on pa.ProductId equals p.Id
                                                                    join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                    from pc in pcl.DefaultIfEmpty()
                                                                    join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                    from ps in psl.DefaultIfEmpty()
                                                                    join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                                                    where od.OrderId == or.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                                                    select new CustomerOrderDetailViewModel
                                                                    {
                                                                        ProductAttributeId = pa.Id,
                                                                        ProductImage = pi.ImageLink,
                                                                        ProductName = p.Name,
                                                                        ProductColor = pc.Name,
                                                                        ProductSize = ps.Name,
                                                                        Price = od.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                        Quantity = od.Quantity,
                                                                        TotalPrice = (od.Price * od.Quantity).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                    }).ToList()
                               }).FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<CustomerOrderViewModel>> GetCustomerOrderViewModels(Guid UserId)
        {
            var orders = await (from or in DbContext.Orders
                                where or.UserId == UserId && or.IsDeleted == false
                                orderby or.Sort descending
                                select new CustomerOrderViewModel
                                {
                                    Id = or.Id,
                                    Code = or.Code,
                                    CreatedDate = or.CreatedDate == null ? "" : or.CreatedDate.Value.ToString("dd/MM/yyyy"),
                                    CustomerName = or.CustomerName,
                                    OrderStatus = or.OrderStatus,
                                    TotalAmount = or.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                    CustomerOrderDetailViewModels = (from od in DbContext.OrderDetails
                                                                     join pa in DbContext.ProductAttributes on od.ProductAttributeId equals pa.Id
                                                                     join p in DbContext.Products on pa.ProductId equals p.Id
                                                                     join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                     from pc in pcl.DefaultIfEmpty()
                                                                     join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                     from ps in psl.DefaultIfEmpty()
                                                                     where od.OrderId == or.Id 
                                                                     select new CustomerOrderDetailViewModel
                                                                     {
                                                                         ProductName = p.Name,
                                                                         ProductColor = pc.Name,
                                                                         ProductSize = ps.Name
                                                                     }).ToList()
                                }).ToListAsync();
            return orders;
        }

        public async Task<List<OrderAdminViewModel>> GetOrderAdminViewModels()
        {
            var orderlist = await (from or in DbContext.Orders
                                      join us in DbContext.Users
                                      on or.UserId equals us.Id
                                      where or.IsDeleted == false
                                      select new OrderAdminViewModel
                                      {
                                          Id = or.Id,
                                          Code = or.Code,
                                          CustomerName = or.CustomerName,
                                          Address = or.Address,
                                          Phone = or.Phone,
                                          Email = or.Email,
                                          Descriptions = or.Descriptions,
                                          OrderStatus = or.OrderStatus,
                                          TotalPrice = or.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                          NotionalPrice = or.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                          Username = us.Username,
                                      }).ToListAsync();
            return orderlist;


        }

        public async Task<OrderInfoAdminViewModel> GetOrderInfoAdminViewModelByCode(string Code)
        {
            var order = await (from or in DbContext.Orders
                               where or.Code == Code
                               select new OrderInfoAdminViewModel
                               {
                                   Id = or.Id,
                                   Code = or.Code,
                                   CreatedDate = or.CreatedDate == null ? "" : or.CreatedDate.Value.ToString("HH:mm dd/MM/yyyy"),
                                   CustomerName = or.CustomerName,
                                   Address = or.Address,
                                   Phone = or.Phone,
                                   DayOfWeek = TimeHelper.GetDayOfWeek(or.CreatedDate.Value),
                                   PaymentMethod = or.PaymentMethod,
                                   OrderStatus = or.OrderStatus,
                                   TotalAmount = or.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   NotionalAmount = or.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   DiscountAmount = (or.TotalPrice - or.NotionalPrice).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                   OrderDetailAdminViewModels = (from od in DbContext.OrderDetails
                                                                    join pa in DbContext.ProductAttributes on od.ProductAttributeId equals pa.Id
                                                                    join p in DbContext.Products on pa.ProductId equals p.Id
                                                                    join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                    from pc in pcl.DefaultIfEmpty()
                                                                    join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                    from ps in psl.DefaultIfEmpty()
                                                                    join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                                                    where od.OrderId == or.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                                                    select new OrderDetailAdminViewModel
                                                                    {
                                                                        ProductAttributeId = pa.Id,
                                                                        ProductName = p.Name,
                                                                        ProductColor = pc.Name,
                                                                        ProductSize = ps.Name,
                                                                        ProductImage = pi.ImageLink,
                                                                        Price = od.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                        Quantity = od.Quantity,
                                                                        TotalPrice = (od.Price * od.Quantity).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                    }).ToList()
                               }).FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<OrderNewAdminViewModel>> GetOrderNewAdminViewModels()
        {
            var orders = await (from or in DbContext.Orders
                                orderby or.CreatedDate descending
                                where or.OrderStatus != OrderStatus.DeliveredSuccess && or.OrderStatus != OrderStatus.Cancle
                                select new OrderNewAdminViewModel
                                {
                                    Code = or.Code,
                                    TotalPrice = or.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                    OrderStatus = or.OrderStatus,
                                    ProductOrderedAdminViewModels = (from od in DbContext.OrderDetails
                                                                     join pa in DbContext.ProductAttributes on od.ProductAttributeId equals pa.Id
                                                                     join p in DbContext.Products on pa.ProductId equals p.Id
                                                                     join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                     from pc in pcl.DefaultIfEmpty()
                                                                     join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                     from ps in psl.DefaultIfEmpty()
                                                                     where od.OrderId == or.Id
                                                                     select new ProductOrderedAdminViewModel
                                                                     {
                                                                         ProductName = p.Name,
                                                                         ProductColor = pc.Name,
                                                                         ProductSize = ps.Name,
                                                                         Quantity = od.Quantity
                                                                     }).ToList()
                                }).Take(10).ToListAsync();
            return orders;
        }

        public async Task<List<RevenueAdminViewModel>> GetRevenueMonthAdminViewModels()
        {
            var revenue = await (from or in DbContext.Orders
                                 orderby or.CreatedDate descending
                                 where or.CreatedDate.Value.Month == DateTime.Now.Month /*&& or.CreatedDate.Value.Year == DateTime.Now.Year*/
                                 group new { or.CreatedDate, or.TotalPrice } by or.CreatedDate.Value.Date into g
                                 select new RevenueAdminViewModel
                                 { 
                                     Date = g.Select(x => x.CreatedDate.Value.Day.ToString()).FirstOrDefault() + TimeHelper.GetDaySuffix(g.Select(x => x.CreatedDate.Value.Day).FirstOrDefault()),
                                     TotalRevenue = g.Select(x => x.TotalPrice).Sum()
                                 }).ToListAsync();
            return revenue;
        }
        public async Task<List<RevenueAdminViewModel>> GetRevenueYearAdminViewModels()
        {
            var revenue = await (from or in DbContext.Orders
                                 orderby or.CreatedDate descending
                                 where or.CreatedDate.Value.Year == DateTime.Now.Year
                                 group new { or.CreatedDate, or.TotalPrice } by or.CreatedDate.Value.Date into g
                                 select new RevenueAdminViewModel
                                 { 
                                     Date = g.Select(x => x.CreatedDate.Value.Day.ToString()).FirstOrDefault() + TimeHelper.GetDaySuffix(g.Select(x => x.CreatedDate.Value.Day).FirstOrDefault()),
                                     TotalRevenue = g.Select(x => x.TotalPrice).Sum()
                                 }).ToListAsync();
            return revenue;
        }
        public async Task<List<RevenueAdminViewModel>> GetRevenueDayAdminViewModels()
        {
            var revenue = await (from or in DbContext.Orders
                                 orderby or.CreatedDate descending
                                 where or.CreatedDate.Value.Day == DateTime.Now.Day
                                 group new { or.CreatedDate, or.TotalPrice } by or.CreatedDate.Value.Date into g
                                 select new RevenueAdminViewModel
                                 {
                                     Date = g.Select(x => x.CreatedDate.Value.Day.ToString()).FirstOrDefault() + TimeHelper.GetDaySuffix(g.Select(x => x.CreatedDate.Value.Day).FirstOrDefault()),
                                     TotalRevenue = g.Select(x => x.TotalPrice).Sum()
                                 }).ToListAsync();
            return revenue;
        }
        public async Task<List<OrderAdminViewModel>> GetOrderProcessAdminViewModels()
        {
            var orderprocess = await (from or in DbContext.Orders
                                   join us in DbContext.Users
                                   on or.UserId equals us.Id
                                   where or.IsDeleted == false && or.OrderStatus != OrderStatus.DeliveredSuccess && or.OrderStatus != OrderStatus.Cancle
                                   select new OrderAdminViewModel
                                   {
                                       Id = or.Id,
                                       Code = or.Code,
                                       CustomerName = or.CustomerName,
                                       Address = or.Address,
                                       Phone = or.Phone,
                                       Email = or.Email,
                                       Descriptions = or.Descriptions,
                                       OrderStatus = or.OrderStatus,
                                       TotalPrice = or.TotalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                       NotionalPrice = or.NotionalPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                       Username = us.Username,
                                   }).ToListAsync();
            return orderprocess;


        }

        
    }
}
