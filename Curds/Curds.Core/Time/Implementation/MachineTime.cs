using System;

namespace Curds.Time.Implementation
{
    using Abstraction;

    internal class MachineTime : ITime
    {
        public DateTimeOffset Current => DateTimeOffset.Now;
    }
}
