using Application.Common;
using AutoMapper;
using infrastructure;
using infrastructure.Data;
using infrastructure.Identity;
using infrastructure.Models;
using infrastructure.Repository;
using infrastructure.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppFramework.ServicesConfiguration
{
    public static class CustomConfiguration
    {
        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }


        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IShopOrderService, ShopOrderService>();

        }

        public static void AutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public static void AddCross(this IServiceCollection services, IConfiguration Configuration)
        {

            string setting = Configuration.GetSection("SiteSettings").GetSection("OriginWebsite").Value?? "http://localhost:4200";
            services.AddCors(options =>
            {
                options.AddPolicy(name: "originList",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                      //WithOrigins(setting)
                                            .AllowAnyHeader()
                                            .AllowCredentials()
                                            .AllowCredentials();
                                  });
            });
        }
    }
}
