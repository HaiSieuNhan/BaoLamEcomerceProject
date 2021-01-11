
using EcommerceCommon.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.ViewModel.Web
{
    public class CategoryNodeTree :ITreeNode<CategoryNodeTree>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryNodeTree Parent { get; set; }
        public IList<CategoryNodeTree> Children { get; set; }

    }
}
