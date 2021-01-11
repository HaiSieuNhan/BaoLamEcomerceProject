using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Domain.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.ViewModels;
using Ecommerce.Service.ViewModels.Admin.CategoryModel;
using Ecommerce.Service.ViewModels.Admin.SelectOption;
using Ecommerce.Service.ViewModels.Web.Header;
using Ecommerce.Service.ViewModels.Web.ProductList;
using EcommerceCommon.Infrastructure.Ultil;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;

namespace Ecommerce.Service.Services
{
    public class CategoryService : EcommerceServices<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;              
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddCategoryAsync(AddCategoryViewModel addCategoryViewModel,string wwwRootPath)
        {
            try
            {
                if(addCategoryViewModel.ImageFile != null)
                {
                    addCategoryViewModel.URLImage = await Ultil.UploadFileAsync(addCategoryViewModel.ImageFile, wwwRootPath, "images");
                }
                var category = _mapper.Map<Category>(addCategoryViewModel);
                await _categoryRepository.AddAsync(category);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        public async Task<bool> DeleteCategoryAsync(Guid Id, string wwwRootPath)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(Id);
                if (category == null)
                {
                    return false;
                }
                if (category.URLImage != null)
                {
                    Ultil.DeleteFile(category.URLImage, wwwRootPath, "images");
                }
                await _categoryRepository.DeleteAsync(category);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditCategoryAsync(EditCategoryViewModel editCategoryViewModel, string wwwRootPath)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(editCategoryViewModel.Id);
                if(category == null)
                {
                    return false;
                }
                if (editCategoryViewModel.ImageFile != null)
                {
                    if (category.URLImage != null)
                    {
                        Ultil.DeleteFile(category.URLImage, wwwRootPath, "images");
                    }
                    category.URLImage = await Ultil.UploadFileAsync(editCategoryViewModel.ImageFile, wwwRootPath, "images");
                }
                category.UpdatedDate = DateTime.Now;
                category.Name = editCategoryViewModel.Name;
                category.IsDeleted = editCategoryViewModel.IsDeleted;
                category.ParentId = editCategoryViewModel.ParentId;
                category.Description = editCategoryViewModel.Description;
                await _categoryRepository.UpdateAsync(category);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<AddCategoryModel> GetAddCategoryModel()
        {
            var categoryParents = await _categoryRepository.GetAllAsync();
            var model = new AddCategoryModel
            {
                CategoryParents = _mapper.Map<List<SelectOptionViewModel>>(categoryParents)
            };
            
            return model;
        }

        public async Task<AddCategoryModel> GetAddCategoryModel(AddCategoryViewModel addCategoryViewModel)
        {
            var categoryParents = await _categoryRepository.GetAllAsync();
            var model = new AddCategoryModel
            {
                CategoryParents = _mapper.Map<List<SelectOptionViewModel>>(categoryParents),
                AddCategoryViewModel = addCategoryViewModel
            };
            return model;
        }

        public async Task<List<CategoryAdminViewModel>> GetCategoryAdminViewModels()
        {
            var category = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryAdminViewModel>>(category);
        }
        public async Task<EditCategoryModel> GetEditCategoryModel(Guid Id)
        {
            var category = await _categoryRepository.GetByIdAsync(Id);
            if (category == null)
                return null;
            var categoryParents = await _categoryRepository.GetAllAsync();
            var model = new EditCategoryModel
            {
                CategoryParents = _mapper.Map<List<SelectOptionViewModel>>(categoryParents),
                EditCategoryViewModel = _mapper.Map<EditCategoryViewModel>(category)
            };
            return model;
        }

        public async Task<EditCategoryModel> GetEditCategoryModel(EditCategoryViewModel editCategoryViewModel)
        {
            var category = await _categoryRepository.GetByIdAsync(editCategoryViewModel.Id);
            if (category == null)
                return null;
            var categoryParents = await _categoryRepository.GetAllAsync();
            var model = new EditCategoryModel
            {
                CategoryParents = _mapper.Map<List<SelectOptionViewModel>>(categoryParents),
                EditCategoryViewModel = editCategoryViewModel
            };
            return model;
        }
        public async Task<IList<CategoryLevel0ViewModel>> GetCategoryLevel0()
        {
            var category = await _categoryRepository.GetCategoryLevel0ViewModel();
            
            return category;

        }


        public async Task<CategoryProductListViewModel> GetCategoryProductList(Guid CategoryID)
        {
            var category = await _categoryRepository.GetByIdAsync(CategoryID);
            return _mapper.Map<CategoryProductListViewModel>(category);
        }



        public async Task<List<CategoryProductListViewModel>> GetListCategoryProductList(Guid? ParentId)
        {
            var category = await _categoryRepository.GetListCategoryProductListViewModel(ParentId);
            return category;
        }
    }
}
