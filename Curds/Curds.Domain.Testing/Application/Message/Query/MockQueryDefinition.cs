using Curds.Application.Message.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Domain.Application.Message.Query
{
    public class MockQueryDefinition : BaseQueryDefinition<MockApplication, MockQuery, MockViewModel>
    {
        public MockQueryDefinition(MockApplication application)
            : base(application)
        { }

        public List<MockQuery> QueriesExecuted = new List<MockQuery>();
        public override Task<MockViewModel> Execute(MockQuery message) => Task.Factory.StartNew(() => ExecuteAndReturn(message));
        private MockViewModel ExecuteAndReturn(MockQuery query)
        {
            QueriesExecuted.Add(query);
            return new MockViewModel();
        }
    }
}
