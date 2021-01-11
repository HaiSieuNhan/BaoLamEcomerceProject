using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductColorModel;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ProductColorService : EcommerceServices<ProductColor>, IProductColorService
    {
        private readonly IProductColorRepository _productColorRepository;
        private readonly IMapper _mapper;


        public ProductColorService(IProductColorRepository productColorRepository, IMapper mapper) : base(productColorRepository)
        {
            _productColorRepository = productColorRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductColorViewModel>> GetListProductColorViewModel()
        {
            var productColor = await _productColorRepository.GetListProductColorViewModel();
            return productColor;
        }

        public async Task<List<ProductColorViewModel>> GetProductColorByProductId(Guid ProductId)
        {
            var productColor = await _productColorRepository.GetListProductColorByProductId(ProductId);
            return productColor;
        }
        public async Task<bool> AddProductColorAsync(AddProductColorViewModel addProductColorViewModel)
        {
            try
            {
                var productColor = _mapper.Map<ProductColor>(addProductColorViewModel);
                await _productColorRepository.AddAsync(productColor);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductColorAsync(Guid Id)
        {
            try
            {
                var productColor = await _productColorRepository.GetByIdAsync(Id);
                if (productColor == null)
                {
                    return false;
                }
                await _productColorRepository.DeleteAsync(productColor);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditProductColorAsync(EditProductColorViewModel editProductColorViewModel)
        {
            try
            {
                var productColor = await _productColorRepository.GetByIdAsync(editProductColorViewModel.Id);
                if (productColor == null)
                {
                    return false;
                }
                productColor.UpdatedDate = DateTime.Now;
                productColor.Name = editProductColorViewModel.Name;
                await _productColorRepository.UpdateAsync(productColor);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<EditProductColorViewModel> GetEditProductColorViewModel(Guid Id)
        {
            var productColor = await _productColorRepository.GetByIdAsync(Id);
            if (productColor == null)
                return null;
            return _mapper.Map<EditProductColorViewModel>(productColor);
        }

        public async Task<List<ProductColorAdminViewModel>> GetProductColorAdminViewModels()
        {
            var productColor = await _productColorRepository.GetAllAsync();
            return _mapper.Map<List<ProductColorAdminViewModel>>(productColor);
        }
    }
}
