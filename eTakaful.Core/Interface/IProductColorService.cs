using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.ProductColorModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IProductColorService : IServices<ProductColor>
    {
        Task<List<ProductColorViewModel>> GetProductColorByProductId(Guid ProductId);
        Task<List<ProductColorViewModel>> GetListProductColorViewModel();
        Task<List<ProductColorAdminViewModel>> GetProductColorAdminViewModels();
        Task<bool> AddProductColorAsync(AddProductColorViewModel addProductColorViewModel);
        Task<EditProductColorViewModel> GetEditProductColorViewModel(Guid Id);
        Task<bool> EditProductColorAsync(EditProductColorViewModel editProductColorViewModel);
        Task<bool> DeleteProductColorAsync(Guid Id);
    }
}
