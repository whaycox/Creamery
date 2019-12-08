using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Gouda.WebApp
{
    using Implementation;
    using DeferredValues.Implementation;
    using DeferredValues.Abstraction;
    using ViewModels.Abstraction;
    using ViewModels.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaWebApp(this IServiceCollection services) => services
            .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
            .AddSingleton<IWebAppViewModelWrapper, WebAppViewModelWrapper>()
            .AddSingleton<IHostedService, CheckExecutionService>()
            .AddSingleton<IDestinationDeferredValue, DestinationDeferredValue>();
    }
}
