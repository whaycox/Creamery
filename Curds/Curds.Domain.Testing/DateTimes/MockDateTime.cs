using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.DateTimes;

namespace Curds.Domain.DateTimes
{
    public class MockDateTime : IProvider
    {
        public static readonly DateTimeOffset StartingPointInTime = DateTimeOffset.MinValue;
        private static DateTimeOffset CurrentPointInTime = StartingPointInTime;

        private static readonly TimeSpan StartingIncrement = TimeSpan.FromTicks(0);
        private static TimeSpan CurrentIncrement = StartingIncrement;

        private static object Locker = new object();

        public DateTimeOffset Fetch => FetchInternal();

        private DateTimeOffset FetchInternal()
        {
            lock (Locker)
            {
                CurrentPointInTime = CurrentPointInTime.Add(CurrentIncrement);
                return CurrentPointInTime;
            }
        }

        public static void SetPointInTime(DateTimeOffset newFixedPoint)
        {
            CurrentPointInTime = newFixedPoint;
        }

        public static void SetIncrement(TimeSpan newIncrement)
        {
            CurrentIncrement = newIncrement;
        }

        public static void Reset()
        {
            CurrentPointInTime = StartingPointInTime;
            CurrentIncrement = StartingIncrement;
        }
    }
}
