﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CustomerOrderDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductAttributeId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}
