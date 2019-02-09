using System;

namespace Curds.Application.DateTimes
{
    public interface IDateTime
    {
        DateTimeOffset Fetch { get; }
    }
}
