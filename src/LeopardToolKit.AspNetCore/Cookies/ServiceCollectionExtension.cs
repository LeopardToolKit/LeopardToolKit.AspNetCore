using LeopardToolKit.AspNetCore.Cookies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtension
    {
        public static IServiceCollection AddCookieManager(this IServiceCollection services)
        {
            services.AddScoped<ICookieManager, CookieManager>();
            return services;
        }
    }
}
