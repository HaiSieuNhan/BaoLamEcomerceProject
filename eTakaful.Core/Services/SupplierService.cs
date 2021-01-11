using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.SupplierModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class SupplierService : EcommerceServices<Supplier>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
            : base(supplierRepository)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddSupplierAsync(AddSupplierViewModel addSupplierViewModel)
        {
            try
            {
                var supplier = _mapper.Map<Supplier>(addSupplierViewModel);
                await _supplierRepository.AddAsync(supplier);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSupplierAsync(Guid Id)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(Id);
                if (supplier == null)
                {
                    return false;
                }
                await _supplierRepository.DeleteAsync(supplier);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditSupplierAsync(EditSupplierViewModel editSupplierViewModel)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(editSupplierViewModel.Id);
                if (supplier == null)
                {
                    return false;
                }
                supplier.UpdatedDate = DateTime.Now;
                supplier.Name = editSupplierViewModel.Name;
                supplier.Fax = editSupplierViewModel.Fax;
                supplier.CodeName = editSupplierViewModel.CodeName;
                supplier.Email = editSupplierViewModel.Email;
                supplier.Phone = editSupplierViewModel.Phone;
                await _supplierRepository.UpdateAsync(supplier);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<EditSupplierViewModel> GetEditSupplierViewModel(Guid Id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(Id);
            if (supplier == null)
                return null;
            return _mapper.Map<EditSupplierViewModel>(supplier);
        }

        public async Task<List<SupplierAdminViewModel>> GetSupplierAdminViewModels()
        {
            var supplier = await _supplierRepository.GetAllAsync();
            return _mapper.Map<List<SupplierAdminViewModel>>(supplier);
        }
    }
}
