using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Domain;
using Ecommerce.EmailService;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Ecommerce.Core.ViewModels.MappingProfile));
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

          //  services.AddDbContext<ApplicationDbContext>();

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddDistributedMemoryCache();
            // tu dong migration
            //using (var context = new ApplicationDbContext())
            //{
            //    context.Database.Migrate();
            //}
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                
            });
            ConfigureCoreAndRepositoryService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private void ConfigureCoreAndRepositoryService(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IServices<>), typeof(EcommerceServices<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();

            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductImageService, ProductImageService>();

            services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
            services.AddScoped<IProductCommentService, ProductCommentService>();

            services.AddScoped<IProductCommentReplyRepository, ProductCommentReplyRepository>();
            services.AddScoped<IProductCommentReplyService, ProductCommentReplyService>();

            services.AddScoped<IProductCommentImageRepository, ProductCommentImageRepository>();
            services.AddScoped<IProductCommentImageService, ProductCommentImageService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            services.AddScoped<ICartDetailService, CartDetailService>();
            
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<ICollectionService, CollectionService>();

            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IProductColorService, ProductColorService>();

            services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
            services.AddScoped<IProductSizeService, ProductSizeService>();

            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICouponService, CouponService>();

            services.AddScoped<ICouponApplyRepository, CouponApplyRepository>();
            services.AddScoped<ICouponApplyService, CouponApplyService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
            services.AddScoped<IOrderHistoryService, OrderHistoryService>();

            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        }
    }
}
