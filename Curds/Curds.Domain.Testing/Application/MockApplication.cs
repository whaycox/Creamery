using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Curds.Domain.Application
{
    using Message.Dispatch;

    public class MockApplication : CurdsApplication
    {
        public override string Description => nameof(MockApplication);

        public MockSimpleDispatch SimpleDispatch { get; }

        public MockApplication(MockOptions options)
            : base(options)
        {
            SimpleDispatch = new MockSimpleDispatch(this);
        }
    }
}
