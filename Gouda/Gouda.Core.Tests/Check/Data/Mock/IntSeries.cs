namespace Gouda.Check.Data.Mock
{
    public class IntSeries : Domain.IntSeries
    {
        public const int MockValue = 7;

        public static Domain.IntSeries Sample => new IntSeries();

        private IntSeries()
            : base(nameof(IntSeries), MockValue)
        { }
    }
}
