using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Parmesan.Application
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanApplication(this IServiceCollection services) => services
            .AddParmesanCore()
            .AddMediatR(typeof(RegistrationExtensions));
    }
}
