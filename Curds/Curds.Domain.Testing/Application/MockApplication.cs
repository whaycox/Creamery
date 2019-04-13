using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Curds.Domain.Application
{
    using Message;

    public class MockApplication : CurdsApplication
    {
        public override string Description => nameof(MockApplication);

        public MockDispatch Dispatch { get; }

        public MockApplication(MockOptions options)
            : base(options)
        {
            Dispatch = new MockDispatch(this);
        }
    }
}
