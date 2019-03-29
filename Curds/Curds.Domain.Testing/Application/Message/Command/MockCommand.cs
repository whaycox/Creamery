using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message;

namespace Curds.Domain.Application.Message.Command
{
    public class MockCommand : BaseMessage<MockViewModel>
    {
        public MockCommand(MockViewModel viewModel)
            : base(viewModel)
        { }
    }

    public class MockCommandHandler : BaseMessageHandler<MockApplication, MockCommand, MockViewModel>
    {
        public MockCommandHandler(MockApplication application)
            : base(application)
        { }
    }

    public class MockCommandDefinition : BaseMessageDefinition<MockApplication, MockCommand, MockCommandHandler, MockViewModel>
    {
        protected override MockCommandHandler Handler => throw new NotImplementedException();

        public MockCommandDefinition(MockApplication application)
            : base(application)
        { }

        public override Task<BaseViewModel> ViewModel()
        {
            throw new NotImplementedException();
        }

        protected override MockCommand BuildMessage(MockViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
