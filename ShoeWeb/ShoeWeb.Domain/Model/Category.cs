using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Category :BaseModel
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public Guid ParentId { get; set; }
        public int ParentLevel { get; set; }
        #region Relationship
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
