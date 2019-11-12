using FluentValidation;

namespace Gouda.Application.Commands.ExecuteCheck.Implementation
{
    using Application.Implementation;
    using Domain;

    public class ExecuteCheckValidator : BaseRequestValidator<ExecuteCheckCommand>
    {
        public ExecuteCheckValidator()
        {
            RuleFor(command => command.Check)
                .NotNull()
                .Must(check => check.Satellite != null);
        }
    }
}
