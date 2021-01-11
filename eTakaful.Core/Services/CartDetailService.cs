using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class CartDetailService : EcommerceServices<CartDetail>, ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartDetailService(ICartDetailRepository cartDetailRepository, IMapper mapper, ICartRepository cartRepository) : base(cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<int> GetCartCount(Guid UserId)
        {
            var cartDetail = await _cartDetailRepository.GetListCartDetailByUserId(UserId);
            var cartCount = 0;

                foreach (var item in cartDetail)
                {
                    cartCount += item.Quantity;
                }

            return cartCount;
        }


        public async Task<List<CartDetailViewModel>> GetListCartDetailByUserId(Guid UserId)
        {
            var cartDetail = await _cartDetailRepository.GetListCartDetailByUserId(UserId);
            return cartDetail;
        }

        public async Task<bool> DeleteCartDetail(Guid CartDetailId)
        {
            var cartdetail = await _cartDetailRepository.GetByIdAsync(CartDetailId);

            if (cartdetail == null)
            {
                return false;
            }
            else
            {
                var cart = await _cartRepository.FindAsync(x => x.Id == CartDetailId);
                await _cartDetailRepository.DeleteAsync(cartdetail);
                if(cartdetail.DiscountPrice == 0)
                {
                    cart.TotalPrice = cart.TotalPrice - (cartdetail.Price * cartdetail.Quantity);
                    cart.NotionalPrice = cartdetail.Price;
                }else
                {
                    cart.TotalPrice = cart.TotalPrice - (cartdetail.DiscountPrice * cartdetail.Quantity);
                    cart.NotionalPrice = cartdetail.DiscountPrice;
                }
                return true;

            }

        }
    }
}
