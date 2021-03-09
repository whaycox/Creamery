using Curds.Time.Abstraction;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Application.Commands.CreateAuthorizationCode.Implementation
{
    using Domain;
    using Parmesan.Abstraction;
    using Parmesan.Domain;
    using Persistence.Abstraction;

    internal class CreateAuthorizationCodeHandler : IRequestHandler<CreateAuthorizationCodeCommand, string>
    {
        private const int CodeLengthInBytes = 48;

        private static TimeSpan ExpirationDuration { get; } = TimeSpan.FromMinutes(5);

        private IParmesanDatabase Database { get; }
        private ISecureRandom Random { get; }
        private ITime Time { get; }

        public CreateAuthorizationCodeHandler(
            IParmesanDatabase database,
            ISecureRandom random,
            ITime time)
        {
            Database = database;
            Random = random;
            Time = time;
        }

        private DateTimeOffset CurrentExpiration => Time.Current.Add(ExpirationDuration);

        public async Task<string> Handle(CreateAuthorizationCodeCommand request, CancellationToken cancellationToken)
        {
            AuthorizationGrant authGrant = new AuthorizationGrant
            {
                ClientID = request.ClientID,
                UserID = request.UserID,
                Scope = request.Scope,
                RedirectUri = request.RedirectUri,
                CodeChallenge = request.CodeChallenge,
                Expiration = CurrentExpiration,
            };
            await Database.AuthorizationGrants.Insert(authGrant);

            AuthorizationCode authCode = new AuthorizationCode
            {
                Code = Random.Generate(CodeLengthInBytes),
                AuthorizationGrantID = authGrant.ID,
            };
            await Database.AuthorizationCodes.Insert(authCode);

            return authCode.Code;
        }
    }
}
