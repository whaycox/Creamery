using System;

namespace Curds.Application.DateTimes
{
    public interface IProvider
    {
        DateTimeOffset Fetch { get; }
    }
}
