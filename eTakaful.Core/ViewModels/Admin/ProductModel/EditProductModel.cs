using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductModel
{
    public class EditProductModel 
    {
        public EditProductViewModel EditProductViewModel { get; set; }
        public List<SelectOptionViewModel> Collections { get; set; }
        public List<SelectOptionViewModel> Categories { get; set; }
        public List<SelectOptionViewModel> Manufacturers { get; set; }
        public List<SelectOptionViewModel> Suppliers { get; set; }
    }
}
