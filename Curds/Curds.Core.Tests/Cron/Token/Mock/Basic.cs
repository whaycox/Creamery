namespace Curds.Cron.Token.Mock
{
    using Enumeration;

    public class Basic : Domain.Basic
    {
        private int _min = int.MinValue;
        public override int AbsoluteMin => _min;

        private int _max = int.MaxValue;
        public override int AbsoluteMax => _max;

        private ExpressionPart _currentPart = ExpressionPart.Minute;
        public override ExpressionPart Part => _currentPart;

        public Basic(Range.Mock.Basic mockRange)
            : base(new Range.Mock.Basic[] { mockRange })
        { }

        public void SetMin(int newMin) => _min = newMin;
        public void SetMax(int newMax) => _max = newMax;

        public void SetExpressionPart(ExpressionPart newPart) => _currentPart = newPart;
    }
}
