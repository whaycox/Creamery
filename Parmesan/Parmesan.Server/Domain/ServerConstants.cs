namespace Parmesan.Server.Domain
{
    public static class ServerConstants
    {
        public const string LoginAuthorizationPolicy = "Parmesan.Server.Authorization.LoggedIn";
        public const string LoginAuthenticationScheme = "Parmesan.Server.Authentication.Login";
        public const string CookiePath = "/Parmesan.Server";

        public const string LoginRoute = "/Parmesan.Server/auth/login";
        public const string AuthorizeRoute = "/Parmesan.Server/oauth/authorize";
    }
}
