using System;

namespace Curds.Time.Implementation
{
    using Abstraction;

    public class Machine : ITime
    {
        public DateTimeOffset Fetch => DateTimeOffset.Now;
    }
}
