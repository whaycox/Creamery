namespace Gouda.Check.Data.Mock
{
    public class DecimalSeries : Domain.DecimalSeries
    {
        public const decimal MockValue = 9876543210.0123456789m;

        public static DecimalSeries Sample => new DecimalSeries();

        private DecimalSeries()
            : base(nameof(DecimalSeries), MockValue)
        { }
    }
}
