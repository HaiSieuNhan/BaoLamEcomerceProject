using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.ProductDetail
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }
        public string ImageLink { get; set; }
        public bool IsMainImage { get; set; }
        public Guid? ProductColorId { get; set; }
    }
}
