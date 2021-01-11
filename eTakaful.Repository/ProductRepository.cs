using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.Helper;
using EcommerceCommon.Infrastructure.ViewModel;
using EcommerceCommon.Infrastructure.ViewModel.Admin;
using EcommerceCommon.Infrastructure.ViewModel.Web;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<ProductHomepage>> GetHotTrendProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductHomepage>> GetProductTopView(string searchString)
        {

            //var products = await (from p in DbContext.Products
            //                      where p.IsDeleted == false
            //                      orderby p.View descending
            //                      select new ProductHomepage
            //                      {
            //                          Name = p.Name,
            //                          ProductHomepageAttributeViewModel =
            //                          (from pa in DbContext.ProductAttributes
            //                           join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
            //                           from pc in pcl.DefaultIfEmpty()
            //                           join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
            //                           from ps in psl.DefaultIfEmpty()
            //                           join pi in DbContext.ProductImages on p.Id equals pi.ProductId
            //                           orderby pc.Sort ascending
            //                           where pa.ProductId == p.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
            //                           group new { pa, pc, ps, pi } by new { pa.ProductId, pa.ProductColorId } into pg
            //                           select new ProductHomepageAttributeViewModel
            //                           {
            //                               Id = pg.FirstOrDefault().pa.Id,
            //                               UrlImage = pg.FirstOrDefault().pi.ImageLink,
            //                               PriceSale = pg.FirstOrDefault().pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
            //                               Price = pg.FirstOrDefault().pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
            //                               PercentSale = Math.Round((1 - pg.FirstOrDefault().pa.DiscountPrice / pg.FirstOrDefault().pa.Price) * 100, 0)
            //                           }).ToList()
            //                      }).Take(5).ToListAsync();
            var products =  (from p in DbContext.Products
                                  where p.IsDeleted == false
                                  orderby p.View descending
                                  select new { p });
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.p.Name.Contains(searchString) || p.p.Keyword.Contains(searchString));
            }
            var data = await products.Select(p=> new ProductHomepage()
            {
                                       Name = p.p.Name,
                                      ProductHomepageAttributeViewModel =
                                      (from pa in DbContext.ProductAttributes
                                       join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                       from pc in pcl.DefaultIfEmpty()
                                       join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                       from ps in psl.DefaultIfEmpty()
                                       join pi in DbContext.ProductImages on p.p.Id equals pi.ProductId
                                       orderby pc.Sort ascending
                                       where pa.ProductId == p.p.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                       group new { pa, pc, ps, pi } by new { pa.ProductId, pa.ProductColorId } into pg
                                       select new ProductHomepageAttributeViewModel
                                       {
                                           Id = pg.FirstOrDefault().pa.Id,
                                           UrlImage = pg.FirstOrDefault().pi.ImageLink,
                                           PriceSale = pg.FirstOrDefault().pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                           Price = pg.FirstOrDefault().pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                           PercentSale = Math.Round((1 - pg.FirstOrDefault().pa.DiscountPrice / pg.FirstOrDefault().pa.Price) * 100, 0)
                                       }).ToList()
            }).Take(5).ToListAsync();
            return data;
        }


        public Task<List<ProductHomepage>> GetProductByCategory(Guid CategoryID)
        {
            var listCategory = (from c in DbContext.Categories
                                where c.IsDeleted == false
                                select new CategoryNodeTree
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Parent = (from c1 in DbContext.Categories
                                              where c1.IsDeleted == false && c1.Id == c.ParentId
                                              select new CategoryNodeTree
                                              {
                                                  Id = c1.Id
                                              }).FirstOrDefault()
                                }).ToList();
            IList<CategoryNodeTree> topLevelCategories = TreeHelper.ConvertToForest(listCategory);
            CategoryNodeTree currentNode = TreeHelper.FindTreeNode(topLevelCategories, CategoryID);
            var listLeafNode = TreeHelper.Iterators.DepthLeafTraversal(currentNode);
            List<Guid> listCategoryId = new List<Guid>();
            foreach (var item in listLeafNode)
            {
                listCategoryId.Add(item.Id);
            }
            if (listCategoryId.Count == 0)
            {
                listCategoryId.Add(CategoryID);
            }
            var product = (from p in DbContext.Products
                           join c in DbContext.Categories on p.CategoryId equals c.Id
                           where p.IsDeleted == false && listCategoryId.Contains(c.Id)
                           select new ProductHomepage
                           {
                               Name = p.Name,
                               ProductHomepageAttributeViewModel = (from pa in DbContext.ProductAttributes
                                                                    join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                                                    from pc in pcl.DefaultIfEmpty()
                                                                    join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                                                    from ps in psl.DefaultIfEmpty()
                                                                    join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                                                    orderby pc.Sort ascending
                                                                    where pa.ProductId == p.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                                                    group new { pa, pc, ps, pi } by new { pa.ProductId, pa.ProductColorId } into pg
                                                                    select new ProductHomepageAttributeViewModel
                                                                    {
                                                                        Id = pg.FirstOrDefault().pa.Id,
                                                                        UrlImage = pg.FirstOrDefault().pi.ImageLink,
                                                                        PriceSale = pg.FirstOrDefault().pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                        Price = pg.FirstOrDefault().pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                                                        PercentSale = Math.Round((1 - pg.FirstOrDefault().pa.DiscountPrice / pg.FirstOrDefault().pa.Price) * 100, 0)
                                                                    }).ToList()
                           }).ToListAsync();
            return product;
        }

        public async Task<List<ProductAdminViewModel>> GetProductListAdminViewModel()
        {
            var productlist = await (from p in DbContext.Products
                                     join c in DbContext.Categories on p.CategoryId equals c.Id
                                     join m in DbContext.Manufacturers on p.ManufacturerId equals m.Id
                                     join s in DbContext.Suppliers on p.SupplierId equals s.Id
                                     where p.IsDeleted == false
                                     select new ProductAdminViewModel
                                     {
                                         Id = p.Id,
                                         Sort = p.Sort,
                                         Name = p.Name,
                                         Description = p.Description,
                                         ShortDescription = p.ShortDescription,
                                         PublicationDate = p.PublicationDate,
                                         Keyword = p.Keyword,
                                         Sku = p.Sku,
                                         View = p.View,
                                         ManufacturerName = m.Name,
                                         SupplierName = s.Name,
                                         ProductStatus = p.ProductStatus
                                     }).ToListAsync();
            return productlist;
        }

        public async Task<ProductCartViewModel> GetProductCartViewModel(Guid ProductId, Guid? ProductSizeId, Guid? ProductColorId)
        {
            var productCart = await (from p in DbContext.Products
                                     join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                     join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                     from pc in pcl.DefaultIfEmpty()
                                     join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                     from ps in psl.DefaultIfEmpty()
                                     join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                     join s in DbContext.Suppliers on p.SupplierId equals s.Id
                                     where p.IsDeleted == false && pa.ProductSizeId == ProductSizeId && pa.ProductColorId == ProductColorId
                                     && pi.IsMainImage == true && pi.ProductColorId == ProductColorId
                                     select new ProductCartViewModel
                                     {
                                         Id = pa.Id,
                                         Name = p.Name,
                                         ImageLink = pi.ImageLink,
                                         Price = pa.Price,
                                         DiscountPrice = pa.DiscountPrice,
                                         Size = ps.Name,
                                         Color = pc.Name,
                                         CountStock = pa.CountStock,
                                         SupplierName = s.Name,
                                     }).FirstOrDefaultAsync();
            return productCart;
        }

        public async Task<ProductFirstAttributeViewModel> GetProductFirstAttributeViewModel(Guid ProductAttributeId)
        {
            var product = await (from p in DbContext.Products
                                 join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                 join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                 from ps in psl.DefaultIfEmpty()
                                 join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                 from pc in pcl.DefaultIfEmpty()
                                 where p.IsDeleted == false && pa.Id == ProductAttributeId
                                 select new ProductFirstAttributeViewModel
                                 {
                                     Id = pa.Id,
                                     Name = p.Name,
                                     Description = p.Description,
                                     ShortDescription = p.ShortDescription,
                                     Price = pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                     DiscountPrice = pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                     Size = ps.Name,
                                     Color = pc.Name,
                                     ProductSizeId = ps.Id,
                                     ProductColorId = pc.Id,
                                     ProductId = p.Id
                                 }).FirstOrDefaultAsync();
            return product;
        }

        public async Task<ProductDetailAdminViewModel> GetProductDetailAdminViewModel(Guid Id)
        {
            var product = await (from p in DbContext.Products
                                 join c in DbContext.Categories on p.CategoryId equals c.Id
                                 join co in DbContext.Collections on p.CollectionId equals co.Id into col
                                 from co in col.DefaultIfEmpty()
                                 join m in DbContext.Manufacturers on p.ManufacturerId equals m.Id
                                 join s in DbContext.Suppliers on p.SupplierId equals s.Id
                                 where p.Id == Id && p.IsDeleted == false
                                 select new ProductDetailAdminViewModel
                                 {
                                     Name = p.Name,
                                     CategoryName = c.Name,
                                     CollectionName = co.Name,
                                     ManufacturerName = m.Name,
                                     SupplierName = s.Name,
                                     Sku = p.Sku,
                                     PublicationDate = p.PublicationDate.ToString("dd-MM-yyyy"),
                                     Keyword = p.Keyword,
                                     View = p.View,
                                     ProductStatus = p.ProductStatus
                                 }).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<ProductOrderedAdminViewModel>> GetProductOrderedAdminViewModels()
        {
            var product = await (from or in DbContext.Orders
                                 join od in DbContext.OrderDetails on or.Id equals od.OrderId
                                 join pa in DbContext.ProductAttributes on od.ProductAttributeId equals pa.Id
                                 join p in DbContext.Products on pa.ProductId equals p.Id
                                 join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                 from pc1 in pcl.DefaultIfEmpty()
                                 join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                 from ps1 in psl.DefaultIfEmpty()
                                 join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                 where or.OrderStatus != OrderStatus.Cancle && pi.IsMainImage == true && pi.ProductColorId == pc1.Id
                                 group new { od.Quantity, p.Name, pi.ImageLink, Color = pc1.Name, Size = ps1.Name, od.Price } by od.ProductAttributeId into g
                                 orderby g.Select(x => x.Quantity).Sum() descending
                                 select new ProductOrderedAdminViewModel
                                 {
                                     ProductName = g.Select(x => x.Name).FirstOrDefault(),
                                     ProductColor = g.Select(x => x.Color).FirstOrDefault(),
                                     ProductSize = g.Select(x => x.Size).FirstOrDefault(),
                                     ProductImage = g.Select(x => x.ImageLink).FirstOrDefault(),
                                     Quantity = g.Select(x => x.Quantity).Sum(),
                                     TotalPrice = g.Select(x => x.Quantity * x.Price).Sum().ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is"))
                                 }).ToListAsync();
            return product;
        }

        public async Task<List<ProductRemainAdminViewModel>> GetProductRemainAdminViewModels()
        {
            var product = await (from p in DbContext.Products
                                 join pa in DbContext.ProductAttributes on p.Id equals pa.ProductId
                                 join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                 from ps in psl.DefaultIfEmpty()
                                 join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                 from pc in pcl.DefaultIfEmpty()
                                 join pi in DbContext.ProductImages on p.Id equals pi.ProductId
                                 where pa.IsDeleted == false && p.IsDeleted == false && pi.IsMainImage == true && pi.ProductColorId == pc.Id
                                 select new ProductRemainAdminViewModel
                                 {
                                     ProductName = p.Name,
                                     ProductImage = pi.ImageLink,
                                     ProductColor = pc.Name,
                                     ProductSize = ps.Name,
                                     Price = pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                     DiscountPrice = pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                     Quantity = pa.CountStock
                                 }).ToListAsync();
            return product;
        }

        public async Task<List<ProductHomepage>> GetAllProduct(string searchString)
        {
            var products = (from p in DbContext.Products
                            where p.IsDeleted == false
                            select new { p });
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.p.Name.Contains(searchString) || p.p.Keyword.Contains(searchString));
            }
            var data = await products.Select(p => new ProductHomepage()
            {
                Name = p.p.Name,
                ProductHomepageAttributeViewModel =
                                      (from pa in DbContext.ProductAttributes
                                       join pc in DbContext.ProductColors on pa.ProductColorId equals pc.Id into pcl
                                       from pc in pcl.DefaultIfEmpty()
                                       join ps in DbContext.ProductSizes on pa.ProductSizeId equals ps.Id into psl
                                       from ps in psl.DefaultIfEmpty()
                                       join pi in DbContext.ProductImages on p.p.Id equals pi.ProductId
                                       orderby pc.Sort ascending
                                       where pa.ProductId == p.p.Id && pi.IsMainImage == true && pi.ProductColorId == pa.ProductColorId
                                       group new { pa, pc, ps, pi } by new { pa.ProductId, pa.ProductColorId } into pg
                                       select new ProductHomepageAttributeViewModel
                                       {
                                           Id = pg.FirstOrDefault().pa.Id,
                                           UrlImage = pg.FirstOrDefault().pi.ImageLink,
                                           PriceSale = pg.FirstOrDefault().pa.DiscountPrice.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                           Price = pg.FirstOrDefault().pa.Price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("is")),
                                           PercentSale = Math.Round((1 - pg.FirstOrDefault().pa.DiscountPrice / pg.FirstOrDefault().pa.Price) * 100, 0)
                                       }).ToList()
            }).ToListAsync();
            return data;
        }
    }
}
