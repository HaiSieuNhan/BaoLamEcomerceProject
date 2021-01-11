using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CategoryLevel0ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<CategoryLevel1ViewModel> CategoryLevel1ViewModel { get; set; }
    }
}
