namespace Parmesan.Domain
{
    public class AccessTokenResponse
    {
        public const string AccessTokenName = "access_token";
        public string AccessToken { get; set; }

        public const string TokenTypeName = "token_type";
        public TokenType TokenType { get; set; }

        public const string ExpiresInName = "expires_in";
        public int ExpiresIn { get; set; }

        public const string RefreshTokenName = "refresh_token";
        public string RefreshToken { get; set; }
    }
}
