using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.ExecuteCheck.Implementation
{
    using Domain;
    using Communication.Abstraction;
    using Gouda.Domain;
    using Persistence.Abstraction;
    using Analysis.Abstraction;
    using Scheduling.Abstraction;

    public class ExecuteCheckHandler : AsyncRequestHandler<ExecuteCheckCommand>
    {
        private ICommunicator Communicator { get; }
        private IGoudaDatabase GoudaDatabase { get; }
        private IAnalyzer Analyzer { get; }
        private IScheduler Scheduler { get; }

        public ExecuteCheckHandler(
            ICommunicator communicator,
            IGoudaDatabase goudaDatabase,
            IAnalyzer analyzer,
            IScheduler scheduler)
        {
            Communicator = communicator;
            GoudaDatabase = goudaDatabase;
            Analyzer = analyzer;
            Scheduler = scheduler;
        }

        protected async override Task Handle(ExecuteCheckCommand request, CancellationToken cancellationToken)
        {
            List<DiagnosticData> result = await Communicator.SendCheck(request.Check);
            await GoudaDatabase.DiagnosticData.Insert(result);
            await Analyzer.AnalyzeResult(request.Check, result);
            Scheduler.RescheduleCheck(request.Check);
        }
    }
}
