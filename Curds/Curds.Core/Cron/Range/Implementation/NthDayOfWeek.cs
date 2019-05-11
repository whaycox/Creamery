namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class NthDayOfWeek : Basic
    {
        public int NthValue { get; }

        public NthDayOfWeek(int dayOfWeek, int nthValue)
            : base(dayOfWeek, dayOfWeek)
        {
            NthValue = nthValue;
        }
    }
}
