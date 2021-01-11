using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductImageRepository: IRepository<ProductImage>
    {
        Task<List<ProductImageFirstAttributeViewModel>> GetProductImageFirstAttribute(Guid ProductAttributeId);
        Task<List<ProductImageAdminViewModel>> GetListProductImageAdminViewModel(Guid ProductId);
    }
}
