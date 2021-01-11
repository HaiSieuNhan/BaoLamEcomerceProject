using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class OrderDetail : BaseModel
    {
        public int Quantity { get; set; }
        #region Relationship
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid AttributeId { get; set; }
        public Attribute Attribute { get; set; }

        #endregion
    }
}
