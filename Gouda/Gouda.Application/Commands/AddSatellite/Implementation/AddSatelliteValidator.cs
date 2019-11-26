using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using System.Net;

namespace Gouda.Application.Commands.AddSatellite.Implementation
{
    using Application.Implementation;
    using Domain;

    public class AddSatelliteValidator : BaseRequestValidator<AddSatelliteCommand>
    {
        public AddSatelliteValidator()
        {
            RuleFor(command => command.SatelliteName)
                .NotEmpty();

            RuleFor(command => command.SatelliteIP)
                .Must(ip => IPAddress.TryParse(ip, out IPAddress parsed));
        }
    }
}
