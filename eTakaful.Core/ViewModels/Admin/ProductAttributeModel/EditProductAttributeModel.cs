using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductAttributeModel
{
    public class EditProductAttributeModel 
    {
        public EditProductAttributeViewModel EditProductAttributeViewModel { get; set; }
        public List<SelectOptionViewModel> ProductColors { get; set; }
        public List<SelectOptionViewModel> ProductSizes { get; set; }
    }
}
