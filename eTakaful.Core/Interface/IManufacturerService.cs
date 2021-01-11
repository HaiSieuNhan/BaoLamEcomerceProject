using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.ManufacturerModel;
using Ecommerce.Service.ViewModels.Web.Homepage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IManufacturerService : IServices<Manufacturer>
    {
        Task<IList<ManufacturerHomepageViewModel>> GetManufacturerHomepage(/*IHttpContextAccessor _httpContextAccessor*/);
        Task<List<ManufacturerAdminViewModel>> GetManufacturerAdminViewModels();
        Task<bool> AddManufacturerAsync(AddManufacturerViewModel addManufacturerViewModel, string wwwRootPath);
        Task<EditManufacturerViewModel> GetEditManufacturerViewModel(Guid Id);
        Task<bool> EditManufacturerAsync(EditManufacturerViewModel editManufacturerViewModel, string wwwRootPath);
        Task<bool> DeleteManufacturerAsync(Guid Id, string wwwRootPath);
    }
}
