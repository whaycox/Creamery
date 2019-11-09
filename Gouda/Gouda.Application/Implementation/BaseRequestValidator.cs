using FluentValidation;
using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace Gouda.Application.Implementation
{
    public abstract class BaseRequestValidator<TRequest> : AbstractValidator<TRequest>, IRequestPreProcessor<TRequest>
    {
        public BaseRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken) =>
            this.ValidateAndThrowAsync(request, null, cancellationToken);
    }
}
