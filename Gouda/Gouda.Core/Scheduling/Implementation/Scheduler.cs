namespace Gouda.Scheduling.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abstraction;
    using Gouda.Domain;
    using Persistence.Abstraction;
    using Time.Abstraction;

    public class Scheduler : IScheduler
    {
        private ITime Time { get; }
        private ISchedule Schedule { get; }
        private IGoudaDatabase GoudaDatabase { get; }

        public Scheduler(
            ITime time,
            IScheduleFactory scheduleFactory,
            IGoudaDatabase goudaDatabase)
        {
            Time = time;
            Schedule = scheduleFactory.BuildSchedule();
            GoudaDatabase = goudaDatabase;
        }

        public Task<List<Check>> ChecksBeforeScheduledTime(DateTimeOffset scheduledTime)
        {
            List<int> checkIDs = Schedule.Trim(scheduledTime);
            return GoudaDatabase.Check.FetchMany(checkIDs);
        }

        public void RescheduleCheck(Check check)
        {
            DateTimeOffset rescheduleTime = Time.Current.AddSeconds(check.RescheduleSecondInterval);
            Schedule.Add(check.ID, rescheduleTime);
        }
    }
}
