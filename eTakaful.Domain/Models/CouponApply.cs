using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class CouponApply:BaseModel
    {
        #region Relationship
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public Guid CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        #endregion
    }
}
