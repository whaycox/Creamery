namespace Gouda.Check.Data.Mock
{
    public class LongSeries : Domain.LongSeries
    {
        public const long MockValue = long.MaxValue;

        public static Domain.LongSeries Sample => new LongSeries();

        private LongSeries()
            : base(nameof(LongSeries), MockValue)
        { }

    }
}
