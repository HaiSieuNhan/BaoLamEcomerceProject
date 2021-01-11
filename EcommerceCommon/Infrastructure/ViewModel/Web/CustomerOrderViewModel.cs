using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CustomerOrderViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CreatedDate { get; set; }
        public string TotalAmount { get; set; }
        public string NotionalAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PaymentMethod { get; set; }
        public List<CustomerOrderDetailViewModel> CustomerOrderDetailViewModels { get; set; }
    }
}
