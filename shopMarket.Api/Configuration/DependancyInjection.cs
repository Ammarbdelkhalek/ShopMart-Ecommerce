using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using ShopMarket.Infrastrcuture.interfaces;
using ShopMarket.Infrastrcuture.Repository;
using ShopMarket.Services.DTOS.CategoryDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Mapping;
using ShopMarket.Services.Services.AuthServices;
using ShopMarket.Services.Services.AuthurizationServices;
using ShopMarket.Services.Services.BrandServices;
using ShopMarket.Services.Services.CategoryServices;
using ShopMarket.Services.Services.EmailServices;
using ShopMarket.Services.Services.ProductServices;

namespace shopMarket.Api.Configuration
{
    public  static class DependancyInjection
    {
        public static IServiceCollection AddDependancyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailHelper>(configuration.GetSection("EmailSetting"));
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            /*services.AddScoped<IProductService, ProductServices>(); 
            services.AddScoped<IBrandServices, BrandServices>(); 
            services.AddScoped<ICategoryServices, CategoryServices>();  */
            services.AddScoped<IUOW, UOW>();

            #region  mapping
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddAutoMapper(typeof(BrandProfile));
            services.AddAutoMapper(typeof(CategoryProfile));
            #endregion 
            return services;
        }

    }
}
