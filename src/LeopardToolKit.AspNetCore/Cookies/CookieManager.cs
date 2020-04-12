using Microsoft.AspNetCore.Http;

namespace LeopardToolKit.AspNetCore.Cookies
{
    public class CookieManager : ICookieManager
    {
        private HttpContext httpContext;

        public CookieManager(IHttpContextAccessor httpContextAccessor)
        {
            httpContextAccessor.ThrowIfNull(nameof(httpContextAccessor));
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-3.0#use-httpcontext-from-custom-components
            this.httpContext = httpContextAccessor.HttpContext;
        }
        public void DeleteCookie(string key)
        {
            this.httpContext.Response.Cookies.Delete(key);
        }

        public string GetCookieValue(string key)
        {
            this.httpContext.Request.Cookies.TryGetValue(key, out string value);
            return value;
        }

        public void SetCookie(string key, string value, CookieOptions cookieOptions)
        {
            this.httpContext.Response.Cookies.Append(key, value ?? string.Empty, cookieOptions);
        }
    }
}
