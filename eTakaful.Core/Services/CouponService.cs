using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.CouponModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using CouponAdminViewModel = EcommerceCommon.Infrastructure.ViewModel.Admin.CouponAdminViewModel;

namespace Ecommerce.Service.Services
{
    public class CouponService : EcommerceServices<Coupon>, ICouponService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponService(ICouponRepository couponRepository, IMapper mapper, ICategoryRepository categoryRepository, ICollectionRepository collectionRepository) : base(couponRepository)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _collectionRepository = collectionRepository;
        }


        public async Task<List<CouponAdminViewModel>> GetCouponAdminViewModels()
        {
            var coupon = await _couponRepository.GetCouponAdminViewModels();
            return coupon;
            
        }


        public async Task<AddCouponModel> GetAddCouponModel()
        {
            var listCategory = await _categoryRepository.GetAllAsync();
            var listCollection = await _collectionRepository.GetAllAsync();
            var model = new AddCouponModel
            {
                CategoryList = _mapper.Map<List<SelectOptionViewModel>>(listCategory),
                CollectionList = _mapper.Map<List<SelectOptionViewModel>>(listCollection)
            };

            return model;
        }

        public async Task<AddCouponModel> GetAddCouponViewModels(AddCouponViewModel AddCouponModel)
        {
            var listCategory = await _categoryRepository.GetAllAsync();
            var listCollection = await _collectionRepository.GetAllAsync();
            var model = new AddCouponModel
            {
                CategoryList = _mapper.Map<List<SelectOptionViewModel>>(listCategory),
                CollectionList = _mapper.Map<List<SelectOptionViewModel>>(listCollection),
                AddCouponViewModel = AddCouponModel
            };
            return model;
        }

        public async Task<bool> AddCouponAsync(AddCouponViewModel AddCouponViewModel)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(AddCouponViewModel);
                await _couponRepository.AddAsync(coupon);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<EditCouponModel> GetEditCouponModel(Guid Id)
        {
            var coupon = await _couponRepository.GetByIdAsync(Id);
            if (coupon == null)
                return null;
            var listCategory = await _categoryRepository.GetAllAsync();
            var listCollection = await _collectionRepository.GetAllAsync();
            var model = new EditCouponModel
            {
                CategoryList = _mapper.Map<List<SelectOptionViewModel>>(listCategory),
                CollectionList = _mapper.Map<List<SelectOptionViewModel>>(listCollection),
                EditCouponViewModel = _mapper.Map<EditCouponViewModel>(coupon)
            };
            return model;
        }

        public async Task<EditCouponModel> GetEditCouponModel(EditCouponViewModel editCouponViewModel)
        {
            var coupon = await _couponRepository.GetByIdAsync(editCouponViewModel.Id);
            if (coupon == null)
                return null;
            var listCategory = await _categoryRepository.GetAllAsync();
            var listCollection = await _collectionRepository.GetAllAsync();
            var model = new EditCouponModel
            {
                CategoryList = _mapper.Map<List<SelectOptionViewModel>>(listCategory),
                CollectionList = _mapper.Map<List<SelectOptionViewModel>>(listCollection),
                EditCouponViewModel = editCouponViewModel
            };
            return model;
        }


        public async Task<bool> EditCouponAsync(EditCouponViewModel editCouponViewModel)
        {
            try
            {
                var coupon = await _couponRepository.GetByIdAsync(editCouponViewModel.Id);
                if (coupon == null)
                {
                    return false;
                }
                coupon.Name = editCouponViewModel.Name;
                coupon.NumberApply = Int32.Parse(editCouponViewModel.NumberApply);
                coupon.Amount = decimal.Parse(editCouponViewModel.Amount.Replace(".", ""));
                if(editCouponViewModel.StartTime != null)
                {
                    coupon.StartTime = DateTime.ParseExact(editCouponViewModel.StartTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    coupon.StartTime = null;
                }
                if(editCouponViewModel.EndTime != null)
                {
                    coupon.EndTime = DateTime.ParseExact(editCouponViewModel.EndTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    coupon.EndTime = null;
                }             
                coupon.CategoryId = editCouponViewModel.CategoryId;
                coupon.CollectionId = editCouponViewModel.CollectionId;
                coupon.UpdatedDate = DateTime.Now;
                await _couponRepository.UpdateAsync(coupon);


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCouponAsync(Guid Id)
        {
            try
            {
                var coupon = await _couponRepository.GetByIdAsync(Id);
                if (coupon == null)
                {
                    return false;
                }
             
                await _couponRepository.DeleteAsync(coupon);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
