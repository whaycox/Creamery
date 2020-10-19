namespace Parmesan.Server.Abstraction
{
    using Controllers.Domain;
    using Parmesan.Domain;

    public interface IAuthorizationRequestParser
    {
        AuthorizationRequest Parse(WebAuthorizationRequest webAuthorizationRequest);
    }
}
