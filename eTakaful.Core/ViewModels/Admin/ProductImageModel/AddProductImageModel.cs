using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductImageModel
{
    public class AddProductImageModel 
    {
        public AddProductImageViewModel AddProductImageViewModel { get; set; }
        public List<SelectOptionViewModel> ProductColors { get; set; }

    }
}
