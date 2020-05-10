using Microsoft.AspNetCore.Http;
using System;

namespace LeopardToolKit.AspNetCore.Cookies
{
    public interface ICookieManager
    {
        void SetCookie(string key, string value, CookieOptions cookieOptions);

        string GetCookieValue(string key);

        void DeleteCookie(string key);
    }

    public static class CookieManagerExtension
    {
        public static void SetCookie(this ICookieManager cookieManager, string key, string value, DateTimeOffset? expires)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = expires;
            cookieManager.SetCookie(key, value, cookieOptions);
        }

        public static void SetCookie(this ICookieManager cookieManager, string key, string value, TimeSpan? maxAge)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.MaxAge = maxAge;
            cookieManager.SetCookie(key, value, cookieOptions);
        }

        public static void SetCookie(this ICookieManager cookieManager, string key, string value)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieManager.SetCookie(key, value, cookieOptions);
        }

    }
}
