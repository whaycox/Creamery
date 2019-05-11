using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Token.Mock
{
    public class Basic : Domain.Basic
    {
        private int _min;
        public override int AbsoluteMin => _min;

        private int _max;
        public override int AbsoluteMax => _max;

        protected override int RetrieveDatePart(DateTime testTime)
        {
            throw new NotImplementedException();
        }

        public void SetMin(int newMin) => _min = newMin;
        public void SetMax(int newMax) => _max = newMax;
    }
}
