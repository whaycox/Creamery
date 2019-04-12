using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message;
using Curds.Application.Message.Command;

namespace Curds.Domain.Application.Message.Command
{
    public class MockVoidCommandDefinition : BaseCommandDefinition<MockApplication, MockVoidCommandHandler, MockCommand, MockViewModel>
    {
        public MockVoidCommandDefinition(MockApplication application)
            : base(application)
        { }

        public override MockViewModel ViewModel => new MockViewModel() { String = nameof(MockVoidCommandDefinition) };

        public List<MockCommand> CommandsExecuted = new List<MockCommand>();
        public override MockVoidCommandHandler Handler() => new MockVoidCommandHandler(Application, CommandsExecuted);
    }
}
