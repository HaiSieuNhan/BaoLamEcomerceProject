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
    public class ProductComment : BaseModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tiêu đề nhận xét")]
        public string Title { get; set; }
        [MinLength(50,ErrorMessage = "Nội dung chứa ít nhất 50 ký tự")]
        [Required(ErrorMessage = "Nội dung chứa ít nhất 50 ký tự")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn đánh giá của bạn về sản phẩm này")]
        public int? Rating { get; set; }
        public ProductCommentStatus ProducCommentStatus { get; set; } = ProductCommentStatus.Qualified;
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        #region Relationship
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductCommentImage> ProductCommentImages { get; set; }
        public virtual ICollection<ProductCommentReply> ProductCommentReplys { get; set; }
        #endregion
    }
}
