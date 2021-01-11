using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class Order :BaseModel
    {
        public string Code { get; set; } = new string(Enumerable.Repeat("0123456789", 9).Select(s => s[new Random().Next(s.Length)]).ToArray());
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }       
        [StringLength(100)]
        public string Email { get; set; }        
        public string Descriptions { get; set; }
        public string OrderStatus { get; set; } 
        public decimal NotionalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        #region Relationship
        public Guid UserId { get; set; } 
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
       
        #endregion
    }

    // Thieu order status  = > enum
    // thieu order history : thoi gian lich su tung giai doan , so luon san pham mua hang
}
