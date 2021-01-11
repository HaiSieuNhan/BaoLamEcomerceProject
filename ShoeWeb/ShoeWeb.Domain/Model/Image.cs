using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Image :BaseModel
    {
        [StringLength(100)]
        public string Name { get; set; }
        #region Relationship
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
