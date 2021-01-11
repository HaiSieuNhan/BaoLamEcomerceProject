using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using EcommerceCommon.Infrastructure.Enums;

namespace Ecommerce.Service.ViewModels.Admin.ManufacturerModel
{
    public class EditManufacturerViewModel : AddManufacturerViewModel
    {
        public Guid Id { get; set; }
    }
}
