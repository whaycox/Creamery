using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Scheduling.Domain
{
    using Abstraction;

    public class ScheduleNode
    {
        public int CheckID { get; }
        public DateTimeOffset ScheduledTime { get; }

        public ScheduleNode Next { get; set; }
        public ScheduleNode Previous { get; set; }

        public ScheduleNode(int checkID, DateTimeOffset scheduledTime)
        {
            CheckID = checkID;
            ScheduledTime = scheduledTime;
        }

        public override string ToString() => $"[{ScheduledTime:yyyy-MM-dd:hhmmss.ffff}]({CheckID})";
    }
}
