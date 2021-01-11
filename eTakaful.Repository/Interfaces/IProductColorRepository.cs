using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductColorRepository : IRepository<ProductColor>
    {
        Task<List<ProductColorViewModel>> GetListProductColorByProductId(Guid ProductId);
        Task<List<ProductColorViewModel>> GetListProductColorViewModel();
    }
}
