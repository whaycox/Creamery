using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;
using Curds.Application;
using Curds.Application.Message.Dispatch;

namespace Curds.Domain.Application.Message.Dispatch
{
    public class MockSimpleDispatch : SimpleDispatch<MockApplication>
    {
        public MockSimpleDispatch(MockApplication application)
            : base(application)
        { }

        protected override Dictionary<Type, BaseMessageDefinition<MockApplication>> BuildMapping(Dictionary<Type, BaseMessageDefinition<MockApplication>> requestMap)
        {
            requestMap.Add(typeof(Command.MockVoidCommandDefinition), new Command.MockVoidCommandDefinition(Application));
            requestMap.Add(typeof(Command.MockQueryingCommandDefinition), new Command.MockQueryingCommandDefinition(Application));
            requestMap.Add(typeof(Query.MockQueryDefinition), new Query.MockQueryDefinition(Application));

            return requestMap;
        }
    }
}
