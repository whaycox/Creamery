namespace Parmesan.Server.Abstraction
{
    using Domain;
    using Parmesan.Domain;

    public interface IAuthorizationRequestParser
    {
        AuthorizationRequest Parse(WebAuthorizationRequest webAuthorizationRequest);
    }
}
