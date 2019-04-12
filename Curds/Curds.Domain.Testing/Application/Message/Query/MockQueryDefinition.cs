using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Query;

namespace Curds.Domain.Application.Message.Query
{
    public class MockQueryDefinition : BaseQueryDefinition<MockApplication, MockQueryHandler, MockQuery, MockViewModel, MockViewModel>
    {
        public MockQueryDefinition(MockApplication application)
            : base(application)
        { }

        public override MockViewModel ViewModel => new MockViewModel() { String = nameof(MockQueryDefinition) };

        public List<MockQuery> QueriesExecuted = new List<MockQuery>();
        public override MockQueryHandler Handler() => new MockQueryHandler(Application, QueriesExecuted);
    }
}
