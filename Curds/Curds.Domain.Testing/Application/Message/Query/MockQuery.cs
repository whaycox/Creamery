using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message;

namespace Curds.Domain.Application.Message.Query
{
    public class MockQuery : BaseMessage<MockViewModel>
    {
        public MockQuery(MockViewModel viewModel)
            : base(viewModel)
        { }
    }

    public class MockQueryHandler : BaseMessageHandler<MockApplication, MockQuery, MockViewModel>
    {
        public MockQueryHandler(MockApplication application)
            : base(application)
        { }
    }

    public class MockQueryDefinition : BaseMessageDefinition<MockApplication, MockQuery, MockQueryHandler, MockViewModel>
    {
        protected override MockQueryHandler Handler => throw new NotImplementedException();

        public MockQueryDefinition(MockApplication application)
            : base(application)
        { }

        public override Task<BaseViewModel> ViewModel()
        {
            throw new NotImplementedException();
        }

        protected override MockQuery BuildMessage(MockViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
