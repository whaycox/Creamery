namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class WeekdayNearest : Basic
    {
        public WeekdayNearest(int dayOfMonth)
            : base(dayOfMonth, dayOfMonth)
        { }
    }
}
