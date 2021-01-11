using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoeWeb.Domain.Model
{
    public class Product:BaseModel
    {
        #region Property
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Keyword { get; set; }

        #endregion

        #region Relationship
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Image> Images { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        #endregion
    }
}
