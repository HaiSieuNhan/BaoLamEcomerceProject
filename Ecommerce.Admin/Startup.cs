using AutoMapper;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interface;
using Ecommerce.Service.Services;
using EcommerceCommon.Infrastructure.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Ecommerce.Admin
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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });
            var key = Token.Key;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
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
            app.UseStaticFiles();
            app.UseCorsMiddleware();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
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

            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();

            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductImageService, ProductImageService>();

            services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
            services.AddScoped<IProductSizeService, ProductSizeService>();

            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IProductColorService, ProductColorService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IManufacturerService, ManufacturerService>();


            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<ICollectionService, CollectionService>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICouponService, CouponService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
            services.AddScoped<IProductCommentService, ProductCommentService>();

            services.AddScoped<IProductCommentImageRepository, ProductCommentImageRepository>();
            services.AddScoped<IProductCommentImageService, ProductCommentImageService>();

            services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
            services.AddScoped<IOrderHistoryService, OrderHistoryService>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            services.AddScoped<ICartDetailService, CartDetailService>();

            services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
            services.AddScoped<IOrderHistoryService, OrderHistoryService>();

            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            services.AddScoped<ICouponApplyRepository, CouponApplyRepository>();
            services.AddScoped<ICouponApplyService, CouponApplyService>();



        }
    }
}
