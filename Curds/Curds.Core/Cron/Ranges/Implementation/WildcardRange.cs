using System;

namespace Curds.Cron.Ranges.Implementation
{
    using Cron.Abstraction;

    internal class WildcardRange : ICronRange
    {
        public bool IsActive(DateTime testTime) => true;
    }
}
