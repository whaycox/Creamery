namespace Gouda.Scheduling.Implementation
{
    using Abstraction;

    public class ScheduleFactory : IScheduleFactory
    {
        public ISchedule BuildSchedule() => new Schedule();
    }
}
