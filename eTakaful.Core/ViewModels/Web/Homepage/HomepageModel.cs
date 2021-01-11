using Ecommerce.Service.Dto;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Service.ViewModels.Web.Homepage
{
    public class HomepageModel
    {
        public IList<CollectionHomepageViewModel> CollectionHomepageViewModel { get; set; }
        public List<ProductHomepage> ProductHomepage { get; set; }
        public IList<CollectionViewModel> CollectionViewModel { get; set; }



    }
}
