using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<ProductHomepage>> GetHotTrendProduct();
        Task<List<ProductHomepage>> GetProductTopView(string searchString);
        Task<List<ProductHomepage>> GetAllProduct(string searchString);
        Task<List<ProductHomepage>> GetProductByCategory(Guid CategoryId);
        Task<List<ProductAdminViewModel>> GetProductListAdminViewModel();
        Task<ProductCartViewModel> GetProductCartViewModel(Guid ProductId,Guid? ProductSizeId,Guid? ProductColorId);
        Task<ProductFirstAttributeViewModel> GetProductFirstAttributeViewModel(Guid ProductId);
        Task<ProductDetailAdminViewModel> GetProductDetailAdminViewModel(Guid Id);
        Task<List<ProductOrderedAdminViewModel>> GetProductOrderedAdminViewModels();
        Task<List<ProductRemainAdminViewModel>> GetProductRemainAdminViewModels();
    } 
   
}
