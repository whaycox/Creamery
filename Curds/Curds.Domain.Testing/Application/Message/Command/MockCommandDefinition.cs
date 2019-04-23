using Curds.Application.Message.Command;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Domain.Application.Message.Command
{
    using Query;

    public class MockCommandDefinition : BaseCommandDefinition<MockApplication, MockCommand, MockQuery>
    {
        public MockCommandDefinition(MockApplication application)
            : base(application)
        { }

        public override Task<MockQuery> Execute(MockCommand message) => Task.Factory.StartNew(() => ExecuteAndReturn(message));
        private MockQuery ExecuteAndReturn(MockCommand command)
        {
            CommandsExecuted.Add(command);
            return new MockQuery(nameof(MockCommandDefinition));
        }
        public List<MockCommand> CommandsExecuted = new List<MockCommand>();
    }
}
