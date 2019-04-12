using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Query;
using Curds.Application.Message.Command;
using System.Threading.Tasks;

namespace Curds.Domain.Application.Message.Command
{
    public class MockQueryingCommandHandler : QueryingCommandHandler<MockApplication, MockCommand, MockViewModel>
    {
        private List<MockCommand> CommandsExecuted { get; }

        public MockQueryingCommandHandler(MockApplication application, List<MockCommand> commandsExecuted)
            : base(application)
        {
            CommandsExecuted = commandsExecuted;
        }

        public override Task<MockViewModel> Execute(MockCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
