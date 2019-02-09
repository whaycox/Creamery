using Curds.Application.DateTimes;
using System;

namespace Curds.Infrastructure.DateTimes
{
    public class Machine : IDateTime
    {
        public DateTimeOffset Fetch => DateTimeOffset.Now;
    }
}
