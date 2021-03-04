using LeopardToolKit.AspNetCore.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace LeopardToolKit.AspNetCore
{
    public static partial class CookieExtension
    {
        public static IServiceCollection AddCookieManager(this IServiceCollection services)
        {
            services.AddScoped<ICookieManager, CookieManager>();
            return services;
        }
    }
}
