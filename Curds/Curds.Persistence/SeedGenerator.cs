using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Persistence;
using System.Threading;

namespace Curds.Persistence
{
    public class SeedGenerator : ISeedGenerator
    {
        private int Last = default(int);

        public int Next => Interlocked.Increment(ref Last);
    }
}
