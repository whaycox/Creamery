using Curds.Application.DateTimes;
using System;

namespace Curds.Domain.DateTimes
{
    public class MockDateTime : IDateTime
    {
        public static readonly DateTimeOffset StartingPointInTime = DateTimeOffset.MinValue;
        private DateTimeOffset CurrentPointInTime = StartingPointInTime;

        private static readonly TimeSpan StartingIncrement = TimeSpan.FromTicks(0);
        private TimeSpan CurrentIncrement = StartingIncrement;

        public DateTimeOffset Fetch => FetchInternal();

        private DateTimeOffset FetchInternal()
        {
            CurrentPointInTime = CurrentPointInTime.Add(CurrentIncrement);
            return CurrentPointInTime;
        }

        public void SetPointInTime(DateTimeOffset newFixedPoint)
        {
            CurrentPointInTime = newFixedPoint;
        }

        public void SetIncrement(TimeSpan newIncrement)
        {
            CurrentIncrement = newIncrement;
        }

        public void Reset()
        {
            CurrentPointInTime = StartingPointInTime;
            CurrentIncrement = StartingIncrement;
        }
    }
}
