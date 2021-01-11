using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductImageColorViewModel
    {
        public Guid Id { get; set; }
        public string URLImage { get; set; }
        public Guid? ProductColorId { get; set; }
    }
}
