using System;

namespace Gouda.Time.Abstraction
{
    public interface ITime
    {
        DateTimeOffset Current { get; }
    }
}
