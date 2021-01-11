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
using EcommerceCommon.Infrastructure.Enums;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Services
{
    public class ProductCommentReplyService : EcommerceServices<ProductCommentReply>, IProductCommentReplyService
    {
        private readonly IProductCommentReplyRepository _productCommentReplyRepository;
        private readonly IMapper _mapper;

        public ProductCommentReplyService(IProductCommentReplyRepository productCommentReplyRepository, IMapper mapper) : base(productCommentReplyRepository)
        {
            _productCommentReplyRepository = productCommentReplyRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductCommentReplyViewModel>> GetMoreReplyComment(int SkipNum, Guid ProductCommentId)
        {
            var productCommentReply = await _productCommentReplyRepository.GetMoreReplyComment(SkipNum, ProductCommentId);
            return productCommentReply;
        }
    }
}
