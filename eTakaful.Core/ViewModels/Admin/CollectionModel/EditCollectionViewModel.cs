using EcommerceCommon.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Admin.CollectionModel
{
    public class EditCollectionViewModel : AddCollectionViewModel
    {
        public Guid Id { get; set; }
    }
}
