using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CategoryModel
{
    public class EditCategoryViewModel : AddCategoryViewModel 
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
