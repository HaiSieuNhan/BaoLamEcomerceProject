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
using EcommerceCommon.Infrastructure.ViewModel;

namespace Ecommerce.Service.Services
{
    public class ProductCommentImageService : EcommerceServices<ProductCommentImage>, IProductCommentImageService
    {
        private readonly IProductCommentImageRepository _productCommentImageRepository;
        private readonly IMapper _mapper;

        public ProductCommentImageService(IProductCommentImageRepository productCommentImageRepository, IMapper mapper) : base(productCommentImageRepository)
        {
            _productCommentImageRepository = productCommentImageRepository;
            _mapper = mapper;
        }



    }
}
