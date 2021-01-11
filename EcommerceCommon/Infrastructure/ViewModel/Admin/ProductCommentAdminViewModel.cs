using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class ProductCommentAdminViewModel :BaseViewModel
    {
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public string Username { get; set; }
        public string NameProduct { get; set; }

    }
}
