using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductSizeModel;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ProductSizeService : EcommerceServices<ProductSize>, IProductSizeService
    {
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IMapper _mapper;


        public ProductSizeService(IProductSizeRepository productSizeRepository, IMapper mapper) : base(productSizeRepository)
        {
            _productSizeRepository = productSizeRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductSizeViewModel>> GetListProductSizeByProductColor(Guid ProductId, Guid? ProductColorId)
        {
            var productSize = await _productSizeRepository.GetListProductSizeByProductColor(ProductId,ProductColorId);
            return productSize;
        }

        public async Task<List<ProductSizeViewModel>> GetListProductSizeViewModel()
        {
            var productSize = await _productSizeRepository.GetListProductSizeViewModel();
            return productSize;
        }

        public async Task<List<ProductSizeViewModel>> GetProductSizeByProductId(Guid ProductId)
        {
            var productSize = await _productSizeRepository.GetListProductSizeByProductId(ProductId);
            return productSize;
        }
        public async Task<bool> AddProductSizeAsync(AddProductSizeViewModel addProductSizeViewModel)
        {
            try
            {
                var productSize = _mapper.Map<ProductSize>(addProductSizeViewModel);
                await _productSizeRepository.AddAsync(productSize);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductSizeAsync(Guid Id)
        {
            try
            {
                var productSize = await _productSizeRepository.GetByIdAsync(Id);
                if (productSize == null)
                {
                    return false;
                }
                await _productSizeRepository.DeleteAsync(productSize);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditProductSizeAsync(EditProductSizeViewModel editProductSizeViewModel)
        {
            try
            {
                var productSize = await _productSizeRepository.GetByIdAsync(editProductSizeViewModel.Id);
                if (productSize == null)
                {
                    return false;
                }
                productSize.UpdatedDate = DateTime.Now;
                productSize.Name = editProductSizeViewModel.Name;
                await _productSizeRepository.UpdateAsync(productSize);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<EditProductSizeViewModel> GetEditProductSizeViewModel(Guid Id)
        {
            var productSize = await _productSizeRepository.GetByIdAsync(Id);
            if (productSize == null)
                return null;
            return _mapper.Map<EditProductSizeViewModel>(productSize);
        }

        public async Task<List<ProductSizeAdminViewModel>> GetProductSizeAdminViewModels()
        {
            var productSize = await _productSizeRepository.GetAllAsync();
            return _mapper.Map<List<ProductSizeAdminViewModel>>(productSize);
        }
    }
}
