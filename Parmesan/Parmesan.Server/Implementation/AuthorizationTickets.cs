using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace Parmesan.Server.Implementation
{
    using Abstraction;
    using Application.Domain;

    internal class AuthorizationTickets : IAuthorizationTickets
    {
        private const int TicketLengthInBytes = 48;

        private Dictionary<string, VerifiedAuthorizationRequest> TicketCollection { get; } = new Dictionary<string, VerifiedAuthorizationRequest>();

        public string Create(VerifiedAuthorizationRequest authorizationRequest)
        {
            if (authorizationRequest == null)
                throw new ArgumentNullException(nameof(authorizationRequest));

            string ticketNumber = CreateNewTicketNumber();
            TicketCollection.Add(ticketNumber, authorizationRequest);

            return ticketNumber;
        }
        private string CreateNewTicketNumber()
        {
            byte[] ticketBytes = new byte[TicketLengthInBytes];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(ticketBytes);
            return Convert.ToBase64String(ticketBytes);
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
