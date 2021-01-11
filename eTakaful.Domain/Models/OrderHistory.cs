using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class OrderHistory :BaseModel
    {
        public string OrderStatus { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        #region Relationship
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        #endregion
    }
}
