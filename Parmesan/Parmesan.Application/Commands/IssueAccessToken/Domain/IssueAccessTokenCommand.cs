using MediatR;

namespace Parmesan.Application.Commands.IssueAccessToken.Domain
{
    using Parmesan.Domain;

    public class IssueAccessTokenCommand : IRequest<AccessTokenResponse>
    {
        public AccessTokenRequest Request { get; set; }
    }
}
