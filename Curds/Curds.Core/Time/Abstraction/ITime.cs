using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Time.Abstraction
{
    public interface ITime
    {
        DateTimeOffset Fetch { get; }
    }
}
