﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Gouda.WebApp
{
    using Implementation;
    using Navigation.Implementation;
    using Navigation.Abstraction;
    using Adapters.Abstraction;
    using Adapters.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaWebApp(this IServiceCollection services) => services
            .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddSingleton<IHostedService, CheckExecutionService>()
            .AddScoped<INavigationTreeBuilder, NavigationTreeBuilder>()
            .AddSingleton<IDestinationCollection, DestinationCollection>()
            .AddScoped<IDestinationAdapter, DestinationAdapter>();
    }
}