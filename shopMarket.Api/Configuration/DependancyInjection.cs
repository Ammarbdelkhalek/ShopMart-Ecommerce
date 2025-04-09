using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using ShopMarket.Infrastrcuture.interfaces;
using ShopMarket.Infrastrcuture.Repository;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Services.AuthServices;
using ShopMarket.Services.Services.AuthurizationServices;
using ShopMarket.Services.Services.EmailServices;

namespace shopMarket.Api.Configuration
{
    public  static class DependancyInjection
    {
        public static IServiceCollection AddDependancyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailHelper>(configuration.GetSection("EmailSetting"));
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IAuthorizeServices, AuthorizationServices>(); 
            services.AddScoped<IUOW, UOW>();
            return services;
        }

    }
}
