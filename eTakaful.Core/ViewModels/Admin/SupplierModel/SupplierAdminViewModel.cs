using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.SupplierModel
{
    public class SupplierAdminViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
