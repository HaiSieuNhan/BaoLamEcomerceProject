using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class OrderDetail :BaseModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        #region Relationship
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductAttributeId { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        #endregion
    }
}
