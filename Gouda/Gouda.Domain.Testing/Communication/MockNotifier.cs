using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Infrastructure.Communication;
using Curds.Application.DateTimes;
using Gouda.Application.Persistence;

namespace Gouda.Domain.Communication
{
    public class MockNotifier : Notifier
    {
        public MockNotifier(IDateTime time, IPersistence persistence)
            : base(time, persistence)
        { }
    }
}
