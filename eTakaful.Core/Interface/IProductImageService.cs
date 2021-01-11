using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.ProductImageModel;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IProductImageService :IServices<ProductImage>
    {
        Task<IList<ProductImageViewModel>> GetProductImageByProductId(Guid ProductId);
        Task<IList<ProductImageViewModel>> GetProductImageByProductColor(Guid ProductId, Guid ProductColorId);
        Task<IList<ProductImageFirstAttributeViewModel>> GetProductImageFirstAttribute(Guid ProductAttributeId);
        Task<List<ProductImageAdminViewModel>> GetProductImageAdminViewModels(Guid ProductId);
        Task<AddProductImageModel> GetAddProductImageModel();
        Task<EditProductImageModel> GetEditProductImageModel(Guid Id);
        Task<AddProductImageModel> GetAddProductImageModel(AddProductImageViewModel addProductImageViewModel);
        Task<EditProductImageModel> GetEditProductImageModel(EditProductImageViewModel editProductImageViewModel);
        Task<bool> AddProductImageAsync(AddProductImageViewModel addProductImageViewModel, string wwwRootPath);
        Task<bool> EditProductImageAsync(EditProductImageViewModel editProductImageViewModel);
        Task<bool> DeleteProductImageAsync(Guid Id, string wwwRootPath);
    }
}
