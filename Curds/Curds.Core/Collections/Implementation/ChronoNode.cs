using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Collections.Implementation
{
    public class ChronoNode<T>
    {
        public ChronoNode<T> Previous { get; set; }
        public ChronoNode<T> Next { get; set; }

        public DateTimeOffset ScheduledTime { get; }
        public T Value { get; }

        public ChronoNode(DateTimeOffset scheduledTime, T value)
        {
            ScheduledTime = scheduledTime;
            Value = value;
        }

        public override string ToString() => $"{ScheduledTime}:{Value}";
    }
}
