namespace Curds.Cron.Range.Implementation
{
    using Domain;

    internal class WeekdayNearest : Basic
    {
        public WeekdayNearest(int dayOfMonth)
            : base(dayOfMonth, dayOfMonth)
        { }
    }
}
