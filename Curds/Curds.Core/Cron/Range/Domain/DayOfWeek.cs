namespace Curds.Cron.Range.Domain
{
    public abstract class DayOfWeek : Basic
    {
        protected const int DaysInWeek = 7;

        public DayOfWeek(int min, int max)
            : base(min, max)
        { }
    }
}
