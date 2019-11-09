using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MediatR;

namespace Gouda.WebApp.Implementation
{
    using Application.Commands.CheckSchedule.Domain;

    public class CheckExecutionService : BackgroundService
    {
        private const int SleepTimeInMs = 500;

        private IMediator Mediator { get; }

        public CheckExecutionService(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Mediator.Send(new CheckScheduleCommand(), stoppingToken);
                await Task.Delay(SleepTimeInMs);
            }
        }
    }
}
