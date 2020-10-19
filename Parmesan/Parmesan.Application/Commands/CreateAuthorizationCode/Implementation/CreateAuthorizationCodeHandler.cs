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
            AuthorizationCode newCode = new AuthorizationCode
            {
                Code = Random.Generate(CodeLengthInBytes),
                ClientID = request.ClientID,
                UserID = request.UserID,
                CodeChallenge = request.CodeChallenge,
                Scope = request.Scope,
                Expiration = CurrentExpiration,
            };
            await Database.AuthorizationCodes.Insert(newCode);

            return newCode.Code;
        }
    }
}
