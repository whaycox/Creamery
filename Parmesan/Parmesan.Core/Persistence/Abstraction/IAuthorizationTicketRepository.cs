namespace Parmesan.Persistence.Abstraction
{
    using Domain;

    public interface IAuthorizationTicketRepository
    {
        string Create(AuthorizationRequest authorizationRequest);
        AuthorizationRequest Consume(string ticketNumber);
    }
}
