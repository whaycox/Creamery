using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Gouda.Application.Queries.DisplaySatellite.Implementation
{
    using Application.Implementation;
    using Domain;

    public class DisplaySatelliteValidator : BaseRequestValidator<DisplaySatelliteQuery>
    {
        public DisplaySatelliteValidator()
        {
            RuleFor(query => query.SatelliteID)
                .GreaterThan(0);
        }
    }
}
