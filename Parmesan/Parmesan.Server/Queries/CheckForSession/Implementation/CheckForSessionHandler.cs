using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Server.Queries.CheckForSession.Implementation
{
    using Domain;
    using Server.Domain;

    internal class CheckForSessionHandler : IRequestHandler<CheckForSessionQuery, string>
    {
        public Task<string> Handle(CheckForSessionQuery request, CancellationToken cancellationToken) => Task.FromResult(HandleInternal(request));
        private string HandleInternal(CheckForSessionQuery query) =>
            query.Cookies.TryGetValue(SessionConstants.CookieName, out string sessionID) ?
                sessionID :
                null;
    }
}
