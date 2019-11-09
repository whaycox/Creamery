using System;

namespace Gouda.Time.Implementation
{
    using Abstraction;

    public class MachineTime : ITime
    {
        public DateTimeOffset Current => DateTimeOffset.Now;
    }
}
