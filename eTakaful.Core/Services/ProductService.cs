using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System.Linq;
using EcommerceCommon.Infrastructure.Ultil;
using Ecommerce.Service.ViewModels.Admin.ProductModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using Ecommerce.Service.ViewModels.Admin.ProductImageModel;
using Ecommerce.Service.ViewModels.Web.ProductList;

namespace Ecommerce.Service.Services
{
    public class ProductService : EcommerceServices<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IProductAttributeRepository productAttributeRepository, IProductImageRepository productImageRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, IProductColorRepository productColorRepository, IProductSizeRepository productSizeRepository, ICollectionRepository collectionRepository, IManufacturerRepository manufacturerRepository) : base(productRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productAttributeRepository = productAttributeRepository;
            _productImageRepository = productImageRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _productColorRepository = productColorRepository;
            _productSizeRepository = productSizeRepository;
            _collectionRepository = collectionRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public Task<List<ProductHomepage>> GetHotTrendProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductHomepage>> GetProductTopView(string searchString)
        {
            var product = await _productRepository.GetProductTopView(searchString);
            return product;

        }

        public async Task<List<ProductHomepage>> GetProductByCategory(Guid CategoryId)
        {
            var product = await _productRepository.GetProductByCategory(CategoryId);
            return product;
        }

        public async Task<ProductFirstAttributeViewModel> GetProductFirstAttributeByProductAttribute(Guid ProductAttributeId)
        {
            var product = await _productRepository.GetProductFirstAttributeViewModel(ProductAttributeId);
            return product;
        }
        public async Task<List<ProductAdminViewModel>> GetProductListAdminViewModel()
        {
            var productlist = await _productRepository.GetProductListAdminViewModel();
            return productlist;
        }

        public async Task<List<ProductHomepage>> GetProductByCategoryPagination(Guid CategoryId, int skip, int take)
        {
            var product = await _productRepository.GetProductByCategory(CategoryId);
            return product.Skip(skip).Take(take).ToList();
        }

        public async Task<bool> CreateProduct(AddProductViewModel addProductViewModel, List<AddProductAttributeViewModel> listAttribute, List<AddProductImageViewModel> listImage, string wwwRootPath)
        {
            try
            {
                var product = _mapper.Map<Product>(addProductViewModel);
                await _productRepository.AddAsync(product);
                if (listAttribute != null)
                {
                    foreach (var item in listAttribute)
                    {
                        var attribute = _mapper.Map<ProductAttribute>(item);
                        attribute.ProductId = product.Id;
                        await _productAttributeRepository.AddAsync(attribute);
                    }
                }
                if (listImage != null)
                {
                    foreach (var item in listImage)
                    {
                        if (item.ImageFiles != null)
                        {
                            foreach (var subitem in item.ImageFiles)
                            {
                                var image = new ProductImage();
                                image.ProductColorId = item.ProductColorId;
                                image.ProductId = product.Id;
                                image.ImageLink = await Ultil.UploadFileAsync(subitem, wwwRootPath, "images");
                                if (subitem.FileName == item.isMainImage)
                                {
                                    image.IsMainImage = true;
                                }
                                await _productImageRepository.AddAsync(image);
                            }
                        }
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<AddProductModel> GetAddProductModel()
        {
            var category = await _categoryRepository.GetAllAsync();
            var collection = await _collectionRepository.GetAllAsync();
            var manufacturer = await _manufacturerRepository.GetAllAsync();
            var supplier = await _supplierRepository.GetAllAsync();
            var productSize = await _productSizeRepository.GetAllAsync();
            var productColor = await _productColorRepository.GetAllAsync();
            var model = new AddProductModel
            {
                Categories = _mapper.Map<List<SelectOptionViewModel>>(category),
                Collections = _mapper.Map<List<SelectOptionViewModel>>(collection),
                Manufacturers = _mapper.Map<List<SelectOptionViewModel>>(manufacturer),
                Suppliers = _mapper.Map<List<SelectOptionViewModel>>(supplier),
                ProductSizes = _mapper.Map<List<SelectOptionViewModel>>(productSize),
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColor),
            };
            return model;
        }

        public async Task<AddProductModel> GetAddProductModel(AddProductViewModel addProductViewModel)
        {
            var category = await _categoryRepository.GetAllAsync();
            var collection = await _collectionRepository.GetAllAsync();
            var manufacturer = await _manufacturerRepository.GetAllAsync();
            var supplier = await _supplierRepository.GetAllAsync();
            var productSize = await _productSizeRepository.GetAllAsync();
            var productColor = await _productColorRepository.GetAllAsync();
            var model = new AddProductModel
            {
                AddProductViewModel = addProductViewModel,
                Categories = _mapper.Map<List<SelectOptionViewModel>>(category),
                Collections = _mapper.Map<List<SelectOptionViewModel>>(collection),
                Manufacturers = _mapper.Map<List<SelectOptionViewModel>>(manufacturer),
                Suppliers = _mapper.Map<List<SelectOptionViewModel>>(supplier),
                ProductSizes = _mapper.Map<List<SelectOptionViewModel>>(productSize),
                ProductColors = _mapper.Map<List<SelectOptionViewModel>>(productColor),
            };
            return model;
        }

        public async Task<EditProductModel> GetEditProductModel(Guid Id)
        {
            var product = await _productRepository.GetByIdAsync(Id);
            var category = await _categoryRepository.GetAllAsync();
            var collection = await _collectionRepository.GetAllAsync();
            var manufacturer = await _manufacturerRepository.GetAllAsync();
            var supplier = await _supplierRepository.GetAllAsync();
            var model = new EditProductModel
            {
                EditProductViewModel = _mapper.Map<EditProductViewModel>(product),
                Categories = _mapper.Map<List<SelectOptionViewModel>>(category),
                Collections = _mapper.Map<List<SelectOptionViewModel>>(collection),
                Manufacturers = _mapper.Map<List<SelectOptionViewModel>>(manufacturer),
                Suppliers = _mapper.Map<List<SelectOptionViewModel>>(supplier),
            };
            return model;
        }

        public async Task<EditProductModel> GetEditProductModel(EditProductViewModel editProductViewModel)
        {
            var category = await _categoryRepository.GetAllAsync();
            var collection = await _collectionRepository.GetAllAsync();
            var manufacturer = await _manufacturerRepository.GetAllAsync();
            var supplier = await _supplierRepository.GetAllAsync();
            var model = new EditProductModel
            {
                EditProductViewModel = editProductViewModel,
                Categories = _mapper.Map<List<SelectOptionViewModel>>(category),
                Collections = _mapper.Map<List<SelectOptionViewModel>>(collection),
                Manufacturers = _mapper.Map<List<SelectOptionViewModel>>(manufacturer),
                Suppliers = _mapper.Map<List<SelectOptionViewModel>>(supplier),
            };
            return model;
        }

        public async Task<bool> EditProduct(EditProductViewModel editProductViewModel)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(editProductViewModel.Id);
                if (product == null)
                {
                    return false;
                }
                product.UpdatedDate = DateTime.Now;
                product.Name = editProductViewModel.Name;
                product.Description = editProductViewModel.Description;
                product.ShortDescription = editProductViewModel.ShortDescription;
                product.ProductStatus = editProductViewModel.ProductStatus;
                product.CategoryId = editProductViewModel.CategoryId;
                product.SupplierId = editProductViewModel.SupplierId;
                product.CollectionId = editProductViewModel.CollectionId;
                product.ManufacturerId = editProductViewModel.ManufacturerId;
                await _productRepository.UpdateAsync(product);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(Guid Id,string wwwRootPath)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(Id);
                var productImage = await _productImageRepository.FindAllAsync(x => x.ProductId == Id);
                if(productImage != null)
                {
                    foreach(var item in productImage)
                    {
                        Ultil.DeleteFile(item.ImageLink, wwwRootPath, "images");
                    }
                }
                await _productRepository.DeleteAsync(product);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public async Task<ProductDetailAdminViewModel> GetProductDetailAdminViewModel(Guid Id)
        {
            var product = await _productRepository.GetProductDetailAdminViewModel(Id);
            return product;
        }

        public async Task<int> GetTotalQuantityProductRemainAdmin()
        {
            var product = await _productRepository.GetProductRemainAdminViewModels();
            var quantity = product.Select(x => x.Quantity).Sum();
            return quantity;
        }

        public async Task<List<ProductOrderedAdminViewModel>> GetProductOrderedAdminViewModels()
        {
            var product = await _productRepository.GetProductOrderedAdminViewModels();
            return product;
        }

        public async Task<int> GetTotalQuantityProductOrderedAdmin()
        {
            var product = await _productRepository.GetProductOrderedAdminViewModels();
            var quantity = product.Select(x => x.Quantity).Sum();
            return quantity;
        }

        public async Task<List<ProductRemainAdminViewModel>> GetProductRemainAdminViewModels()
        {
            var product = await _productRepository.GetProductRemainAdminViewModels();
            return product;
        }

        public async Task<List<ProductHomepage>> GetAllProduct(string searchString)
        {
            var product = await _productRepository.GetAllProduct(searchString);
            return product;
        }
    }
}
