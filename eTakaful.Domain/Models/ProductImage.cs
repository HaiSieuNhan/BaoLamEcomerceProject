using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class ProductImage :BaseModel
    {
        public string ImageLink { get; set; }
        public bool IsMainImage { get; set; }
        public Guid? ProductColorId { get; set; }
        #region Relationship
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
