using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class ProductImageFirstAttributeViewModel
    {
        public Guid Id { get; set; }
        public string ImageLink { get; set; }
        public bool IsMainImage { get; set; }
        public Guid? ProductColorId { get; set; }
    }
}
