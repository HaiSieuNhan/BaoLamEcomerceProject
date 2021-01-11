using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;
using EcommerceCommon.Infrastructure.Enums;

namespace Ecommerce.Repository
{
    public class ProductCommentImageRepository : BaseRepository<ProductCommentImage>, IProductCommentImageRepository
    {
        public ProductCommentImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
