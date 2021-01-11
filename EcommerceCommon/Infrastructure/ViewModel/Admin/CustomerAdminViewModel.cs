using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Admin
{
    public class CustomerAdminViewModel:BaseViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
