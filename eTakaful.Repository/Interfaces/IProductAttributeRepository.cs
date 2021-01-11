using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductAttributeRepository: IRepository<ProductAttribute>
    {
        
        Task<List<ProductAttributeAdminViewModel>> GetListProductAttributeAdminViewModel(Guid ProductId);
        Task<ProductAttributeViewModel> GetProductAttributeByProductColor(Guid ProductId, Guid ProductColorId);
        Task<ProductHomepageAttributeViewModel> GetProductHomepageByProductAttribute(Guid ProductAttributeId);
        Task<ProductAttributeViewModel> GetProductAttributeByProductSize(Guid ProductId, Guid? ProductColorId, Guid ProductSizeId);
        Task<ProductAddCartErrorViewModel> GetProductAddCartErrorViewModel(Guid ProductId, Guid? ProductSizeId, Guid? ProductColorId);
    }
}
