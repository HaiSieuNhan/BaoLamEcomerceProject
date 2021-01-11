using Ecommerce.Service.ViewModels.Admin.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CouponModel
{
     public class EditCouponModel
    {
        public EditCouponViewModel EditCouponViewModel { get; set; }
        public List<SelectOptionViewModel> CollectionList { get; set; }
        public List<SelectOptionViewModel> CategoryList { get; set; }
    }
}
