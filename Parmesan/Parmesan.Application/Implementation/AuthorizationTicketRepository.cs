using System;
using System.Collections.Generic;

namespace Parmesan.Application.Implementation
{
    using Abstraction;
    using Domain;
    using Parmesan.Abstraction;

    internal class AuthorizationTicketRepository : IAuthorizationTicketRepository
    {
        private const int TicketLengthInBytes = 48;

        private ISecureRandom Random { get; }

        private Dictionary<string, VerifiedAuthorizationRequest> TicketCollection { get; } = new Dictionary<string, VerifiedAuthorizationRequest>();

        public AuthorizationTicketRepository(ISecureRandom random)
        {
            Random = random;
        }

        public string Create(VerifiedAuthorizationRequest authorizationRequest)
        {
            if (authorizationRequest == null)
                throw new ArgumentNullException(nameof(authorizationRequest));

            string ticketNumber = Random.Generate(TicketLengthInBytes);
            TicketCollection.Add(ticketNumber, authorizationRequest);

            return ticketNumber;
        }

        public VerifiedAuthorizationRequest Consume(string ticketNumber)
        {
            if (!TicketCollection.TryGetValue(ticketNumber, out VerifiedAuthorizationRequest value))
                return null;
            TicketCollection.Remove(ticketNumber);
            return value;
        }
    }
}
