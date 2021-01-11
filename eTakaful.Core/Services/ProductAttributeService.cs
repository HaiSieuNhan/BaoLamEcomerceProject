using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ProductAttributeService : EcommerceServices<ProductAttribute>, IProductAttributeService
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly IMapper _mapper;


        public ProductAttributeService(IProductAttributeRepository productAttributeRepository, IMapper mapper, IProductSizeRepository productSizeRepository, IProductColorRepository productColorRepository) : base(productAttributeRepository)
        {
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
            _productSizeRepository = productSizeRepository;
            _productColorRepository = productColorRepository;
        }

        public async Task<bool> AddProductAttributeAsync(AddProductAttributeViewModel addProductAttributeViewModel)
        {
            try
            {
                var productAttribute = _mapper.Map<ProductAttribute>(addProductAttributeViewModel);
                await _productAttributeRepository.AddAsync(productAttribute);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        public async Task<bool> DeleteProductAttributeAsync(Guid Id)
        {
            try
            {
                var productAttribute = await _productAttributeRepository.GetByIdAsync(Id);
                if (productAttribute == null)
                {
                    return false;
                }
                await _productAttributeRepository.DeleteAsync(productAttribute);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditProductAttributeAsync(EditProductAttributeViewModel editProductAttributeViewModel)
        {
            try
            {
                var productAttribute = await _productAttributeRepository.GetByIdAsync(editProductAttributeViewModel.Id);
                if (productAttribute == null)
                {
                    return false;
                }
                productAttribute.UpdatedDate = DateTime.Now;
                productAttribute.ProductId = editProductAttributeViewModel.ProductId;
                productAttribute.ProductColorId = editProductAttributeViewModel.ProductColorId;
                productAttribute.ProductSizeId = editProductAttributeViewModel.ProductSizeId;
                productAttribute.CountStock = Int32.Parse(editProductAttributeViewModel.CountStock);
                productAttribute.Price = decimal.Parse(editProductAttributeViewModel.Price.Replace(".", ""));
                productAttribute.DiscountPrice = decimal.Parse(editProductAttributeViewModel.DiscountPrice.Replace(".", ""));
                await _productAttributeRepository.UpdateAsync(productAttribute);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<AddProductAttributeModel> GetAddProductAttributeModel()
        {
            var productSize = await _productSizeRepository.GetAllAsync();
            var productColor = await _productColorRepository.GetAllAsync();
            var model = new AddProductAttributeModel
            {
                ProductSizes = _mapper.Map<List<SelectOptionViewModel>>(productSize),
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColor)
            };

            return model;
        }

        public async Task<AddProductAttributeModel> GetAddProductAttributeModel(AddProductAttributeViewModel addProductAttributeViewModel)
        {
            var productSize = await _productSizeRepository.GetAllAsync();
            var productColor = await _productColorRepository.GetAllAsync();
            var model = new AddProductAttributeModel
            {
                ProductSizes = _mapper.Map<List<SelectOptionViewModel>>(productSize),
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColor),
                AddProductAttributeViewModel = addProductAttributeViewModel
            };
            return model;
        }

        public async Task<List<ProductAttributeAdminViewModel>> GetProductAttributeAdminViewModels()
        {
            var productAttribute = await _productAttributeRepository.GetAllAsync();
            return _mapper.Map<List<ProductAttributeAdminViewModel>>(productAttribute);
        }
        public async Task<EditProductAttributeModel> GetEditProductAttributeModel(Guid Id)
        {
            var productAttribute = await _productAttributeRepository.GetByIdAsync(Id);
            if (productAttribute == null)
                return null;
            var productSize = await _productSizeRepository.GetAllAsync();
            var productColor = await _productColorRepository.GetAllAsync();
            var model = new EditProductAttributeModel
            {
                ProductSizes = _mapper.Map<List<SelectOptionViewModel>>(productSize),
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColor),
                EditProductAttributeViewModel = _mapper.Map<EditProductAttributeViewModel>(productAttribute)
            };
            return model;
        }

        public async Task<EditProductAttributeModel> GetEditProductAttributeModel(EditProductAttributeViewModel editProductAttributeViewModel)
        {
            var productAttribute = await _productAttributeRepository.GetByIdAsync(editProductAttributeViewModel.Id);
            if (productAttribute == null)
                return null;
            var productSize = await _productSizeRepository.GetAllAsync();
            var productColor = await _productColorRepository.GetAllAsync();
            var model = new EditProductAttributeModel
            {
                ProductSizes = _mapper.Map<List<SelectOptionViewModel>>(productSize),
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColor),
                EditProductAttributeViewModel = editProductAttributeViewModel
            };
            return model;
        }

        public async Task<List<ProductAttributeAdminViewModel>> GetListProductAttributeAdminViewModel(Guid ProductId)
        {
            var attribute = await _productAttributeRepository.GetListProductAttributeAdminViewModel(ProductId);
            return attribute;
        }

        public async Task<ProductAddCartErrorViewModel> GetProductAddCartError(Guid ProductId, Guid? ProductSizeId, Guid? ProductColorId)
        {
            var product = await _productAttributeRepository.GetProductAddCartErrorViewModel(ProductId, ProductSizeId, ProductColorId);
            return product;
        }

        public async Task<ProductAttributeViewModel> GetProductAttributeByProductColor(Guid ProductId, Guid ProductColorId)
        {
            var productAttribute = await _productAttributeRepository.GetProductAttributeByProductColor(ProductId, ProductColorId);
            return productAttribute;
        }


        public async Task<ProductAttributeViewModel> GetProductAttributeByProductSize(Guid ProductId, Guid? ProductColorId, Guid ProductSizeId)
        {
            var productAttribute = await _productAttributeRepository.GetProductAttributeByProductSize(ProductId, ProductColorId, ProductSizeId);
            return productAttribute;
        }

        public async Task<ProductHomepageAttributeViewModel> GetProductHomepageByProductAttribute(Guid ProductAttributeId)
        {
            var productHomepage = await _productAttributeRepository.GetProductHomepageByProductAttribute(ProductAttributeId);
            return productHomepage;
        }

    }
}
