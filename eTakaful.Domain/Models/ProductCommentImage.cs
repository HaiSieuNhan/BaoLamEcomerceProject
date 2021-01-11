using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class ProductCommentImage:BaseModel
    {
        public string ImageLink { get; set; }
        #region Relationship
        public Guid ProductCommentId { get; set; }
        public virtual ProductComment ProductComment { get; set; }
        #endregion
    }
}
