﻿using Ecommerce.Domain.Models;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
        Task<List<CartDetailViewModel>> GetListCartDetailByUserId(Guid UserId);
    }
}
