using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Scheduling.Abstraction
{
    public interface ISchedule
    {
        void Add(int checkID, DateTimeOffset scheduledTime);
        List<int> Trim(DateTimeOffset maxTime);
    }
}
