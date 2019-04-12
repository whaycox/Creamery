using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Query;
using System.Threading.Tasks;

namespace Curds.Domain.Application.Message.Query
{
    public class MockQueryHandler : BaseQueryHandler<MockApplication, MockQuery, MockViewModel>
    {
        private List<MockQuery> QueriesExecuted { get; }

        public MockQueryHandler(MockApplication application, List<MockQuery> queriesExecuted)
            : base(application)
        {
            QueriesExecuted = queriesExecuted;
        }

        public override Task<MockViewModel> Execute(MockQuery query)
        {
            QueriesExecuted.Add(query);
            return Task.Factory.StartNew(MockResponse);
        }
        private MockViewModel MockResponse() => new MockViewModel() { String = nameof(MockQueryHandler), Integer = QueriesExecuted.Count };
    }
}
