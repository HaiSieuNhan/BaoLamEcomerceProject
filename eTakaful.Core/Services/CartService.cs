using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class CartService : EcommerceServices<Cart>, ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly ICouponApplyRepository _couponApplyRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, ICartDetailRepository cartDetailRepository, IMapper mapper, IProductAttributeRepository productAttributeRepository, ICouponApplyRepository couponApplyRepository, ICouponRepository couponRepository) : base(cartRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cartDetailRepository = cartDetailRepository;
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
            _couponApplyRepository = couponApplyRepository;
            _couponRepository = couponRepository;
        }

        public async Task<bool> AddCart(Guid ProductId ,Guid? ProductSizeId ,Guid? ProductColorId, int Quantity, Guid UserId)
        {
            ProductCartViewModel product = await _productRepository.GetProductCartViewModel(ProductId,ProductSizeId,ProductColorId);
            var cart = await _cartRepository.GetFirstOrDefaultAsync(x => x.UserId == UserId && x.CartStatus == CartStatus.PreOrder);
            if (cart == null)
            {
                if (Quantity <= product.CountStock)
                {
                    Cart CartNew = new Cart();
                    CartNew.UserId = UserId;
                    if (product.DiscountPrice != 0)
                    {
                        CartNew.NotionalPrice = product.DiscountPrice * Quantity;
                        CartNew.TotalPrice = product.DiscountPrice * Quantity;
                    }
                    else
                    {
                        CartNew.NotionalPrice = product.Price * Quantity;
                        CartNew.TotalPrice = product.Price * Quantity;
                    }

                    await _cartRepository.AddAsync(CartNew);
                    CartDetail cartDetailNew = new CartDetail();
                    cartDetailNew.ProductAttributeId = product.Id;
                    cartDetailNew.CartId = CartNew.Id;
                    cartDetailNew.Price = product.Price;
                    cartDetailNew.Quantity = Quantity;
                    cartDetailNew.DiscountPrice = product.DiscountPrice;
                    await _cartDetailRepository.AddAsync(cartDetailNew);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var cartDetail = await _cartDetailRepository.GetFirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductAttributeId== product.Id);
                if(cartDetail == null)
                {
                    if (Quantity <= product.CountStock)
                    {
                        CartDetail cartDetailNew = new CartDetail();
                        cartDetailNew.ProductAttributeId = product.Id;
                        cartDetailNew.CartId = cart.Id;
                        cartDetailNew.Price = product.Price;
                        cartDetailNew.Quantity = Quantity;
                        cartDetailNew.DiscountPrice = product.DiscountPrice;
                        await _cartDetailRepository.AddAsync(cartDetailNew);
                        if (product.DiscountPrice != 0)
                        {
                            cart.NotionalPrice += product.DiscountPrice * Quantity;
                            cart.TotalPrice += product.DiscountPrice * Quantity;
                        }
                        else
                        {
                            cart.NotionalPrice += product.Price * Quantity;
                            cart.TotalPrice += product.Price * Quantity;
                        }
                        await _cartRepository.UpdateAsync(cart);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if((cartDetail.Quantity + Quantity) <= product.CountStock)
                    {
                        cartDetail.Quantity += Quantity;
                        await _cartDetailRepository.UpdateAsync(cartDetail);
                        if (product.DiscountPrice != 0)
                        {
                            cart.NotionalPrice += product.DiscountPrice * Quantity;
                            cart.TotalPrice += product.DiscountPrice * Quantity;
                        }
                        else
                        {
                            cart.NotionalPrice += product.Price * Quantity;
                            cart.TotalPrice += product.Price * Quantity;
                        }
                        await _cartRepository.UpdateAsync(cart);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<Validate> DeleteCart(Guid CartDetailId)
        {
            var cartDetail = await _cartDetailRepository.GetByIdAsync(CartDetailId);
            if(cartDetail != null)
            {
                
                var cart = await _cartRepository.GetByIdAsync(cartDetail.CartId);
                if(cart == null && cart.CartStatus == CartStatus.Ordered)
                {
                    return new Validate { IsValid = false, Message = " Không tìm thấy giỏ hàng" };
                }
                cart.TotalPrice = cart.TotalPrice - (cartDetail.DiscountPrice * cartDetail.Quantity);
                cart.NotionalPrice = cart.NotionalPrice - (cartDetail.DiscountPrice * cartDetail.Quantity);
                 var couponApplies = await _couponApplyRepository.FindAllAsync(x => x.CartId == cart.Id);
                
                await _cartDetailRepository.DeleteAsync(cartDetail);
                await _cartRepository.UpdateAsync(cart);
                var cartDetails = await _cartDetailRepository.FindAllAsync(x=>x.CartId == cart.Id);
                foreach(var item in couponApplies)
                {
                    var coupon = await _couponRepository.GetByIdAsync(item.CouponId);
                    var check = false;
                    foreach (var subItem in cartDetails)
                    {
                        var productAttr = await _productAttributeRepository.GetByIdAsync(subItem.ProductAttributeId);
                        var product = await _productRepository.GetByIdAsync(productAttr.ProductId);
                        
                        if (coupon.CategoryId == product.CategoryId || coupon.CollectionId == product.CollectionId)
                        {
                            check = true;
                            break;
                        }
                    }
                    if(check == false)
                    {
                        coupon.NumberApply++;
                        cart.TotalPrice += coupon.Amount;
                        await _cartRepository.UpdateAsync(cart);
                        await _couponRepository.UpdateAsync(coupon);
                        await _couponApplyRepository.DeleteAsync(item);
                    }
                }
                if(cartDetails.Count == 0)
                {
                    cart.NotionalPrice = 0;
                    cart.TotalPrice = 0;
                    await _cartRepository.UpdateAsync(cart);
                }

                return new Validate { IsValid = true };
            }
            else
            {
                return new Validate { IsValid = false, Message = "Không tìm thấy sản phẩm" };
            }
        }

        public async Task<CartViewModel> GetCartViewModelByUserId(Guid UserId)
        {
            var cart = await _cartRepository.GetCartViewModelByUserId(UserId);
            return cart;
        }

        public async Task<Validate> ApplyCoupon(string CodeName, Guid UserId)
        {
            var coupon = await _couponRepository.GetFirstOrDefaultAsync(x => x.Name.ToLower().Trim() == CodeName.ToLower().Trim());
            if(coupon == null)
            {
                return new Validate { IsValid = false, Message = "Mã giảm giá không tồn tại" };
            }
            if (coupon.NumberApply <= 0)
            {
                return new Validate { IsValid = false, Message = "Mã giảm giá đã hết số lần sử dụng" };
            }
            if (coupon.StartTime != null)
            {
                if (DateTime.Compare(coupon.StartTime.GetValueOrDefault(), DateTime.Now) > 0)
                {
                    return new Validate { IsValid = false, Message = "Mã giảm giá chưa đến hạn sử dụng" };
                }
            }
            if (coupon.EndTime != null)
            {
                if (DateTime.Compare(coupon.EndTime.GetValueOrDefault(), DateTime.Now) < 0)
                {
                    return new Validate { IsValid = false, Message = "Mã giảm giá đã quá hạn sử dụng" };
                }
            }
            var cart = await _cartRepository.GetFirstOrDefaultAsync(x => x.UserId == UserId && x.CartStatus == CartStatus.PreOrder);
            if (cart != null )
            {
                var couponApply = await _couponApplyRepository.FindAllAsync(x => x.CartId == cart.Id);
                if(couponApply.Count > 2)
                {
                    return new Validate { IsValid = false, Message = "Chỉ được áp dụng tối đa 3 mã giảm giá" };
                }
                foreach(var item in couponApply)
                {
                    if(item.CouponId == coupon.Id)
                    {
                        return new Validate { IsValid = false, Message = "Mã giảm giá đã được áp dụng" };
                    }
                }
                var cartDetails = await _cartDetailRepository.FindAllAsync(x => x.CartId == cart.Id);
                if (cartDetails.Count == 0)
                {
                    return new Validate { IsValid = false, Message = "Giỏ hàng không có sản phẩm" };
                }
                foreach (var item in cartDetails)
                {
                    var productAttr = await _productAttributeRepository.GetByIdAsync(item.ProductAttributeId);
                    var product = await _productRepository.GetByIdAsync(productAttr.ProductId);
                    if(coupon.CategoryId == product.CategoryId || coupon.CollectionId == product.CollectionId)
                    {
                        cart.TotalPrice -= coupon.Amount;
                        coupon.NumberApply--;
                        var apply = new CouponApply
                        {
                            CouponId = coupon.Id,
                            CartId = cart.Id
                        };
                        await _couponApplyRepository.AddAsync(apply);
                        await _cartRepository.UpdateAsync(cart);
                        await _couponRepository.UpdateAsync(coupon);
                        return new Validate { IsValid = true };
                    }
                }
                return new Validate { IsValid = false , Message = "Mã giảm giá không áp dụng được cho giỏ hàng này" };
            }
            else
            {
                return new Validate { IsValid = false, Message = "Giỏ hàng không tồn tại" };
            }

        }
        public async Task<Validate> UpdateCart(Guid CartDetailId, int Quantity)
        {
            var cartDetail = await _cartDetailRepository.GetByIdAsync(CartDetailId);
            
            if(cartDetail != null)
            {
                var product = await _productAttributeRepository.GetByIdAsync(cartDetail.ProductAttributeId);
                var cart = await _cartRepository.FindAsync(x => x.Id == cartDetail.CartId);
                if (cart == null)
                {
                    return new Validate { IsValid = false, Message = "Không tìm thấy giỏ hàng !" };
                }else if(product == null)
                {
                    return new Validate { IsValid = false, Message = "Không tìm thấy sản phẩm !" };
                }
                var oldTotalPrice = cartDetail.Quantity * cartDetail.DiscountPrice;
                cartDetail.Price = product.Price;
                cartDetail.DiscountPrice = product.DiscountPrice;
                if (Quantity <= product.CountStock)
                {
                    cart.NotionalPrice = cart.NotionalPrice - oldTotalPrice + (product.DiscountPrice * Quantity);
                    cart.TotalPrice = cart.TotalPrice - oldTotalPrice + (product.DiscountPrice * Quantity);
                    cartDetail.Quantity = Quantity;
                    await _cartDetailRepository.UpdateAsync(cartDetail);
                    await _cartRepository.UpdateAsync(cart);
                    return new Validate { IsValid = true };
                }
                else
                {
                    cart.NotionalPrice = cart.NotionalPrice - oldTotalPrice + (product.DiscountPrice * product.CountStock);
                    cart.TotalPrice = cart.TotalPrice - oldTotalPrice + (product.DiscountPrice * product.CountStock);
                    cartDetail.Quantity = product.CountStock;
                    await _cartDetailRepository.UpdateAsync(cartDetail);
                    await _cartRepository.UpdateAsync(cart);
                    return new Validate { IsValid = false, Message = "Số lượng còn lại của sản phẩm này là "+ product.CountStock + " !" ,Quantity = product.CountStock};
                }
                
            }
            else
            {
                return new Validate { IsValid = false , Message = "Không tìm thấy sản phẩm !" };
            }
        }

        public async Task<List<CartAdminViewModel>> GetCartAdminViewModels()
        {
            var carts = await _cartRepository.GetCartAdminViewModels();
            return carts;
        }

        public async Task<CartAdminViewModel> GetCartAdminViewModelById(Guid Id)
        {
            var cart = await _cartRepository.GetCartAdminViewModelById(Id);
            return cart;
        }
    }
}
