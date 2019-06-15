namespace Gouda.Check.Data.Mock
{
    using Domain;

    public class Set : Domain.Set
    {
        private static readonly Series[] TestSeries = new Series[]
        {
            IntSeries.Sample,
            LongSeries.Sample,
            DecimalSeries.Sample,
        };

        public static Domain.Set Sample => new Set();

        private Set()
            : base(TestSeries)
        { }
    }
}
