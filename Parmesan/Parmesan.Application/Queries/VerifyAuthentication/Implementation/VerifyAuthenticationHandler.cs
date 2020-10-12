using MediatR;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Application.Queries.VerifyAuthentication.Implementation
{
    using Domain;
    using Parmesan.Abstraction;
    using Parmesan.Domain;
    using Persistence.Abstraction;
    using Security.Abstraction;
    using Security.Domain;

    internal class VerifyAuthenticationHandler : IRequestHandler<VerifyAuthenticationQuery, ClaimsPrincipal>
    {
        private IParmesanDatabase Database { get; }
        private IAuthenticationVerifier Verifier { get; }
        private IClaimsPrincipalFactory PrincipalFactory { get; }

        private string AuthenticationType { get; set; }

        public VerifyAuthenticationHandler(
            IParmesanDatabase database, 
            IAuthenticationVerifier verifier, 
            IClaimsPrincipalFactory principalFactory)
        {
            Database = database;
            Verifier = verifier;
            PrincipalFactory = principalFactory;
        }

        public async Task<ClaimsPrincipal> Handle(VerifyAuthenticationQuery request, CancellationToken cancellationToken)
        {
            IAuthenticationData authentication = request.Authentication;
            User user = await Database.Users.FetchByUserName(authentication.UserName);
            bool isValid = await VerifyAuthentication(user.ID, authentication);
            if (!isValid)
                throw new AuthenticationFailedException();
            return PrincipalFactory.CreateLoginForUser(AuthenticationType, user);
        }
        private async Task<bool> VerifyAuthentication(int userID, IAuthenticationData authentication)
        {
            switch (authentication)
            {
                case PasswordAuthenticationData passwordData:
                    AuthenticationType = nameof(PasswordAuthentication);
                    PasswordAuthentication storedPassword = await Database.Passwords.Fetch(userID);
                    return Verifier.VerifyPassword(storedPassword, passwordData.Password);
                default:
                    throw new InvalidOperationException($"Usupported authentication data: {authentication.GetType().FullName}");
            }
        }
    }
}
