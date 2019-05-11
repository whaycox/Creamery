namespace Curds.Cron.Range.Implementation
{
    using Domain;

    internal class NthDayOfWeek : Basic
    {
        public int NthValue { get; }

        public NthDayOfWeek(int dayOfWeek, int nthValue)
            : base(dayOfWeek, dayOfWeek)
        {
            NthValue = nthValue;
        }
    }
}
