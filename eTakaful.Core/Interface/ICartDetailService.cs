using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface ICartDetailService : IServices<CartDetail>
    {
        Task<List<CartDetailViewModel>> GetListCartDetailByUserId(Guid UserId);
        Task<int> GetCartCount(Guid UserId);
        //Task<List<>>
        Task<bool> DeleteCartDetail(Guid CartDetail);

    }
}
