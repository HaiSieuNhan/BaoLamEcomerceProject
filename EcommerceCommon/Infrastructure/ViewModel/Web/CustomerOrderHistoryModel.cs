using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CustomerOrderHistoryModel
    {
        public List<CustomerOrderHistoryViewModel> CustomerOrderHistoryViewModels { get; set; }
        public List<OrderHistoryDate> OrderHistoryDates { get; set; }
    }
}
