using MediatR;
using Microsoft.AspNetCore.Http;

namespace Parmesan.Server.Queries.CheckForSession.Domain
{
    public class CheckForSessionQuery : IRequest<string>
    {
        public IRequestCookieCollection Cookies { get; set; }
    }
}
