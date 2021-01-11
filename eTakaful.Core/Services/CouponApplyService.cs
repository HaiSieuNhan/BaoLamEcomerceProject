using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class CouponApplyService : EcommerceServices<CouponApply>, ICouponApplyService
    {
        private readonly ICouponApplyRepository _couponApplyRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IMapper _mapper;

        public CouponApplyService(ICouponApplyRepository couponApplyRepository, IMapper mapper, ICouponRepository couponRepository, ICartRepository cartRepository) : base(couponApplyRepository)
        {
            _couponApplyRepository = couponApplyRepository;
            _mapper = mapper;
            _couponRepository = couponRepository;
            _cartRepository = cartRepository;
        }

        
    }
}
