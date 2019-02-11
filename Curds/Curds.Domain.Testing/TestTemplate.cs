using System;
using System.Collections.Generic;
using System.Text;
using Curds.Infrastructure.Cron;

namespace Curds.Domain
{
    using DateTimes;

    public abstract class TestTemplate<T>
    {
        protected MockDateTime Time = new MockDateTime();

        protected abstract T TestObject { get; }
    }
}
