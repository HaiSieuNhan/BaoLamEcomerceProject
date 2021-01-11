using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels.Admin.CollectionModel;
using Ecommerce.Service.ViewModels.Web.Homepage;
using EcommerceCommon.Infrastructure.Ultil;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Service.Services
{
    public class CollectionService : EcommerceServices<Collection>, ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public CollectionService(ICollectionRepository collectionRepository, IMapper mapper) : base(collectionRepository)
        {
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }

        public async Task<IList<CollectionHomepageViewModel>> GetCollectionHomepage()
        {
            var listCollection = await _collectionRepository.FindAllAsync(x => x.IsDeleted == false);
            return _mapper.Map<IList<CollectionHomepageViewModel>>(listCollection);
        }
        public async Task<IList<CollectionViewModel>> GetListCollectionHomepage()
        {
            var listCollection = await _collectionRepository.GetListCollectionViewModel();
            return listCollection;
        }

        // CRUD ADMIN 
        public async Task<List<CollectionAdminViewModel>> GetCollectionAdminViewModels()
        {
            var collection = await _collectionRepository.GetAllAsync();
            return _mapper.Map<List<CollectionAdminViewModel>>(collection);
        }

        public async Task<bool> AddCollectionAsync(Collection collection, string wwwRootPath)
        {
            try
            {
                if (collection.ImageFile != null)
                {
                    collection.URLImage = await Ultil.UploadFileAsync(collection.ImageFile, wwwRootPath, "images");
                }
               // var collection = _mapper.Map<Collection>(addCollectionViewModel);
                await _collectionRepository.AddAsync(collection);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public async Task<EditCollectionViewModel> GetEditCollectionViewModel(Guid Id)
        {
            var collection = await _collectionRepository.GetByIdAsync(Id);
            if (collection == null)
                return null;
            
            return _mapper.Map<EditCollectionViewModel>(collection);
        }

        public async Task<bool> EditCollectionAsync(EditCollectionViewModel editCollectionViewModel, string wwwRootPath)
        {
            try
            {
                var collection = await _collectionRepository.GetByIdAsync(editCollectionViewModel.Id);
                if (collection == null)
                {
                    return false;
                }
                if (editCollectionViewModel.ImageFile != null)
                {
                    if (collection.URLImage != null)
                    {
                        Ultil.DeleteFile(collection.URLImage, wwwRootPath, "images");
                    }
                    collection.URLImage = await Ultil.UploadFileAsync(editCollectionViewModel.ImageFile, wwwRootPath, "images");
                }
                collection.UpdatedDate = DateTime.Now;
                collection.Name = editCollectionViewModel.Name;
                collection.Description = editCollectionViewModel.Description;
                await _collectionRepository.UpdateAsync(collection);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCollectionAsync(Guid Id, string wwwRootPath)
        {
            try
            {
                var collection = await _collectionRepository.GetByIdAsync(Id);
                if (collection == null)
                {
                    return false;
                }
                if (collection.URLImage != null)
                {
                    Ultil.DeleteFile(collection.URLImage, wwwRootPath, "images");
                }
                await _collectionRepository.DeleteAsync(collection);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        
    }
}
