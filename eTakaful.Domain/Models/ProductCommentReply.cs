using EcommerceCommon.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web;

namespace Ecommerce.Domain.Models
{
    public class ProductCommentReply : BaseModel
    {
        [MinLength(30, ErrorMessage = "Nội dung chứa ít nhất 30 ký tự")]
        [Required(ErrorMessage = "Nội dung chứa ít nhất 30 ký tự")]
        public string Comment { get; set; }
        public ProductCommentStatus ProducCommentStatus { get; set; } = ProductCommentStatus.Qualified;
        public Guid UserId { get; set; }
        #region Relationship

        public Guid ProductCommentId { get; set; }
        public virtual ProductComment ProductComment { get; set; }
        #endregion
    }
}
