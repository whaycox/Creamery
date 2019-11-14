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
        private ICheckInheritor CheckInheritor { get; }

        public Scheduler(
            ITime time,
            IScheduleFactory scheduleFactory,
            ICheckInheritor checkInheritor)
        {
            Time = time;
            Schedule = scheduleFactory.BuildSchedule();
            CheckInheritor = checkInheritor;
        }

        public Task<List<Check>> ChecksBeforeScheduledTime(DateTimeOffset scheduledTime)
        {
            List<int> checkIDs = Schedule.Trim(scheduledTime);
            return CheckInheritor.Build(checkIDs);
        }

        public void RescheduleCheck(Check check)
        {
            DateTimeOffset rescheduleTime = Time.Current.AddSeconds(check.RescheduleSecondInterval.Value);
            Schedule.Add(check.ID, rescheduleTime);
        }
    }
}
