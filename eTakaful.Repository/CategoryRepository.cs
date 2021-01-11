using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.ViewModels.Web.ProductList;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CategoryLevel0ViewModel>> GetCategoryLevel0ViewModel()
        {
            var listcategory = await (from c in DbContext.Categories
                                      where c.ParentId == null
                                      select new CategoryLevel0ViewModel
                                      {
                                          Id = c.Id,
                                          Name = c.Name,
                                          CategoryLevel1ViewModel = (from c1 in DbContext.Categories
                                                                     where c1.ParentId == c.Id
                                                                     select new CategoryLevel1ViewModel
                                                                     {
                                                                         Id = c1.Id,
                                                                         Name = c1.Name,
                                                                         ParentId = c1.ParentId
                                                                     }).ToList()
                                      }).ToListAsync();
            return listcategory;
        }

        public async Task<ICollection<Category>> GetCategoryParrent()
        {
            return await DbContext.Categories.Where(c => c.ParentId == null).ToListAsync();
        }

        public async Task<List<CategoryProductListViewModel>> GetListCategoryProductListViewModel(Guid? ParentId)
        {
            var listCategory = await (from c in DbContext.Categories
                                      where c.IsDeleted == false && c.ParentId == ParentId
                                      select new CategoryProductListViewModel
                                      {
                                          Id = c.Id,
                                          Name = c.Name,
                                          ParentId = c.ParentId,
                                          CategoryChildren = (from c1 in DbContext.Categories
                                                              where c1.IsDeleted == false && c1.ParentId == c.Id
                                                              select new CategoryProductListViewModel
                                                              {
                                                                  Id = c1.Id,
                                                                  Name = c1.Name,
                                                                  ParentId = c1.ParentId
                                                              }).ToList()
                                      }).ToListAsync();
            return listCategory;
        }
    }
}
