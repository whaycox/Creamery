using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Curds.Domain.Application
{
    public class MockApplication : CurdsApplication
    {
        public override string Description => throw new NotImplementedException();

        public Message.MockDispatch Dispatch { get; }

        public MockApplication(MockOptions options)
            : base(options)
        {
            Dispatch = new Message.MockDispatch(this);
        }
    }
}
