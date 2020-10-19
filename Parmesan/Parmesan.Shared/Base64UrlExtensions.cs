using System;

namespace Parmesan
{
    public static class Base64UrlExtensions
    {
        public static string Base64UrlEncode(this byte[] bytes) =>
            Convert.ToBase64String(bytes)
            .Base64UrlEncode();
        public static string Base64UrlEncode(this string base64) //https://tools.ietf.org/html/rfc7636#appendix-A
        {
            base64 = base64.Split('=')[0];
            base64 = base64.Replace('+', '-');
            base64 = base64.Replace('/', '_');
            return base64;
        }
    }
}
