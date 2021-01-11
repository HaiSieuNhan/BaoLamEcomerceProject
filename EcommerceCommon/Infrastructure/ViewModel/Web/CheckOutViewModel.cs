using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CheckOutViewModel
    {
        public CheckOutInfoViewModel CheckOutInfoViewModel { get; set; }
        public CartViewModel CartViewModel { get; set; }
    }
}
