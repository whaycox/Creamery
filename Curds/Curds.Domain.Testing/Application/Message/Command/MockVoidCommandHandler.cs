using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message.Command;

namespace Curds.Domain.Application.Message.Command
{
    public class MockVoidCommandHandler : VoidCommandHandler<MockApplication, MockCommand>
    {
        private List<MockCommand> CommandsExecuted { get; }

        public MockVoidCommandHandler(MockApplication application, List<MockCommand> commandsExecuted)
            : base(application)
        {
            CommandsExecuted = commandsExecuted;
        }

        public override Task Execute(MockCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
