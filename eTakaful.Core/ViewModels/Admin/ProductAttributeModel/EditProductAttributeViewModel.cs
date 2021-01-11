using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ProductAttributeModel
{
    public class EditProductAttributeViewModel :AddProductAttributeViewModel
    {
        public Guid Id { get; set; }

    }
}
