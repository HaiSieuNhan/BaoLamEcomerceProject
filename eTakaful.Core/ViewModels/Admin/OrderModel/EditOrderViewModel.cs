using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.OrderModel
{
    public class EditOrderViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Mã đơn hàng")]
        public string Code { get; set; }
        [DisplayName("Tên khách hàng")]
        public string CustomerName { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [DisplayName("Trạng thái đơn hàng")]
        public string OrderStatus { get; set; }
    }
}
