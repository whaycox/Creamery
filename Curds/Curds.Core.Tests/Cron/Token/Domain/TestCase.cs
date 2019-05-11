using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Domain
{
    public class TestCase
    {
        public Func<IEnumerable<Range.Domain.Basic>> RangeGenerator { get; set; }
        public DateTime Time { get; set; }
        public bool Expected { get; set; }
    }
}
