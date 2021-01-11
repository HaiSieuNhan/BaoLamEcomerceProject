using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.ProductList
{
    public class CategoryProductListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string URLImage { get; set; }
        public Guid? ParentId { get; set; }
        public int ProductCount { get; set; }
        public List<CategoryProductListViewModel> CategoryChildren { get; set; }
    }
}
