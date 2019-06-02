using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Curds.Persistence.Implementation
{
    using Abstraction;

    public class SeedGenerator : ISeedGenerator
    {
        private int Last = default(int);

        public int Next => Interlocked.Increment(ref Last);
    }
}
