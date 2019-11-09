using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Gouda.Application
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddGoudaApplication(this IServiceCollection services) => services
            .AddMediatR(typeof(RegistrationExtensions));
    }
}
