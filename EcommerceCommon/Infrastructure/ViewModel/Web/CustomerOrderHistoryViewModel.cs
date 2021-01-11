using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CustomerOrderHistoryViewModel
    {
        public Guid Id { get; set; }
        public string OrderStatus { get; set; }
        public string DayOfWeek { get; set; }
        public string Hour { get; set; }
        public string Date { get; set; }
    }
}
