using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductAttributeModel
{
    public class AddProductAttributeModel
    {
        public AddProductAttributeViewModel AddProductAttributeViewModel { get; set; }
        public List<SelectOptionViewModel> ProductColors { get; set; }
        public List<SelectOptionViewModel> ProductSizes { get; set; }

    }
}
