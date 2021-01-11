using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Interface
{
    public interface ICartService:IServices<Cart>
    {
        Task<List<CartAdminViewModel>> GetCartAdminViewModels();
        Task<CartAdminViewModel> GetCartAdminViewModelById(Guid Id);
        Task<bool> AddCart(Guid ProductId,Guid? ProductSizeId,Guid? ProductColorId , int Quantity,Guid UserId);
        Task<Validate> UpdateCart(Guid CartDetailId, int Quantity);
        Task<Validate> DeleteCart(Guid CartDetailId);
        Task<CartViewModel> GetCartViewModelByUserId(Guid UserId);
        Task<Validate> ApplyCoupon(string CodeName, Guid UserId);
    }
}
