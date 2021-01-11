using Ecommerce.Domain.Models;
using System.Collections.Generic;

namespace Ecommerce.Service.ViewModels.Admin.AddProduct
{
    public class AddProductModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Supplier> Supplier { get; set; }
        public IEnumerable<Manufacturer> Manufacturer { get; set; }
        public IEnumerable<Collection> Collections { get; set; }
        public IEnumerable <ProductAttribute> ProductAttributes { get; set; }

    }
}
