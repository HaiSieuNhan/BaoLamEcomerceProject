using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Attribute : BaseModel
    {
        public int Size { get; set; }
        [StringLength(100)]
        public string Color { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        #region Relationship
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
