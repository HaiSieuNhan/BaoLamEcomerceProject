using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.CategoryModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using Ecommerce.Service.ViewModels.Web.Header;
using Ecommerce.Service.ViewModels.Web.ProductList;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Interface
{
    public interface ICategoryService : IServices<Category>
    {
        //Task<ICollection<CategoryViewModel>> GetCategoryParrent();
        Task<IList<CategoryLevel0ViewModel>> GetCategoryLevel0();
        Task<CategoryProductListViewModel> GetCategoryProductList(Guid CategoryID);
        Task<List<CategoryProductListViewModel>> GetListCategoryProductList(Guid? ParentId);
        Task<AddCategoryModel> GetAddCategoryModel();
        Task<EditCategoryModel> GetEditCategoryModel(Guid Id);
        Task<AddCategoryModel> GetAddCategoryModel(AddCategoryViewModel addCategoryViewModel);
        Task<EditCategoryModel> GetEditCategoryModel(EditCategoryViewModel editCategoryViewModel);
        Task<List<CategoryAdminViewModel>> GetCategoryAdminViewModels();
        Task<bool> AddCategoryAsync(AddCategoryViewModel addCategoryViewModel,string wwwRootPath);
        Task<bool> EditCategoryAsync(EditCategoryViewModel editCategoryViewModel,string wwwRootPath);
        Task<bool> DeleteCategoryAsync(Guid Id, string wwwRootPath);
    }
}
