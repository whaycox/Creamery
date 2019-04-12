using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;
using Curds.Application;

namespace Curds.Domain.Application.Message
{
    public class MockDispatch : BaseDispatch<MockApplication>
    {
        public Command.MockVoidCommandDefinition MockVoidCommand { get; }
        public Command.MockQueryingCommandDefinition MockQueryingCommand { get; }
        public Query.MockQueryDefinition MockQuery { get; }

        public MockDispatch(MockApplication application)
            : base(application)
        {
            MockVoidCommand = new Command.MockVoidCommandDefinition(application);
            MockQueryingCommand = new Command.MockQueryingCommandDefinition(application);
            MockQuery = new Query.MockQueryDefinition(application);
        }
    }
}
