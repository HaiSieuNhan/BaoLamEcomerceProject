using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class CartDetail :BaseModel
    {
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Quantity { get; set; }
        #region Relationship
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public Guid ProductAttributeId { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        #endregion
    }
}
