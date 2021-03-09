namespace Parmesan.Server.Abstraction
{
    using Domain;
    using Parmesan.Domain;

    public interface IOAuthRequestFactory
    {
        AuthorizationRequest Authorization(WebAuthorizationRequest request);
        AccessTokenRequest AccessToken(WebAccessTokenRequest request);
    }
}
