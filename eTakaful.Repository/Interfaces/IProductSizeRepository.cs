using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductSizeRepository : IRepository<ProductSize>
    {
        Task<List<ProductSizeViewModel>> GetListProductSizeByProductId(Guid ProductId);
        Task<List<ProductSizeViewModel>> GetListProductSizeViewModel();
        Task<List<ProductSizeViewModel>> GetListProductSizeByProductColor(Guid ProductId, Guid? ProductColorId);
    }
}
