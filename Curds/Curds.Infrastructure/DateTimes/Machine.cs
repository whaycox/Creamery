using Curds.Application.DateTimes;
using System;

namespace Curds.Infrastructure.DateTimes
{
    public class Machine : IProvider
    {
        public DateTimeOffset Fetch => DateTimeOffset.Now;
    }
}
