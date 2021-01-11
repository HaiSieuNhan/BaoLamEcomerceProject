using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public int Sort { get; set; }
       
    }
}
