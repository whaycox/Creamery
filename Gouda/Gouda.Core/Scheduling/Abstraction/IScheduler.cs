using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Scheduling.Abstraction
{
    using Gouda.Domain;

    public interface IScheduler
    {
        Task<List<CheckDefinition>> ChecksBeforeScheduledTime(DateTimeOffset scheduledTime);
        void RescheduleCheck(CheckDefinition check);
    }
}
