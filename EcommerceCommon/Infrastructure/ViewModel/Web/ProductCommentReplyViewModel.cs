using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductCommentReplyViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
