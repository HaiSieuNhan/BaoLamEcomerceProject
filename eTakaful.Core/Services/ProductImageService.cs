using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ProductImageModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using EcommerceCommon.Infrastructure.Ultil;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ProductImageService : EcommerceServices<ProductImage>, IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly IMapper _mapper;


        public ProductImageService(IProductImageRepository productImageRepository, IMapper mapper, IProductColorRepository productColorRepository) : base(productImageRepository)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
            _productColorRepository = productColorRepository;
        }

        public async Task AddProductImage(AddProductImageViewModel addImageModel, string wwwRootPath)
        {
            if(addImageModel.ImageFiles != null)
            {
                foreach(var item in addImageModel.ImageFiles)
                {
                    var image = new ProductImage();
                    image.ProductId = addImageModel.ProductId;
                    image.ProductColorId = addImageModel.ProductColorId;
                    image.ImageLink = await Ultil.UploadFileAsync(item, wwwRootPath, "images");
                    if (item.FileName == addImageModel.isMainImage)
                    {
                        image.IsMainImage = true;
                    }
                    await _productImageRepository.AddAsync(image);
                }
            }
        }
        public async Task<bool> AddProductImageAsync(AddProductImageViewModel addProductImageViewModel, string wwwRootPath)
        {
            try
            {
                foreach (var item in addProductImageViewModel.ImageFiles)
                {
                    var image = new ProductImage();
                    image.ProductId = addProductImageViewModel.ProductId;
                    image.ProductColorId = addProductImageViewModel.ProductColorId;
                    image.ImageLink = await Ultil.UploadFileAsync(item, wwwRootPath, "images");
                    if (item.FileName == addProductImageViewModel.isMainImage)
                    {
                        image.IsMainImage = true;
                    }
                    await _productImageRepository.AddAsync(image);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        public async Task<bool> DeleteProductImageAsync(Guid Id, string wwwRootPath)
        {
            try
            {
                var productImage = await _productImageRepository.GetByIdAsync(Id);
                if (productImage == null)
                {
                    return false;
                }
                if (productImage.ImageLink != null)
                {
                    Ultil.DeleteFile(productImage.ImageLink, wwwRootPath, "images");
                }
                await _productImageRepository.DeleteAsync(productImage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditProductImageAsync(EditProductImageViewModel editProductImageViewModel)
        {
            try
            {
                var productImage = await _productImageRepository.GetByIdAsync(editProductImageViewModel.Id);
                if (productImage == null)
                {
                    return false;
                }
                productImage.UpdatedDate = DateTime.Now;
                productImage.ImageLink = editProductImageViewModel.ImageLink;
                productImage.ProductColorId = editProductImageViewModel.ProductColorId;
                productImage.IsMainImage = editProductImageViewModel.IsMainImage;
                await _productImageRepository.UpdateAsync(productImage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<AddProductImageModel> GetAddProductImageModel()
        {
            var productColors = await _productColorRepository.GetAllAsync();
            var model = new AddProductImageModel
            {
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColors)
            };

            return model;
        }

        public async Task<AddProductImageModel> GetAddProductImageModel(AddProductImageViewModel addProductImageViewModel)
        {
            var productColors = await _productColorRepository.GetAllAsync();
            var model = new AddProductImageModel
            {
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColors),
                AddProductImageViewModel = addProductImageViewModel
            };
            return model;
        }

        public async Task<EditProductImageModel> GetEditProductImageModel(Guid Id)
        {
            var productImage = await _productImageRepository.GetByIdAsync(Id);
            if (productImage == null)
                return null;
            var productColors = await _productColorRepository.GetAllAsync();
            var model = new EditProductImageModel
            {
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColors),
                EditProductImageViewModel = _mapper.Map<EditProductImageViewModel>(productImage)
            };
            return model;
        }

        public async Task<EditProductImageModel> GetEditProductImageModel(EditProductImageViewModel editProductImageViewModel)
        {
            var productImage = await _productImageRepository.GetByIdAsync(editProductImageViewModel.Id);
            if (productImage == null)
                return null;
            var productColors = await _productColorRepository.GetAllAsync();
            var model = new EditProductImageModel
            {
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColors),
                EditProductImageViewModel = editProductImageViewModel
            };
            return model;
        }

        public async Task<List<ProductImageAdminViewModel>> GetProductImageAdminViewModels(Guid ProductId)
        {
            var listImage = await _productImageRepository.GetListProductImageAdminViewModel(ProductId);
            return listImage;
        }

        public async Task<IList<ProductImageViewModel>> GetProductImageByProductColor(Guid ProductId, Guid ProductColorId)
        {
            var productImage = await _productImageRepository.FindAllAsync(x => x.IsDeleted == false && x.ProductId == ProductId && x.ProductColorId==ProductColorId);
            return _mapper.Map<List<ProductImageViewModel>>(productImage);
        }

        public async Task<IList<ProductImageViewModel>> GetProductImageByProductId(Guid ProductId)
        {
            var productImage = await _productImageRepository.FindAllAsync(x => x.IsDeleted == false && x.ProductId == ProductId);
            return _mapper.Map<List<ProductImageViewModel>>(productImage);
        }

        public async Task<IList<ProductImageFirstAttributeViewModel>> GetProductImageFirstAttribute(Guid ProductAttributeId)
        {
            var productImage = await _productImageRepository.GetProductImageFirstAttribute(ProductAttributeId);
            return productImage;
        }

    }
}
