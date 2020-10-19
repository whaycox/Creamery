namespace Parmesan.Application.Abstraction
{
    using Domain;

    public interface IAuthorizationTicketRepository
    {
        string Create(VerifiedAuthorizationRequest authorizationRequest);
        VerifiedAuthorizationRequest Consume(string ticketNumber);
    }
}
