using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Gouda.Application.Commands.AddCheck.Implementation
{
    using Application.Implementation;
    using Domain;

    public class AddCheckValidator : BaseRequestValidator<AddCheckCommand>
    {
        public AddCheckValidator()
        {
            RuleFor(command => command.SatelliteID)
                .GreaterThan(0);

            RuleFor(command => command.Name)
                .NotEmpty();

            RuleFor(command => command.CheckID)
                .NotEmpty();
        }
    }
}
