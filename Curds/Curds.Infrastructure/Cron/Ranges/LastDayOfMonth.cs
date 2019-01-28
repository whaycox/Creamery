using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Cron.Ranges
{
    internal class LastDayOfMonth : Basic
    {
        private const int MaxOffset = 20;

        public int Offset { get; set; }

        public LastDayOfMonth(int offset)
            : base(28, 31)
        {
            if (offset < 0 || offset > MaxOffset)
                throw new ArgumentOutOfRangeException(nameof(offset));
            Offset = offset;
        }
    }
}
