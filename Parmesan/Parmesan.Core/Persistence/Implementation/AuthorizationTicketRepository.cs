using System;
using System.Collections.Generic;

namespace Parmesan.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Parmesan.Abstraction;

    internal class AuthorizationTicketRepository : IAuthorizationTicketRepository
    {
        private const int TicketLengthInBytes = 48;

        private ISecureRandom Random { get; }

        private Dictionary<string, AuthorizationRequest> TicketCollection { get; } = new Dictionary<string, AuthorizationRequest>();

        public AuthorizationTicketRepository(ISecureRandom random)
        {
            Random = random;
        }

        public string Create(AuthorizationRequest authorizationRequest)
        {
            if (authorizationRequest == null)
                throw new ArgumentNullException(nameof(authorizationRequest));

            string ticketNumber = Random.Generate(TicketLengthInBytes);
            TicketCollection.Add(ticketNumber, authorizationRequest);

            return ticketNumber;
        }

        public AuthorizationRequest Consume(string ticketNumber)
        {
            if (!TicketCollection.TryGetValue(ticketNumber, out AuthorizationRequest value))
                return null;
            TicketCollection.Remove(ticketNumber);
            return value;
        }
    }
}
