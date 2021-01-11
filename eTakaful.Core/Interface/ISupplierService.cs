using Ecommerce.Domain.Models;
using Ecommerce.Service.ViewModels.Admin.SupplierModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface ISupplierService : IServices<Supplier>
    {
        Task<List<SupplierAdminViewModel>> GetSupplierAdminViewModels();
        Task<bool> AddSupplierAsync(AddSupplierViewModel addSupplierViewModel);
        Task<EditSupplierViewModel> GetEditSupplierViewModel(Guid Id);
        Task<bool> EditSupplierAsync(EditSupplierViewModel editSupplierViewModel);
        Task<bool> DeleteSupplierAsync(Guid Id);
    }
}
