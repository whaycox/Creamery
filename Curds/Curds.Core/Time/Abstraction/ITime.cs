using System;

namespace Curds.Time.Abstraction
{
    public interface ITime
    {
        DateTimeOffset Current { get; }
    }
}
