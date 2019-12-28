using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Gouda.Application
{
    using ViewModels.Satellite.Abstraction;
    using ViewModels.Satellite.Implementation;
    using ViewModels.Navigation.Implementation;
    using ViewModels.Navigation.Abstraction;
    using DeferredValues.Abstraction;
    using DeferredValues.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaApplication(this IServiceCollection services) => services
            .AddMediatR(typeof(RegistrationExtensions))
            .AddSingleton<ILabelDeferredValue, LabelDeferredValue>()
            .AddScoped<INavigationTreeBuilder, NavigationTreeBuilder>()
            .AddViewModelMappers();

        private static IServiceCollection AddViewModelMappers(this IServiceCollection services) => services
            .AddTransient<ISatelliteMapper, SatelliteMapper>()
            .AddTransient<ISatelliteSummaryMapper, SatelliteSummaryMapper>()
            .AddTransient<ICheckDefinitionMapper, CheckDefinitionMapper>();
    }
}
