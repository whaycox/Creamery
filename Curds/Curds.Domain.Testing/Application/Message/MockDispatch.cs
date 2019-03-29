using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;
using Curds.Application;

namespace Curds.Domain.Application.Message
{
    public class MockDispatch : BaseDispatch<MockApplication>
    {
        public Command.MockCommandDefinition MockCommand { get; }
        public Query.MockQueryDefinition MockQuery { get; }

        public MockDispatch(MockApplication application)
            : base(application)
        {
            MockCommand = new Command.MockCommandDefinition(application);
            MockQuery = new Query.MockQueryDefinition(application);
        }
    }
}
