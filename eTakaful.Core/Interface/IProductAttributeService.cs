using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IProductAttributeService : IServices<ProductAttribute>
    {
        Task<ProductAttributeViewModel> GetProductAttributeByProductColor(Guid ProductId, Guid ProductColorId);
        Task<ProductHomepageAttributeViewModel> GetProductHomepageByProductAttribute(Guid ProductAttributeId);
        Task<ProductAttributeViewModel> GetProductAttributeByProductSize(Guid ProductId, Guid? ProductColorId, Guid ProductSizeId);
        Task<ProductAddCartErrorViewModel> GetProductAddCartError(Guid ProductId, Guid? ProductSizeId, Guid? ProductColorId);
        Task<List<ProductAttributeAdminViewModel>> GetListProductAttributeAdminViewModel(Guid ProductId);

        Task<AddProductAttributeModel> GetAddProductAttributeModel();
        Task<EditProductAttributeModel> GetEditProductAttributeModel(Guid Id);
        Task<AddProductAttributeModel> GetAddProductAttributeModel(AddProductAttributeViewModel addProductAttributeViewModel);
        Task<EditProductAttributeModel> GetEditProductAttributeModel(EditProductAttributeViewModel editProductAttributeViewModel);
        Task<List<ProductAttributeAdminViewModel>> GetProductAttributeAdminViewModels();
        Task<bool> AddProductAttributeAsync(AddProductAttributeViewModel addProductAttributeViewModel);
        Task<bool> EditProductAttributeAsync(EditProductAttributeViewModel editProductAttributeViewModel);
        Task<bool> DeleteProductAttributeAsync(Guid Id);
    }
}
