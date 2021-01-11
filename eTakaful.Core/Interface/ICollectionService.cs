using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.CollectionModel;
using Ecommerce.Service.ViewModels.Admin.ProductColorModel;
using Ecommerce.Service.ViewModels.Web.Homepage;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface ICollectionService : IServices<Collection>
    {
        Task<IList<CollectionHomepageViewModel>> GetCollectionHomepage();

        Task<IList<CollectionViewModel>> GetListCollectionHomepage();
        Task<List<CollectionAdminViewModel>> GetCollectionAdminViewModels();
        Task<bool> AddCollectionAsync(Collection collection, string wwwRootPath);

        Task<EditCollectionViewModel> GetEditCollectionViewModel(Guid Id);

        Task<bool> EditCollectionAsync(EditCollectionViewModel editCollectionViewModel, string wwwRootPath);

        Task<bool> DeleteCollectionAsync(Guid Id, string wwwRootPath);

    }


}

