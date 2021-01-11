using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.ManufacturerModel
{
    public class ManufacturerAdminViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
    }
}
