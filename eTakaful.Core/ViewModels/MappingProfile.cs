using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Service.Dto;
using Ecommerce.Service.ViewModels;
using Ecommerce.Service.ViewModels.Admin.CategoryModel;
using Ecommerce.Service.ViewModels.Admin.CollectionModel;
using Ecommerce.Service.ViewModels.Admin.CouponModel;
using Ecommerce.Service.ViewModels.Admin.ManufacturerModel;
using Ecommerce.Service.ViewModels.Admin.OrderModel;
using Ecommerce.Service.ViewModels.Admin.ProductAttributeModel;
using Ecommerce.Service.ViewModels.Admin.ProductColorModel;
using Ecommerce.Service.ViewModels.Admin.ProductImageModel;
using Ecommerce.Service.ViewModels.Admin.ProductModel;
using Ecommerce.Service.ViewModels.Admin.ProductSizeModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using Ecommerce.Service.ViewModels.Admin.SupplierModel;
using Ecommerce.Service.ViewModels.Web.Customer;
using Ecommerce.Service.ViewModels.Web.Header;
using Ecommerce.Service.ViewModels.Web.Homepage;
using Ecommerce.Service.ViewModels.Web.ProductDetail;
using Ecommerce.Service.ViewModels.Web.ProductList;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ecommerce.Core.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MappingEntityToViewModel();
            MappingDtoToEntity();
        }

        private void MappingEntityToViewModel()
        {
            // case get data
            CreateMap<Category, CategoryViewModel>();
            CreateMap<ProductAttribute, ProductAttributeViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<User, UserDto>();
            CreateMap<Manufacturer, ManufacturerHomepageViewModel>()
                .ForMember(dest => dest.UrlImage, src => src.MapFrom(arc => arc.Logo));
            CreateMap<Collection, CollectionHomepageViewModel>();
            CreateMap<Collection, CollectionAdminViewModel>();
            CreateMap<Collection, EditCollectionViewModel>();
            CreateMap<Coupon, EditCouponViewModel>()
                .ForMember(dest => dest.Amount, src => src.MapFrom(arc => Regex.Replace(arc.Amount.ToString().Substring(0, arc.Amount.ToString().Length - 3), @"\B(?=(\d{3})+(?!\d))", ".")))
                .ForMember(dest => dest.StartTime, src => src.MapFrom(arc => arc.StartTime == null ? "" : arc.StartTime.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.EndTime, src => src.MapFrom(arc => arc.EndTime == null ? "" : arc.EndTime.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.NumberApply, src => src.MapFrom(arc => arc.NumberApply.ToString()));

            CreateMap<Category, CategoryProductListViewModel>();
            CreateMap<Coupon, CouponAdminViewModel>();
            CreateMap<UserProfile, UserProfileDto>()
            .ForMember(dest => dest.Birthday, src => src.MapFrom(arc => arc.Birthday == null ? "" : arc.Birthday.Value.ToString("dd/MM/yyyy")));
            CreateMap<Category, SelectOptionViewModel>();
            CreateMap<Collection, SelectOptionViewModel>();
            CreateMap<Manufacturer, SelectOptionViewModel>();
            CreateMap<Supplier, SelectOptionViewModel>();
            CreateMap<ProductColor, SelectOptionViewModel>();
            CreateMap<ProductSize, SelectOptionViewModel>();
            CreateMap<Category, CategoryAdminViewModel>();
            CreateMap<Category, EditCategoryViewModel>();
            CreateMap<Product, EditProductViewModel>();
            CreateMap<Manufacturer, EditManufacturerViewModel>();
            CreateMap<Manufacturer, ManufacturerAdminViewModel>();
            CreateMap<Supplier, EditSupplierViewModel>();
            CreateMap<Supplier, SupplierAdminViewModel>();
            CreateMap<ProductColor, ProductColorAdminViewModel>();
            CreateMap<ProductColor, EditProductColorViewModel>();            
            CreateMap<ProductSize, ProductSizeAdminViewModel>();
            CreateMap<ProductSize, EditProductSizeViewModel>();
            CreateMap<ProductImage, EditProductImageViewModel>();
            CreateMap<ProductAttribute, ProductAttributeAdminViewModel>();
            CreateMap<ProductAttribute, EditProductAttributeViewModel>()
                .ForMember(dest => dest.Price, src => src.MapFrom(arc => Regex.Replace(arc.Price.ToString().Substring(0, arc.Price.ToString().Length - 3), @"\B(?=(\d{3})+(?!\d))", ".")))
                .ForMember(dest => dest.DiscountPrice, src => src.MapFrom(arc => Regex.Replace(arc.DiscountPrice.ToString().Substring(0, arc.DiscountPrice.ToString().Length - 3), @"\B(?=(\d{3})+(?!\d))", ".")))
                .ForMember(dest => dest.CountStock, src => src.MapFrom(arc => arc.CountStock.ToString()));
            CreateMap<UserProfile, CustomerProfileViewModel>()
            .ForMember(dest => dest.Birthday, src => src.MapFrom(arc => arc.Birthday == null ? "" : arc.Birthday.Value.ToString("dd/MM/yyyy")));
            CreateMap<Order, EditOrderViewModel>();
        }

        private void MappingDtoToEntity()
        {
            // case insert or update
            CreateMap<AddCategoryViewModel, Category>();
            CreateMap<AddManufacturerViewModel, Manufacturer>();
            CreateMap<AddSupplierViewModel, Supplier>();
            CreateMap<AddProductColorViewModel, ProductColor>();
            CreateMap<AddProductSizeViewModel, ProductSize>();
            CreateMap<AddProductViewModel, Product>();
            CreateMap<AddProductImageViewModel, ProductImage>();
            CreateMap<AddProductAttributeViewModel, ProductAttribute>()
                .ForMember(dest => dest.Price, src => src.MapFrom(arc => decimal.Parse(arc.Price.Replace(".", ""))))
                .ForMember(dest => dest.DiscountPrice, src => src.MapFrom(arc => decimal.Parse(arc.DiscountPrice.Replace(".", ""))))
                .ForMember(dest => dest.CountStock, src => src.MapFrom(arc => Int32.Parse(arc.CountStock)));

            CreateMap<AddCouponViewModel, Coupon>()
                .ForMember(dest => dest.StartTime, src => src.MapFrom(arc => DateTime.ParseExact(arc.StartTime, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.EndTime, src => src.MapFrom(arc => DateTime.ParseExact(arc.EndTime, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Amount, src => src.MapFrom(arc => decimal.Parse(arc.Amount.Replace(".", ""))))
                .ForMember(dest => dest.NumberApply, src => src.MapFrom(arc => Int32.Parse(arc.NumberApply)));
            CreateMap<UserProfileDto, UserProfile>();
            
            CreateMap<CategoryDto, Category>();
            CreateMap<UserDto, User>();
        }
    }
}