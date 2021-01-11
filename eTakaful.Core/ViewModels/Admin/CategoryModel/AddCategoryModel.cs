using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CategoryModel
{
    public class AddCategoryModel 
    {
        public AddCategoryViewModel AddCategoryViewModel { get; set; }
        public List<SelectOptionViewModel> CategoryParents { get; set; }
    }
}
