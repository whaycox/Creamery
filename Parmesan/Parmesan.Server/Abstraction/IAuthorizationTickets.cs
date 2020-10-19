namespace Parmesan.Server.Abstraction
{
    using Application.Domain;

    public interface IAuthorizationTickets
    {
        string Create(VerifiedAuthorizationRequest authorizationRequest);
        VerifiedAuthorizationRequest Consume(string ticketNumber);
    }
}
