using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public static class OrderStatus
    {
        public const string OrderSuccess = "Đặt hàng thành công";
        public const string PickingUp = "Đang lấy hàng";
        public const string PackageDone = "Đóng gói xong";
        public const string Delivering = "Đang vận chuyển";
        public const string DeliveredSuccess = "Giao hàng thành công";
        public const string Cancle = "Đã hủy";
    }
}
