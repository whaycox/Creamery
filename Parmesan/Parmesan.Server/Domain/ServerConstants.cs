namespace Parmesan.Server.Domain
{
    public static class ServerConstants
    {
        public const string LoginAuthorizationPolicy = "Parmesan.Server.Authorization.LoggedIn";
        public const string LoginAuthenticationScheme = "Parmesan.Server.Authentication.Login";
        public const string CookiePath = "/parmesan.server";

        public const string LoginRoute = "/parmesan.server/auth/login";
        public const string AuthorizeRoute = "/parmesan.server/oauth/authorize";
        public const string TokenRoute = "/parmesan.server/oauth/token";
    }
}
