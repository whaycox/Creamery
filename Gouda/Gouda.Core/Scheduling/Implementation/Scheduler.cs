using Curds.Time.Abstraction;

namespace Gouda.Scheduling.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abstraction;
    using Gouda.Domain;
    using Persistence.Abstraction;

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

        public Task<List<CheckDefinition>> ChecksBeforeScheduledTime(DateTimeOffset scheduledTime)
        {
            List<int> checkIDs = Schedule.Trim(scheduledTime);
            return CheckInheritor.Build(checkIDs);
        }

        public void RescheduleCheck(CheckDefinition check)
        {
            DateTimeOffset rescheduleTime = Time.Current.AddSeconds(check.RescheduleSecondInterval.Value);
            Schedule.Add(check.ID, rescheduleTime);
        }
    }
}
