using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Gouda.Application
{
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaApplication(this IServiceCollection services) => services
            .AddMediatR(typeof(RegistrationExtensions))
            .AddTransient<ISatelliteSummaryMapper, SatelliteSummaryMapper>();
    }
}
