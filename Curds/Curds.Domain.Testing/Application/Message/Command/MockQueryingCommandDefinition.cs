using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Command;

namespace Curds.Domain.Application.Message.Command
{
    using System.Threading.Tasks;
    using Curds.Application.Message;
    using Query;

    public class MockQueryingCommandDefinition : BaseCommandDefinition<MockApplication, MockQueryingCommandHandler, MockCommand, MockViewModel>
    {
        public MockQueryingCommandDefinition(MockApplication application)
            : base(application)
        { }
        
        public override MockViewModel ViewModel => new MockViewModel() { String = nameof(MockVoidCommandDefinition) };

        public List<MockCommand> CommandsExecuted = new List<MockCommand>();
        public override MockQueryingCommandHandler Handler() => new MockQueryingCommandHandler(Application, CommandsExecuted);
    }
}
