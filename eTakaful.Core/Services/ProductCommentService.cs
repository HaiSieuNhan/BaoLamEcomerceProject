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
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Services
{
    public class ProductCommentService : EcommerceServices<ProductComment>, IProductCommentService
    {
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly IMapper _mapper;

        public ProductCommentService(IProductCommentRepository productCommentRepository, IMapper mapper) : base(productCommentRepository)
        {
            _productCommentRepository = productCommentRepository;
            _mapper = mapper;
        }



        public async Task<List<ProductCommentViewModel>> GetProductCommentByProductId(Guid ProductId)
        {
            var productcomment = await _productCommentRepository.GetProductCommentViewModel(ProductId);
            return productcomment;
        }

        public async Task<List<ProductCommentAdminViewModel>> GetProductCommentListViewModel()
        {
            var productcomment = await _productCommentRepository.GetProductCommentListViewModel();
            return productcomment;
        }

        public async Task<List<ProductCommentViewModel>> GetProductCommentPagination(Guid ProductId, int skip, int take)
        {
            var productcomment = await _productCommentRepository.GetProductCommentPagination(ProductId, skip, take);
            return productcomment;
        }

        public async Task<List<ProductRatingViewModel>> GetProductRatingViewModel(Guid ProductId)
        {
            List<ProductRatingViewModel> listProductRating = new List<ProductRatingViewModel>();
            for(int i = 5; i >= 1; i--)
            {
                var listRating = await _productCommentRepository.FindAllAsync(x => x.ProductId == ProductId &&
                x.Rating == i && x.ProducCommentStatus == ProductCommentStatus.Qualified);
                ProductRatingViewModel productRating = new ProductRatingViewModel
                {
                    Rating = i,
                    Number = listRating.Count
                };
                listProductRating.Add(productRating);
            }
            return listProductRating;
        }
    }
}
