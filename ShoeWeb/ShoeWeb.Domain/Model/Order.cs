using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Order :BaseModel
    {
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        #region Relationship
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
