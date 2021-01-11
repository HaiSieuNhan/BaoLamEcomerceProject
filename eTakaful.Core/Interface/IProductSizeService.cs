using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.ProductSizeModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IProductSizeService : IServices<ProductSize>
    {
        Task<List<ProductSizeViewModel>> GetProductSizeByProductId(Guid ProductId);
        Task<List<ProductSizeViewModel>> GetListProductSizeViewModel();
        Task<List<ProductSizeViewModel>> GetListProductSizeByProductColor(Guid ProductId, Guid? ProductColorId);
        Task<List<ProductSizeAdminViewModel>> GetProductSizeAdminViewModels();
        Task<bool> AddProductSizeAsync(AddProductSizeViewModel addProductSizeViewModel);
        Task<EditProductSizeViewModel> GetEditProductSizeViewModel(Guid Id);
        Task<bool> EditProductSizeAsync(EditProductSizeViewModel editProductSizeViewModel);
        Task<bool> DeleteProductSizeAsync(Guid Id);
    }
}
