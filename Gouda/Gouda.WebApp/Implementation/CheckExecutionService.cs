using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gouda.WebApp.Implementation
{
    using Application.Commands.CheckSchedule.Domain;
    using Domain;

    public class CheckExecutionService : BackgroundService
    {
        private CheckExecutionServiceOptions Options { get; }
        private IMediator Mediator { get; }

        public CheckExecutionService(IOptions<CheckExecutionServiceOptions> options, IMediator mediator)
        {
            Options = options.Value;
            Mediator = mediator;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Mediator.Send(new CheckScheduleCommand(), stoppingToken);
                await Task.Delay(Options.SleepTimeInMs);
            }
        }
    }
}
