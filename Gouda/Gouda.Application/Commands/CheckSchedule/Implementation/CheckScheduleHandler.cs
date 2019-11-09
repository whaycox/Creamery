using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Gouda.Application.Commands.CheckSchedule.Implementation
{
    using Domain;
    using Time.Abstraction;
    using Scheduling.Abstraction;
    using Gouda.Domain;
    using ExecuteCheck.Domain;

    public class CheckScheduleHandler : AsyncRequestHandler<CheckScheduleCommand>
    {
        private ITime Time { get; }
        private IScheduler Scheduler { get; }
        private IMediator Mediator { get; }

        public CheckScheduleHandler(
            ITime time, 
            IScheduler scheduler,
            IMediator mediator)
        {
            Time = time;
            Scheduler = scheduler;
            Mediator = mediator;
        }

        protected async override Task Handle(CheckScheduleCommand request, CancellationToken cancellationToken)
        {
            List<Check> scheduledChecks = await Scheduler.ChecksBeforeScheduledTime(Time.Current);

            IEnumerable<Task> checkTasks = scheduledChecks
                .Select(check => ExecuteScheduledCheck(check, cancellationToken));

            await Task.WhenAll(checkTasks);
        }
        private Task ExecuteScheduledCheck(Check scheduledCheck, CancellationToken cancellationToken)
        {
            ExecuteCheckCommand command = new ExecuteCheckCommand { Check = scheduledCheck };
            return Mediator.Send(command, cancellationToken);
        }
    }
}
