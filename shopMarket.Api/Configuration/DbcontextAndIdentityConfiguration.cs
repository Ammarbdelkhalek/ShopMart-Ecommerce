using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopMarket.Core.Data;
using ShopMarket.Core.Entites;

namespace shopMarket.Api.Configuration
{
    public static class DbcontextAndIdentityConfiguration
    {
        public static IServiceCollection AddDbContextAndIdentityServices (this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser,IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 1;
                    options.User.RequireUniqueEmail = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.User.AllowedUserNameCharacters = null;
                }

                ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
