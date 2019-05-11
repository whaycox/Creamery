namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class LastDayOfWeek : Basic
    {
        public LastDayOfWeek(int dayOfWeek)
            : base(dayOfWeek, dayOfWeek)
        { }
    }
}
