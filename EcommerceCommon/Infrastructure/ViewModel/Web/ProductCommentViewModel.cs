using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductCommentViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public int? Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<ProductCommentImageViewModel> ListImage { get; set; }
        public List<ProductCommentReplyViewModel> ListReply { get; set; }
    }
}
