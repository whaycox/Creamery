namespace Curds.Cron.Range.Implementation
{
    using Domain;

    internal class LastDayOfWeek : Basic
    {
        public LastDayOfWeek(int dayOfWeek)
            : base(dayOfWeek, dayOfWeek)
        { }
    }
}
