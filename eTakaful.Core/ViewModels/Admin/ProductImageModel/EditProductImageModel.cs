using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductImageModel
{
    public class EditProductImageModel 
    {
        public EditProductImageViewModel EditProductImageViewModel { get; set; }
        public List<SelectOptionViewModel> ProductColors { get; set; }
    }
}
