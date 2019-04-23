using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;
using Curds.Application.Message.Dispatch;

namespace Curds.Domain.Application.Message
{
    public class MockDispatch : BaseDispatch<MockApplication>
    {
        public MockDispatch(MockApplication application)
            : base(application)
        { }

        protected override Dictionary<Type, BaseMessageDefinition<MockApplication>> BuildMapping(Dictionary<Type, BaseMessageDefinition<MockApplication>> requestMap)
        {
            requestMap.Add(typeof(Command.MockCommandDefinition), new Command.MockCommandDefinition(Application));
            requestMap.Add(typeof(Query.MockQueryDefinition), new Query.MockQueryDefinition(Application));

            return requestMap;
        }
    }
}
