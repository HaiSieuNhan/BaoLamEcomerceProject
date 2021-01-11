using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Web.ProductList;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
       Task<ICollection<Category>> GetCategoryParrent();
        Task<List<CategoryLevel0ViewModel>> GetCategoryLevel0ViewModel();
        Task<List<CategoryProductListViewModel>> GetListCategoryProductListViewModel(Guid? ParentId);
    }
}
