using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.ManufacturerModel;
using Ecommerce.Service.ViewModels.Web.Homepage;
using EcommerceCommon.Infrastructure.Ultil;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ManufacturerService : EcommerceServices<Manufacturer>, IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;

        public ManufacturerService(IManufacturerRepository manufacturerRepository, IMapper mapper) 
            : base(manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddManufacturerAsync(AddManufacturerViewModel addManufacturerViewModel, string wwwRootPath)
        {
            try
            {
                if (addManufacturerViewModel.ImageFile != null)
                {
                    addManufacturerViewModel.Logo = await Ultil.UploadFileAsync(addManufacturerViewModel.ImageFile, wwwRootPath, "images");
                }
                var manufacturer = _mapper.Map<Manufacturer>(addManufacturerViewModel);
                await _manufacturerRepository.AddAsync(manufacturer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<bool> DeleteManufacturerAsync(Guid Id, string wwwRootPath)
        {
            try
            {
                var manufacturer = await _manufacturerRepository.GetByIdAsync(Id);
                if (manufacturer == null)
                {
                    return false;
                }
                if (manufacturer.Logo != null)
                {
                    Ultil.DeleteFile(manufacturer.Logo, wwwRootPath, "images");
                }
                await _manufacturerRepository.DeleteAsync(manufacturer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditManufacturerAsync(EditManufacturerViewModel editManufacturerViewModel, string wwwRootPath)
        {
            try
            {
                var manufacturer = await _manufacturerRepository.GetByIdAsync(editManufacturerViewModel.Id);
                if (manufacturer == null)
                {
                    return false;
                }
                if (editManufacturerViewModel.ImageFile != null)
                {
                    if (manufacturer.Logo != null)
                    {
                        Ultil.DeleteFile(manufacturer.Logo, wwwRootPath, "images");
                    }
                    manufacturer.Logo = await Ultil.UploadFileAsync(editManufacturerViewModel.ImageFile, wwwRootPath, "images");
                }
                manufacturer.UpdatedDate = DateTime.Now;
                manufacturer.Name = editManufacturerViewModel.Name;
                manufacturer.Website = editManufacturerViewModel.Website;
                manufacturer.CodeName = editManufacturerViewModel.CodeName;
                manufacturer.Description = editManufacturerViewModel.Description;
                await _manufacturerRepository.UpdateAsync(manufacturer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<EditManufacturerViewModel> GetEditManufacturerViewModel(Guid Id)
        {
            var manufacturer = await _manufacturerRepository.GetByIdAsync(Id);
            if (manufacturer == null)
                return null;
            return _mapper.Map<EditManufacturerViewModel>(manufacturer);
        }

        public async Task<List<ManufacturerAdminViewModel>> GetManufacturerAdminViewModels()
        {
            var manufacturer = await _manufacturerRepository.GetAllAsync();
            return _mapper.Map<List<ManufacturerAdminViewModel>>(manufacturer);
        }

        public async Task<IList<ManufacturerHomepageViewModel>> GetManufacturerHomepage(/*IHttpContextAccessor _httpContextAccessor*/)
        {
            // buoc 1: lay ra duoc tap hop cac entity
            var manufacturers = await _manufacturerRepository.FindAllAsync(x => x.IsDeleted == false);
            // Buoc 2  : map entity  => view model
            return _mapper.Map<List<ManufacturerHomepageViewModel>>(manufacturers);
            //var manu = _mapper.Map<List<ManufacturerHomepageViewModel>>(manufacturers);

            //foreach (var item in manu)
            //{
            //    item.UrlImage = "https://localhost:44361/image/" + item.UrlImage;
            //}
            //return manu;
        }
    }
}
