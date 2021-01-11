using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class Cart :BaseModel
    {
        public string CartName { get; set; } = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8).Select(s => s[new Random().Next(s.Length)]).ToArray());
        public decimal NotionalPrice { get; set; }
        public decimal TotalPrice { get; set; }       
        public CartStatus CartStatus { get; set; } = CartStatus.PreOrder;
        #region Relationship
        public virtual ICollection<CouponApply> CouponApplies { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
